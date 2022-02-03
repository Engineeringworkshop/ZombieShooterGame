using UnityEngine;

// Esta clse actua como link entre FileReadWrite e ItemSaveData
public static class ItemSaveIO
{
	private static readonly string baseSavePath;

	// Contructor stático: Se carga automaticamente cuando una variable estatica o metodo estático accede a la clase
	static ItemSaveIO()
	{
		baseSavePath = Application.persistentDataPath;
	}

	// Metodo para guardar los items
	public static void SaveItems(ItemContainerSaveData items, string path)
	{
		FileReadWrite.WriteToBinaryFile(baseSavePath + "/" + path + ".dat", items);
	}

	// Metodo para cargar los items
	public static ItemContainerSaveData LoadItems(string path)
	{
		string filePath = baseSavePath + "/" + path + ".dat";

		if (System.IO.File.Exists(filePath))
		{
			return FileReadWrite.ReadFromBinaryFile<ItemContainerSaveData>(filePath);
		}
		return null;
	}
}
