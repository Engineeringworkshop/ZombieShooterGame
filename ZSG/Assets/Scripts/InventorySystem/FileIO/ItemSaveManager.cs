using System.Collections.Generic;
using UnityEngine;

// convierte los inventarios en objetos guardables
public class ItemSaveManager : MonoBehaviour
{
	[SerializeField] ItemDatabase itemDatabase;

	private const string InventoryFileName = "Inventory";
	private const string EquipmentFileName = "Equipment";

	public void LoadInventory(Character character)
	{
		// Primero tratamos de obtener los items del archivo. Si no puede, no cargará nada.
		ItemContainerSaveData savedSlots = ItemSaveIO.LoadItems(InventoryFileName);
		if (savedSlots == null) return;

		// Vaciamos el inventario
		character.Inventory.Clear();

		// Recorremos los slots
		for (int i = 0; i < savedSlots.SavedSlots.Length; i++)
		{
			// Para mejorar la lectura del código
			ItemSlot itemSlot = character.Inventory.ItemSlots[i];
			ItemSlotSaveData savedSlot = savedSlots.SavedSlots[i];

			if (savedSlot == null)
			{
				itemSlot.Item = null;
				itemSlot.Amount = 0;
			}
			else
			{
				itemSlot.Item = itemDatabase.GetItemCopy(savedSlot.ItemID);
				itemSlot.Amount = savedSlot.Amount;
			}
		}
	}

	public void LoadEquipment(Character character)
	{
		// Tratamos de cargar los objetos
		ItemContainerSaveData savedSlots = ItemSaveIO.LoadItems(EquipmentFileName);
		if (savedSlots == null) return;

		// No podemos cargar los objetos directamente, usamos el metodo que ya tenemos para cargar las modificaciones
		foreach (ItemSlotSaveData savedSlot in savedSlots.SavedSlots)
		{
			// Si el slot es null, no hay nada que hacer, pasamos al siguiente elemento
			if (savedSlot == null) {
				continue;
			}

			// Si no es null, cargamos el objeto
			Item item = itemDatabase.GetItemCopy(savedSlot.ItemID);
			character.Inventory.AddItem(item);
			character.Equip((EquippableItem)item); // Necesitamos que el objeto esté en el inventario, por eso primero lo añadimos al inventario. Se podría modificar el metodo Equip.
		}
	}

	// Metodo para guardar inventario
	public void SaveInventory(Character character)
	{
		SaveItems(character.Inventory.ItemSlots, InventoryFileName);
	}

	// Metodo para guardar el quipamiento
	public void SaveEquipment(Character character)
	{
		SaveItems(character.EquipmentPanel.EquipmentSlots, EquipmentFileName);
	}

	// Metodo para guardar items, guardará array o list.
	private void SaveItems(IList<ItemSlot> itemSlots, string fileName)
	{
		var saveData = new ItemContainerSaveData(itemSlots.Count);

		for (int i = 0; i < saveData.SavedSlots.Length; i++)
		{
			ItemSlot itemSlot = itemSlots[i];

			if (itemSlot.Item == null) {
				saveData.SavedSlots[i] = null;
			} else {
				saveData.SavedSlots[i] = new ItemSlotSaveData(itemSlot.Item.ID, itemSlot.Amount);
			}
		}

		ItemSaveIO.SaveItems(saveData, fileName);
	}
}
