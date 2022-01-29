using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public GameObject item;
    public int ID;
    public string type;
    public string description;

    public bool empty; // para saber si el slot está libre
    public Sprite icon;

    public Transform slotIconGameObject;

    // Metodo para actualziar el icono del item en el slot
    public void UpdateSlot()
    {
        slotIconGameObject.GetComponent<Image>().sprite = icon;

    }
}
