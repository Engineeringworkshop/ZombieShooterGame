using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Vector3 offset;

    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        //slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);        
        rectTransform.anchoredPosition = transform.parent.localPosition;
    }

    // Metodo para definir el valor maximo (vida maxima) del slider
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        SetHealth(health);
    }

    // Metodo para pdoer indicar la vida actual al slider para que se actualize en consecuencia.
    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
