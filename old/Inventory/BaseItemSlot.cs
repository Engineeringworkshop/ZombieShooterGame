using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image image;
    [SerializeField] protected Text amountText;

    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;
    public event Action<BaseItemSlot> OnRightClickEvent;

    protected Color normalColor = Color.white;
    protected Color disabledColor = new Color(1, 1, 1, 0);


    protected Item _item;
    public Item Item
    {
        get { return _item; }
        set
        {
            _item = value;

            if (_item == null)
            {
                image.color = disabledColor; // Cambiamos el color a un color transparente. Si desactivamos la imagen no podrá acceder al slot si está desactivado.
            }
            else
            {
                image.sprite = _item.Icon;
                image.color = normalColor;
            }
        }
    }

    // metodo de unity que se llama fuera de play mode también. Util para rellenar referencias antes de cargar el juego, por ejemplo
    protected virtual void OnValidate()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }
    }

    // Metodo para comprobar si el slot puede recibir un objeto
    public virtual bool CanReceiveItem(Item item)
    {
        return false; // Por defecto, no queremos que un slot pueda recibir objetos, lo sobreescribiremos cuando nos interese.
    }

    // Delegaremos toda la gestión de eventos a la clase Character

    public void OnPointerClick(PointerEventData eventData)
    {
        // Checkeamos que es el click correcto
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (OnRightClickEvent != null)
            {
                // La casuistica de lo que pasa con el click event lo manjerá otra clase
                OnRightClickEvent(this);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnPointerEnterEvent != null)
        {
            OnPointerEnterEvent(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnPointerExitEvent != null)
        {
            OnPointerExitEvent(this);
        }
    }
}