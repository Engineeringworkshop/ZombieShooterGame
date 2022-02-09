using UnityEngine;
using UnityEngine.UI;

public class DraggableItemController : MonoBehaviour
{
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();

        // La imagen del objeto empieza desactivada
        image.enabled = false;
    }

    // Método para gestionar cuando un objeto empieza a ser arrastrado
    public void DraggableItemBeginDrag(Sprite icon)
    {
        image.sprite = icon;
        transform.position = Input.mousePosition;
        image.enabled = true;
    }

    // Método para gestionar cuando un objeto está siendo arrastrado
    public void DraggableItemDrag()
    {
        transform.position = Input.mousePosition;
    }

    // Método para gestionar cuando un objeto se ha dejado de arrastrar
    public void DraggableItemEndDrag()
    {
        image.enabled = false;
    }
}
