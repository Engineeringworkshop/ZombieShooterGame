using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

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
