using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Esta clase permitirá recuperar los items cargados del archivo
[CreateAssetMenu]
public class ItemDatabase : ScriptableObject
{
	[SerializeField] Item[] items;

	// Metodo que devuelve el item si encuentra la itemID en la database
	public Item GetItemReference(string itemID)
	{
		foreach (Item item in items)
		{
			if (item.ID == itemID)
			{
				return item;
			}
		}
		return null;
	}

	// Metodo que devuelve una copia del itemID si lo encuentra.
	public Item GetItemCopy(string itemID)
	{
		Item item = GetItemReference(itemID);
		return item != null ? item.GetCopy() : null;
	}

	// Este código solo compila si está en el modo editor. No compilará para la build final
	#if UNITY_EDITOR
	private void OnValidate()
	{
		LoadItems();
	}

	private void OnEnable()
	{
		EditorApplication.projectChanged -= LoadItems;
		EditorApplication.projectChanged += LoadItems;
	}

	private void OnDisable()
	{
		EditorApplication.projectChanged -= LoadItems;
	}

	private void LoadItems()
	{
		items = FindAssetsByType<Item>("Assets/Items");
	}

	// Metodo para cargar todos los objetos de un tipo dado para llenar la base de datos automaticamente

	// Slightly modified version of this answer: http://answers.unity.com/answers/1216386/view.html
	public static T[] FindAssetsByType<T>(params string[] folders) where T : Object
	{
		string type = typeof(T).Name;

		string[] guids;
		// Si no se ha especificado ninguna carpeta, busca en toda partes. Esto es util para filtrar por carpetas los tipos de archivo.
		if (folders == null || folders.Length == 0) {
			guids = AssetDatabase.FindAssets("t:" + type);
		} else {
			guids = AssetDatabase.FindAssets("t:" + type, folders);
		}

		T[] assets = new T[guids.Length];

		for (int i = 0; i < guids.Length; i++)
		{
			string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
			assets[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
		}
		return assets;
	}
	#endif
}
