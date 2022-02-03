using UnityEngine;
using UnityEngine.UI;
using Pablo.CharacterStats;

public class Character : MonoBehaviour
{
    // Stat variables

    public CharacterStat Accuracy;
    public CharacterStat WalkingSpeed;


    // Componentes
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] StatPanel statPanel;
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] Image draggableItem; // Craremos una imagen que será la que arrastraremos (Para evitar fallos visuales)

    private BaseItemSlot dragItemSlot;

    private void OnValidate()
    {
        if (itemTooltip == null)    
        {
            itemTooltip = FindObjectOfType<ItemTooltip>();
        }
    }

    private void Awake()
    {
        // Cargamos los estats en el panel y actualizamos los valores
        statPanel.SetStats(Accuracy, WalkingSpeed);
        statPanel.UpdateStatValues();

        // Cargamos las inicializaciones de inventory y equipment. Lo hacemos aqui para garantizar que se han creado los eventos del raton y no dejar que unity randomize el orden

        // Setup Events:
        // Right Click
        inventory.OnRightClickEvent += Equip;
        equipmentPanel.OnRightClickEvent += Unequip;
        // Pointer Enter
        inventory.OnPointerEnterEvent += ShowTooltip;
        equipmentPanel.OnPointerEnterEvent += ShowTooltip;
        // Pointer Exit
        inventory.OnPointerExitEvent += HideTooltip;
        equipmentPanel.OnPointerExitEvent += HideTooltip;
        // Begin Drag
        inventory.OnBeginDragEvent += BeginDrag;
        equipmentPanel.OnBeginDragEvent += BeginDrag;
        // End Drag
        inventory.OnEndDragEvent += EndDrag;
        equipmentPanel.OnEndDragEvent += EndDrag;
        // Drag
        inventory.OnDragEvent += Drag;
        equipmentPanel.OnDragEvent += Drag;
        // Drop
        inventory.OnDropEvent += Drop;
        equipmentPanel.OnDropEvent += Drop;
    }

    // Metodo para equipar "itemSlot"
    private void Equip(BaseItemSlot itemSlot)
    {
        // Tenemos que chequear el tipo correcto
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;

        // Si itemSlot.Item no es equippableItem, se asignará el valor null. Si lo es, se asignará el objeto
        if (equippableItem != null)
        {
            Equip(equippableItem);
        }
    }

    // Metodo para desequipar "itemSlot"
    private void Unequip(BaseItemSlot itemSlot)
    {
        // Tenemos que chequear el tipo correcto
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;

        // Si itemSlot.Item no es equippableItem, se asignará el valor null. Si lo es, se asignará el objeto
        if (equippableItem != null)
        {
            Unequip(equippableItem);
        }
    }

    // Metodo para mostrar el Tooltip
    private void ShowTooltip(BaseItemSlot itemSlot)
    {
        // Tenemos que chequear el tipo correcto
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;

        // Si itemSlot.Item no es equippableItem, se asignará el valor null. Si lo es, se asignará el objeto
        if (equippableItem != null)
        {
            itemTooltip.ShowTooltip(equippableItem);
        }
    }

    // Metodo para ocultar el Tooltip
    private void HideTooltip(BaseItemSlot itemSlot)
    {
        itemTooltip.HideTooltip();
    }

    // Metodo para empezar a arrastrar un item
    private void BeginDrag(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            dragItemSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.Icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

    // Metodo para el final del arrastrado de un item
    private void EndDrag(BaseItemSlot itemSlot)
    {
        dragItemSlot = null;
        draggableItem.enabled = false;
    }

    // Metodo durante el arrastrado de un item
    private void Drag(BaseItemSlot itemSlot)
    {
        // No queremos que lo arrastre si no está activado
        if (draggableItem.enabled)
        {
            // El objeto arrastable sigue la posición del raton
            draggableItem.transform.position = Input.mousePosition;
        }
    }

    private void Drop(BaseItemSlot dropItemSlot)
    {
        // Comprobamos que el slot no sea null para evitar errores
        if (dragItemSlot == null) return;

        // Comprobamos si se pueden intercambiar los objetos. Checkeamos si el slot destino puede aceptar el objeto y si el slot de origen puede aceptar el objeto del slot destino. 
        if (dropItemSlot.CanReceiveItem(dragItemSlot.Item) && dragItemSlot.CanReceiveItem(dropItemSlot.Item))
        {
            // Obtenemos los items
            EquippableItem dragItem = dragItemSlot.Item as EquippableItem;
            EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

            // Comprobamos que los dos slot son equippables
            if (dragItemSlot is EquipmentSlot)
            {
                if (dragItem != null) dragItem.Unequip(this); // Como el objeto viene del equipamiento, lo desequipamos primero si no es null
                if (dropItem != null) dropItem.Equip(this); // una vez desequipado el anterior objeto, equipamos el nuevo
            }
            if (dropItemSlot is EquipmentSlot)
            {
                if (dragItem != null) dragItem.Equip(this); // En este caso equipamos primero el arrastrado
                if (dropItem != null) dropItem.Unequip(this); // desequipamos el anterior objeto equipado
            }

            // actualizamos el panel de estados
            statPanel.UpdateStatValues();

            // Intercambiar items en el inventario
            Item draggedItem = dragItemSlot.Item;
            dragItemSlot.Item = dropItemSlot.Item;
            dropItemSlot.Item = draggedItem;
        }

    }

    // Metodo para equipar un item equipable
    public void Equip(EquippableItem item)
    {
        // Primero lo quitamos del inventario
        if (inventory.RemoveItem(item))
        {
            // lo añadimos al panel de equipamiento
            EquippableItem previousItem;

            if (equipmentPanel.AddItem(item, out previousItem))
            {
                // Si habia algo en el slot lo devolvemos al inventario
                if(previousItem != null)
                {
                    inventory.AddItem(previousItem);

                    // Desactivamos los bonus del objeto y actualizamos el panel
                    previousItem.Unequip(this);
                    statPanel.UpdateStatValues();
                }
                // Cargamos los bonus del objeto a los stats y actualizamos el panel
                item.Equip(this);
                statPanel.UpdateStatValues();
            }
            // Si falla por alguna razon, lo devuelve al inventario. Por ejemplo: Que no sea de un tipo compatible con el slot.
            else
            {
                inventory.AddItem(item);
            }
        }
    }

    // Metodo para desequipar un objeto
    public void Unequip(EquippableItem item)
    {
        // Nos aseguramos de que el inventario no está lleno y lo removemos del panel
        if(!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
            // Quitamos los bonus del objeto a los stats y actualizamos el panel
            item.Unequip(this);
            statPanel.UpdateStatValues();

            // Lo ponemos en el inventario
            inventory.AddItem(item);
        }
    }
}
