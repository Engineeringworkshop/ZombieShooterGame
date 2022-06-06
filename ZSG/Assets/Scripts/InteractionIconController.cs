using UnityEngine;
using UnityEngine.UI;

public class InteractionIconController : MonoBehaviour
{
    Image image;
    Text text;

    private void OnValidate()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }

        if (text == null)
        {
            text = GetComponentInChildren<Text>();
        }
    }

    // Método para saber si la imagen está activada o desactivada
    public bool ImageIsActive()
    {
        return image.isActiveAndEnabled;
    }

    // Método para activar la imagen
    public void LoadInteractionIcon(string showText)
    {
        image.enabled = true;

        text.text = showText;
        text.enabled = true;
    }

    // Método para desactivar la imagen
    public void DisableInteractionIcon()
    {
        image.enabled = false;
        text.enabled = false;
    }

    // Método para mover el icono
    public void UpdateInteractionIconPosition()
    {
        transform.position = Input.mousePosition + new Vector3(50f, 50f, 0f);
    }
}
