using System;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchWindow : MonoBehaviour
{
	[Header("References")]
	[SerializeField] WorkbenchRecipeUI recipeUIPrefab;
	[SerializeField] RectTransform recipeUIParent;
	[SerializeField] List<WorkbenchRecipeUI> craftingRecipeUIs;

	[Header("Public Variables")]
	public ItemContainer ItemContainer;
	public List<CraftingRecipe> CraftingRecipes;

	public event Action<BaseItemSlot> OnPointerEnterEvent;
	public event Action<BaseItemSlot> OnPointerExitEvent;

	private void OnValidate()
	{
		Init();
	}

	private void Start()
	{
		Init();

		// A�ade los eventos a cada receta
		foreach (WorkbenchRecipeUI craftingRecipeUI in craftingRecipeUIs)
		{
			craftingRecipeUI.OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
			craftingRecipeUI.OnPointerExitEvent += slot => OnPointerExitEvent(slot);
		}
	}

	// Inicializa las variables y carga las recetas.
	private void Init()
	{
		recipeUIParent.GetComponentsInChildren<WorkbenchRecipeUI>(includeInactive: true, result: craftingRecipeUIs);
		UpdateCraftingRecipes();
	}

	// M�todo para actualziar las recetas del panel
	public void UpdateCraftingRecipes()
	{
		// A�ade las recetas
		for (int i = 0; i < CraftingRecipes.Count; i++)
		{
			// Si ya est�n todos los slots ocupados, a�ade uno m�s a la lista.
			if (craftingRecipeUIs.Count == i)
			{
				craftingRecipeUIs.Add(Instantiate(recipeUIPrefab, recipeUIParent, false));
			}
			// Si el UI est� vac�o, a�ade la receta
			else if (craftingRecipeUIs[i] == null)
			{
				craftingRecipeUIs[i] = Instantiate(recipeUIPrefab, recipeUIParent, false);
			}

			// A�ade el container de donde recoger� los items
			craftingRecipeUIs[i].ItemContainer = ItemContainer;
			// A�ade la receta.
			craftingRecipeUIs[i].WorkbenchRecipe = CraftingRecipes[i];
		}

		// Asigna a null las recetas de las UI extras que no son necesarias (Mejor eliminar estancias???)
		for (int i = CraftingRecipes.Count; i < craftingRecipeUIs.Count; i++)
		{
			craftingRecipeUIs[i].WorkbenchRecipe = null;
		}
	}
}
