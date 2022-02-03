// Interface para permitir comunicar cualquier cosa que pueda contener items. Como inventario, loot box, stash (bank)
public interface IItemContainer
{
	bool CanAddItem(Item item, int amount = 1);
	bool AddItem(Item item);

	Item RemoveItem(string itemID);
	bool RemoveItem(Item item);

	void Clear();

	int ItemCount(string itemID);
}
