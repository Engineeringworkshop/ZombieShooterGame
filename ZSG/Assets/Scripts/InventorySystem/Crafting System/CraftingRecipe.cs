using System;
using System.Collections.Generic;
using UnityEngine;

// Estructura para las recetas. Cada elemento de la receta, tanto materiales como resultados tendrá un Item y una cantidad.
// Para relacionar el Item con si cantidad creamos este struct
[Serializable]
public struct ItemAmount
{
	public Item Item;
	[Range(1, 999)]
	public int Amount;
}

// Script para crear recetas en el interface de Unity
[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
	public List<ItemAmount> Materials;
	public List<ItemAmount> Results;

	public string RecipeName;
	public string RecipeDescription;
	public Sprite RecipeIcon;

	// Método para comprobar si tenemos todo lo necesario para crear la receta
	public bool CanCraft(IItemContainer itemContainer)
	{
		return HasMaterials(itemContainer) && HasSpace(itemContainer);
	}

	// Método para comprobar si tenemos todos los materiales
	private bool HasMaterials(IItemContainer itemContainer)
	{
		foreach (ItemAmount itemAmount in Materials)
		{
			if (itemContainer.ItemCount(itemAmount.Item.ID) < itemAmount.Amount)
			{
				Debug.LogWarning("You don't have the required materials.");
				return false;
			}
		}
		return true;
	}

	// Método para comprobar si tenemos espacio para el resultado
	private bool HasSpace(IItemContainer itemContainer)
	{
		foreach (ItemAmount itemAmount in Results)
		{
			if (!itemContainer.CanAddItem(itemAmount.Item, itemAmount.Amount))
			{
				Debug.LogWarning("Your inventory is full.");
				return false;
			}
		}
		return true;
	}

	// Método para crear la receta
	public void Craft(IItemContainer itemContainer)
	{
		if (CanCraft(itemContainer))
		{
			// Elimina los materiales
			RemoveMaterials(itemContainer);
			// Añade el resultado
			AddResults(itemContainer);
		}
	}

	// Método para eliminar los materiales
	private void RemoveMaterials(IItemContainer itemContainer)
	{
		foreach (ItemAmount itemAmount in Materials)
		{
			for (int i = 0; i < itemAmount.Amount; i++)
			{
				Item oldItem = itemContainer.RemoveItem(itemAmount.Item.ID);
				oldItem.Destroy();
			}
		}
	}

	// Método para añadir el resultado
	private void AddResults(IItemContainer itemContainer)
	{
		foreach (ItemAmount itemAmount in Results)
		{
			for (int i = 0; i < itemAmount.Amount; i++)
			{
				itemContainer.AddItem(itemAmount.Item.GetCopy());
			}
		}
	}
}
