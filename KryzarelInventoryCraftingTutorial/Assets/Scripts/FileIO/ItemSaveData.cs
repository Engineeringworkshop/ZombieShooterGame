using System;

// Clase para representar los datos que guardaremos del objeto
[Serializable]
public class ItemSlotSaveData
{
	public string ItemID; // En este caso guarda el ItemID, Podemos guardar el objeto entero si queremos poder guardar modificaciones del usuario.
	public int Amount;

	public ItemSlotSaveData(string id, int amount)
	{
		ItemID = id;
		Amount = amount;
	}
}

[Serializable]
public class ItemContainerSaveData
{
	public ItemSlotSaveData[] SavedSlots;

	public ItemContainerSaveData(int numItems)
	{
		SavedSlots = new ItemSlotSaveData[numItems];
	}
}
