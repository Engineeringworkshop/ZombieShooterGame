using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkbenchRecipeUI : MonoBehaviour
{
	[Header("References")]
	[SerializeField] Image recipeIcon;
	[SerializeField] Text recipeTitle;
	[SerializeField] Text recipeDescription;

    [SerializeField] RectTransform arrowParent;
    [SerializeField] BaseItemSlot[] itemSlots;

    [Header("Public Variables")]
    public ItemContainer ItemContainer;


	private CraftingRecipe workbenchRecipe;
	public CraftingRecipe WorkbenchRecipe
	{
		get { return workbenchRecipe; }
		set { SetWorkbenchRecipe(value); }
	}

	public event Action<BaseItemSlot> OnPointerEnterEvent;
	public event Action<BaseItemSlot> OnPointerExitEvent;

	private void OnValidate()
	{
		itemSlots = GetComponentsInChildren<BaseItemSlot>(includeInactive: true);
	}

	private void Start()
	{
		foreach (BaseItemSlot itemSlot in itemSlots)
		{
			itemSlot.OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
			itemSlot.OnPointerExitEvent += slot => OnPointerExitEvent(slot);
		}
	}

	public void OnRecipeClick()
	{
		if (workbenchRecipe != null && ItemContainer != null)
		{
			// TODO cargar receta en la ventana de crafteo
			
		}
	}


	private void SetWorkbenchRecipe(CraftingRecipe newCraftingRecipe)
	{
		// Guarda la receta
		workbenchRecipe = newCraftingRecipe;

		if (workbenchRecipe != null)
		{
			recipeIcon.sprite = newCraftingRecipe.RecipeIcon;
			recipeTitle.text = newCraftingRecipe.RecipeName;
			recipeDescription.text = newCraftingRecipe.RecipeDescription;

			int slotIndex = 0;
			slotIndex = SetSlots(workbenchRecipe.Materials, slotIndex);
			arrowParent.SetSiblingIndex(slotIndex);
			slotIndex = SetSlots(workbenchRecipe.Results, slotIndex);

			for (int i = slotIndex; i < itemSlots.Length; i++)
			{
				itemSlots[i].transform.parent.gameObject.SetActive(false);
			}

			gameObject.SetActive(true);
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

	// Metodo para añadir los slots de la receta, tanto para los materiales como para el resultado
	private int SetSlots(IList<ItemAmount> itemAmountList, int slotIndex)
	{
		for (int i = 0; i < itemAmountList.Count; i++, slotIndex++)
		{
			ItemAmount itemAmount = itemAmountList[i];
			BaseItemSlot itemSlot = itemSlots[slotIndex];

			itemSlot.Item = itemAmount.Item;
			itemSlot.Amount = itemAmount.Amount;
			itemSlot.transform.parent.gameObject.SetActive(true);
		}
		return slotIndex;
	}
}
