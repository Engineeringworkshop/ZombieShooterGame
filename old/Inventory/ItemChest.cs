using UnityEngine;

public class ItemChest : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Inventory inventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inventory.AddItem(Instantiate(item));
    }



}
