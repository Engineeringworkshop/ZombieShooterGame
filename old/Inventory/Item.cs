using UnityEngine;
using UnityEditor;

// Queremos que cada objeto sea una copia, pero no queremos perder la información de que todos son copias del mismo

[CreateAssetMenu(fileName = "newItem", menuName = "Item/Items")]
public class Item : ScriptableObject
{
    // Tendremos dos id 
    [SerializeField] string id; // Para poder ver la id y cambiarla en el editor
    public string ID { get { return id; } } // ID que podrá ser consultada en codigo pero no modificada
    public string ItemName;
    public Sprite Icon;

    private void OnValidate()
    {
        // Usamos la ID única que genera unity para cada uno de los assets
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }
}
