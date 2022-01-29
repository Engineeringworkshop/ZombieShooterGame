using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour, IItemContainer
{
    [SerializeField] List<Item> startingItems;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots;

    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    // En lugar de usar awake, usamos start para garantizar que Character ha creado los eventos de ratón antes de asignarlos.
    private void Start()
    {
        // Añadimos listener a los eventos del click
        for (int i = 0; i < itemSlots.Length; i++)
        {
            // para añadir un listener usamos el operador += y asignamos un metodo con la misma firma que el evento 
            itemSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            itemSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            itemSlots[i].OnRightClickEvent += OnRightClickEvent;
            itemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            itemSlots[i].OnEndDragEvent += OnEndDragEvent;
            itemSlots[i].OnDragEvent += OnDragEvent;
            itemSlots[i].OnDropEvent += OnDropEvent;
        }

        SetStartingItems();
    }

    // OnValidate solo se ejecuta en unity editor, no en la build del juego. cuidado con RefreshUI (por eso lo tenemos que llamar desde start.
    private void OnValidate()
    {
        if (itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        }

        SetStartingItems();
    }

    // Para refrescar las imagenes del inventario
    private void SetStartingItems()
    {
        int i = 0;
        for (; i < startingItems.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = Instantiate(startingItems[i]);
        }

        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }

    // Metodo para añadir elementos al inventario
    public bool AddItem(Item item)
    {
        // buscamos un slot de inventario libre y ponemos el objeto en el. Devuelve false si no encuentra nada.
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == null)
            {
                itemSlots[i].Item = item;
                return true;
            }
        }

        return false; // TODO si el inventario está lleno, añadir un log en el juego con la información
    }

    // Metodo para eliminar elementos del inventario buscando por item
    public bool RemoveItem(Item item)
    {
        // buscamos el objeto en el inventario, y si lo encuentra lo elimina. Si no lo encuentra devuelve false.
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == item)
            {
                itemSlots[i].Item = null;
                return true;
            }
        }

        return false;
    }

    // Metodo para eliminar elementos del inventario buscando por itemID
    public Item RemoveItem(string itemID)
    {
        // buscamos el objeto en el inventario, y si lo encuentra lo elimina. Si no lo encuentra devuelve false.
        for (int i = 0; i < itemSlots.Length; i++)
        {
            Item item = itemSlots[i].Item;
            if (item != null && item.ID == itemID)
            {
                itemSlots[i].Item = null;
                return item;
            }
        }

        return null;
    }

    // Metodo para checkear si el inventario está lleno
    public bool IsFull()
    {
        // Recorremos todos los slots, si encuentra uno vacio, devuelve true
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == null)
            {
                return false;
            }
        }

        return true;
    }

    /*
    // metodo para bsucar si hay un objeto en el inventario
    public bool ContainsItem(Item item)
    {
        // buscamos el objeto en el inventario, si lo encuentra, devuelve true, si no false.
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == item)
            {
                return true;
            }
        }

        return false;
    }*/

    // Metodo para contar el numero de objetos iguales en el inventario
    public int ItemCount(string itemID)
    {
        int number = 0;
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item.ID == itemID)
            {
                number++;
            }
        }

        return number;
    }
}
