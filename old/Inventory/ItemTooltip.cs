using System.Text; // Para crear cadenas de caracteres más facilmente con StringBuilder. Así evitamos crear copias de las cadenas de caracteres
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] Text ItemNameText;
    [SerializeField] Text ItemSlotText;
    [SerializeField] Text ItemStatsText;

    private StringBuilder sb = new StringBuilder();

    // Metodo para mostrar el tooltip de un onjeto equipable
    public void ShowTooltip(EquippableItem item)
    {
        // Cambiamos los textos
        ItemNameText.text = item.ItemName;
        ItemSlotText.text = item.EquipmentType.ToString();

        sb.Length = 0;
        // Añadimos los stats constantes
        AddStat(item.AccuracyBonus, "Accuracy");
        AddStat(item.SpeedBonus, "Speed");

        // Añadimos los stats porcentuales
        AddStat(item.AccuracyPercentBonus, "Accuracy", isPercent: true);
        AddStat(item.SpeedPercentBonus, "Speed", isPercent: true);

        // Cambiamos el texto de los bonuses
        ItemStatsText.text = sb.ToString();

        gameObject.SetActive(true);
    }

    // Metodo para ocultar el tooltip
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    // Metodo auxiliar, para ir añadiendo cada stat a la cadena de caracteres que se mostrará en el tooltip
    private void AddStat(float value, string statName, bool isPercent = false)
    {
        // Solo añadimos el stat si es diferente de 0
        if (value != 0)
        {
            // si no es el primero, lo escribimos en la siguiente linea
            if (sb.Length > 0)
            {
                sb.AppendLine();
            }

            // Si el valor es >0 añadimos un simbolo +
            if (value > 0)
            {
                sb.Append("+");
            }

            // si el valor es un porcentajeo o si no
            if (isPercent)
            {
                sb.Append(value * 100); // Al ser un porcentaje, multiplicamos por 100 y añadimos %
                sb.Append("% ");
            }
            else
            {
                sb.Append(value);
                sb.Append(" ");
            }

            sb.Append(statName);
        }
    }
}
