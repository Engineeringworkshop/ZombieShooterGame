using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private bool isInventoryEnabled;

    // GameObject que representa el inventario
    public GameObject inventory;

    // Objetos de los slots de armas
    public GameObject primaryWeapon;
    public GameObject secondaryWeapon;
    public GameObject meleeWeapon;

    private int maxInventorySlots;

    private int enabledSlots;

    private GameObject[] slots;

    public GameObject slotHolder;

    private Player player;

    void Start()
    {
        player = GetComponent<Player>();

        // Guardamos el numero total de slots directamente de los hijos del slotHolder
        maxInventorySlots = slotHolder.transform.childCount;

        // Creamos la lista de objetos del inventario
        slots = new GameObject[maxInventorySlots];

        // Asignamos los slots a los miembtos de la lista
        for (int i = 0; i < maxInventorySlots; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;

            // si el item esta en null, lo inicializamos como empty = true
            if (slots[i].GetComponent<Slot>().item == null)
            {
                slots[i].GetComponent<Slot>().empty = true;
            }
        }
    }

    // metodo para abrir cerrar inventario
    public void OpenInventory()
    {
        // cambiamos el bool de inventario
        isInventoryEnabled = !isInventoryEnabled;

        // pausamos el juego
        player.gameplayManager.PauseGame();

        // Activamos o desactivamos el panel de inventario
        if (isInventoryEnabled)
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            GameObject itemPickedUp = collision.gameObject;

            Item2 item = itemPickedUp.GetComponent<Item2>();

            AddItem(itemPickedUp, item.ID, item.type, item.description, item.icon);
        }
    }

    public void AddItem(GameObject itemObject, int itemID, string itemType, string itemDescription, Sprite itemIcon)
    {
        for (int i = 0; i < maxInventorySlots; i++)
        {
            if (slots[i].GetComponent<Slot>().empty)
            {
                itemObject.GetComponent<Item2>().pickedUp = true;

                slots[i].GetComponent<Slot>().item = itemObject;
                slots[i].GetComponent<Slot>().ID = itemID;
                slots[i].GetComponent<Slot>().type = itemType;
                slots[i].GetComponent<Slot>().description = itemDescription;
                slots[i].GetComponent<Slot>().icon = itemIcon;

                itemObject.transform.parent = slots[i].transform;
                itemObject.SetActive(false);

                slots[i].GetComponent<Slot>().empty = false;

                // Actualizamos la imagen en el slot 
                slots[i].GetComponent<Slot>().UpdateSlot();

                return; // sustituir por un while.
            }
            
        }
    }

    public void equipPrimaryWeapon()
    {

    }
}
