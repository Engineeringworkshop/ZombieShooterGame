using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Pablo.CharacterStats;

public class StatTooltip : MonoBehaviour
{
    [SerializeField] Text StatNameText;
    [SerializeField] Text StatModifiersLabelText;
    [SerializeField] Text StatModifiersText;

    private StringBuilder sb = new StringBuilder();

    // Metodo para mostrar el tooltip de un onjeto equipable
    public void ShowTooltip(CharacterStat stat, string statName)
    {
        // Cambiamos el texto del stat con la funcion auxiliar 
        StatNameText.text = GetStatTopText(stat, statName);

        // Generamos el texto de los modificadores y lo aplicamos
        StatModifiersText.text = GetStatModifiersText(stat);

        gameObject.SetActive(true);
    }

    // Metodo para ocultar el tooltip
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    // Metodo para crear el titulo del stat con los bonuses
    private string GetStatTopText(CharacterStat stat, string statName)
    {
        // Limpiamos el string builder
        sb.Length = 0;

        sb.Append(statName);
        sb.Append(" ");
        sb.Append(stat.Value);

        // Esta parte solo se mostrará si hay modificadores
        if (stat.Value != stat.BaseValue)
        {
            sb.Append(" (");
            sb.Append(stat.BaseValue);

            // si el valor es distinto del valor base, habrá dos miembros, base + bonus, añadimos el símbolo +
            if (stat.Value > stat.BaseValue)
            {
                sb.Append("+");
            }

            sb.Append(System.Math.Round((stat.Value - stat.BaseValue), 4));

            sb.Append(")");
        }


        return sb.ToString();
    }

    // Metodo para crear la lista de modificadores
    private string GetStatModifiersText(CharacterStat stat)
    {
        // Limpiamos el string builder
        sb.Length = 0;

        foreach (StatModifier mod in stat.StatModifiers)
        {
            // Si no es el primer modificador, añadimos un salto de linea
            if(sb.Length > 0)
            {
                sb.AppendLine();
            }

            // Si el valor es >0 añadimos un +
            if(mod.Value > 0)
            {
                sb.Append("+");
            }

            // si es modificador constante o porcentual
            if (mod.Type == StatModType.Flat)
            {
                sb.Append(mod.Value);
            }
            else
            {
                sb.Append(mod.Value * 100);
                sb.Append("%");
            }
            

            // Checkeamos si el item es equipable, si lo es, devolverá el item, si no lo es, devolverá null
            EquippableItem item = mod.Source as EquippableItem;

            if (item != null)
            {
                sb.Append(" ");
                sb.Append(item.ItemName);
            }
            else
            {
                Debug.LogError("Modifier is not an EquippableItem!");
            }
        }

        return sb.ToString();
    }
}
