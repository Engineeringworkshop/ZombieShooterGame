using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Pablo.CharacterStats;

public class StatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Propiedades
    private CharacterStat _stat;
    public CharacterStat Stat {
        get { return _stat; }
        set
        {
            _stat = value;
            UpdateStatValue();
        }
    }

    private string _name;
    public string Name {
        get { return _name; }
        set
        {
            _name = value;
            nameText.text = _name;
        }
    }

    [SerializeField] Text nameText;
    [SerializeField] Text valueText;

    [SerializeField] StatTooltip tooltip;

    private void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        nameText = texts[0];
        valueText = texts[1];

        if (tooltip == null)
        {
            tooltip = FindObjectOfType<StatTooltip>();
        }
    }

    // Metodo para actualizar el valor de un stat
    public void UpdateStatValue()
    {
        valueText.text = _stat.Value.ToString();
    }

    // Metodos para eventos de ratón
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.ShowTooltip(Stat, Name); 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }

}
