using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; // Para poder crear una colección que solo se pueda leer

namespace Pablo.CharacterStats
{
    [Serializable]
    public class CharacterStat
    {
        public float BaseValue;

        // Valor final del stat, lo actualizamos cuando se haya producido un cambio (añadido o eliminado un modificador).
        public virtual float Value
        {
            get
            {
                if (isStatModifiersListChanged || BaseValue != lastBaseValue) // Recalcula el valor final si la lista ha cambiado o si el valor base ha cambiado
                {
                    lastBaseValue = BaseValue; // Si el valor base ha cambiado, actualizamos el lastBaseValue
                    _value = CalculateFinalValue();
                    isStatModifiersListChanged = false;
                }
                return _value;
            }
        }

        protected bool isStatModifiersListChanged = true;
        protected float _value;
        protected float lastBaseValue = float.MinValue;

        protected readonly List<StatModifier> statModifiers; // Lista con todos los modificadores que afectarán al stat
        public readonly ReadOnlyCollection<StatModifier> StatModifiers; // Lista publica de modificadores

        public CharacterStat()
        {
            statModifiers = new List<StatModifier>();
            StatModifiers = statModifiers.AsReadOnly(); // inicializamos la lista publica de modificadores
        }

        public CharacterStat(float baseValue) : this()
        {
            BaseValue = baseValue;
        }

        // Metodo para añadir modificadores
        public virtual void AddModifier(StatModifier mod)
        {
            isStatModifiersListChanged = true;
            statModifiers.Add(mod);
            statModifiers.Sort(CompareModifierOrder); // Ordena los modificadores para poner los constantes primero y los % despues. Necesita un metodo de ordenación: CompareModifierOrder
        }

        // Metodo para ordenar la lista de modificadores
        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order)
            {
                return -1;
            }
            else if (a.Order > b.Order)
            {
                return 1;
            }

            return 0; // si a.Order == b.Order
        }

        // Metodo para eliminar modificadores
        public virtual bool RemoveModifier(StatModifier mod)
        {
            // Si el modificador se ha removido, marcamos el flag de actualización con true.
            if (statModifiers.Remove(mod))
            {
                isStatModifiersListChanged = true;
                return true;
            }
            return false;
        }

        // Metodo para eliminar todos los modificadores de un objeto
        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;

            // Recorremos la lista en orden inverso para intentar redicir el número de cambios de indice.
            for (int i = statModifiers.Count - 1; i >= 0; i--)
            {
                if (statModifiers[i].Source == source)
                {
                    isStatModifiersListChanged = true;
                    didRemove = true;
                    statModifiers.RemoveAt(i);
                }
            }

            return didRemove;
        }

        // Metodo para calcular el valor final del stat
        protected virtual float CalculateFinalValue()
        {
            float finalValue = BaseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < statModifiers.Count; i++)
            {
                StatModifier mod = statModifiers[i];

                // Los modificadores constantes se sumarán al valor base del stat
                if (mod.Type == StatModType.Flat)
                {
                    finalValue += mod.Value;
                }
                // Los modificadores de % aditivos se sumarán entre ellos antes de aplicarse, al final de la suma, se aplicarán todos a la vez
                else if (mod.Type == StatModType.PercentAdd)
                {
                    sumPercentAdd += mod.Value;

                    // Recorre la lista de modificadores hasta el final o hasta que encuentre el ultimo modificador aditivo
                    if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                // Los porcentajes multiplicativos se irán aplicando multiplicando uno detrás de otro
                else if (mod.Type == StatModType.PercentMult)
                {
                    finalValue *= 1 + mod.Value;
                }
            }

            // Mas adelante, usaremos modificadores de % que pueden añadir errores de calculo, con 4 decimales deberiamos tener suficiente rpecisión
            return (float)Math.Round(finalValue, 4);
        }


    }
}
