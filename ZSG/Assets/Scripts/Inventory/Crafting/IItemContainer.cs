// Interface para permitir comunicar cualquier cosa que pueda contener items. Como inventario, loot box, stash (bank)
public interface IItemContainer
{
    int ItemCount(string itemID);
    Item RemoveItem(string itemID);
    bool RemoveItem(Item item);
    bool AddItem(Item item);
    bool IsFull();
}
