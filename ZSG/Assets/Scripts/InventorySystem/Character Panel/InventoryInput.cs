using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryInput : MonoBehaviour
{
	[SerializeField] GameObject characterPanelGameObject;
	[SerializeField] GameObject equipmentPanelGameObject;
	//[SerializeField] KeyCode[] toggleCharacterPanelKeys;
	//[SerializeField] KeyCode[] toggleInventoryKeys;
	[SerializeField] bool showAndHideMouse = true;
	[SerializeField] Player player;

	[SerializeField] PlayerInput playerUIInput;

    private void Start()
    {
		playerUIInput = GetComponent<PlayerInput>();

	}
    void Update()
	{
		//ToggleCharacterPanel();
		//ToggleInventory();
	}

	// Metodo para cambiar el estado del inventario, si está activo -> desactivado, si está desactivado -> activado
	void OnInventoryClose()
    {
		//equipmentPanelGameObject.SetActive(false);
		//characterPanelGameObject.SetActive(false);

		Debug.Log(player.playerInput.currentActionMap);
		player.character.Inventory.gameObject.SetActive(false);
		player.character.EquipmentPanel.gameObject.SetActive(false);
		player.playerInput.SwitchCurrentActionMap("Gameplay");
		Debug.Log(player.playerInput.currentActionMap);
		//player.playerInput.MenuAndInventory.Disable();
		//player.playerInput.Gameplay.Enable();


		/*

		// Panel del personaje
		characterPanelGameObject.SetActive(!characterPanelGameObject.activeSelf);

		if (characterPanelGameObject.activeSelf)
		{
			equipmentPanelGameObject.SetActive(true);
			ShowMouseCursor();
		}
		else
		{
			HideMouseCursor();
		}

		// Panel del inventario
		if (!characterPanelGameObject.activeSelf)
		{
			characterPanelGameObject.SetActive(true);
			equipmentPanelGameObject.SetActive(false);
			ShowMouseCursor();
		}
		else if (equipmentPanelGameObject.activeSelf)
		{
			equipmentPanelGameObject.SetActive(false);
		}
		else
		{
			characterPanelGameObject.SetActive(false);
			HideMouseCursor();
		}
		*/
	}

	private void ToggleCharacterPanel()
	{
		/*for (int i = 0; i < toggleCharacterPanelKeys.Length; i++)
		{
			if (Input.GetKeyDown(toggleCharacterPanelKeys[i]))
			{
				characterPanelGameObject.SetActive(!characterPanelGameObject.activeSelf);

				if (characterPanelGameObject.activeSelf)
				{
					equipmentPanelGameObject.SetActive(true);
					ShowMouseCursor();
				}
				else
				{
					HideMouseCursor();
				}

				break;
			}
		}*/
	}

	private void ToggleInventory()
	{
		/*for (int i = 0; i < toggleInventoryKeys.Length; i++)
		{
			if (Input.GetKeyDown(toggleInventoryKeys[i]))
			{
				if (!characterPanelGameObject.activeSelf)
				{
					characterPanelGameObject.SetActive(true);
					equipmentPanelGameObject.SetActive(false);
					ShowMouseCursor();
				}
				else if (equipmentPanelGameObject.activeSelf)
				{
					equipmentPanelGameObject.SetActive(false);
				}
				else
				{
					characterPanelGameObject.SetActive(false);
					HideMouseCursor();
				}
				break;
			}
		}*/
	}

	public void ShowMouseCursor()
	{
		if (showAndHideMouse)
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
	}

	public void HideMouseCursor()
	{
		if (showAndHideMouse)
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

	public void ToggleEquipmentPanel()
	{
		equipmentPanelGameObject.SetActive(!equipmentPanelGameObject.activeSelf);
	}
}
