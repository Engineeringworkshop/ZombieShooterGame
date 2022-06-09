using UnityEngine;

// Clase para enlazar los objetos con los elementos del UI, para automatizar la asignación de objetos al crear nuevas escenas.
public class ScreenFrameController : MonoBehaviour
{
    [SerializeField] GameObject deadFrame;

    [SerializeField] GameObject gameMenu;

    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject equipmentAndStatsPanel;
    [SerializeField] GameObject craftingPanel;

    [SerializeField] GameplayManager gameplayManager;

    [SerializeField] PlayerInputController playerInputController;

    // Para guardar el elemento extra actual, un cofre, un banco...
    RectTransform currentElement;

    private void OnValidate()
    {
        if (gameplayManager == null)
        {
            gameplayManager = FindObjectOfType<GameplayManager>(includeInactive: true);
        }

        if (playerInputController == null)
        {
            playerInputController = FindObjectOfType<PlayerInputController>(includeInactive: true);
        }
    }

    private void Start()
    {
        DisableAllPanels();
    }

    // Método para activar y desactivar el frame de muerte
    public void ToggleDeadFrame()
    {
        deadFrame.SetActive(!deadFrame.activeSelf);
    }

    #region Game Menu

    public void EnableGameMenu()
    {
        gameMenu.SetActive(true);
        gameplayManager.PauseGame();
        playerInputController.SwitchActionMap("MenuAndInventory");
    }

    public void DisableGameMenu()
    {
        gameMenu.SetActive(false);
        gameplayManager.ResumeGame();
        playerInputController.SwitchActionMap("Gameplay");
    }

    #endregion

    #region Inventory & stats panels

    // Métodos para activar y desactivar el panel de inventario
    public void EnableInventoryPanel()
    {
        inventoryPanel.SetActive(true);
        gameplayManager.PauseGame();
        playerInputController.SwitchActionMap("MenuAndInventory");
    }

    public void DisableInventoryPanel()
    {
        inventoryPanel.SetActive(false);
        gameplayManager.ResumeGame();
        playerInputController.SwitchActionMap("Gameplay");
    }

    // Métodos para activar y desactivar el panel de quipamiento y estats
    public void EnableEquipmentAndStatsPanel()
    {
        equipmentAndStatsPanel.SetActive(true);
        gameplayManager.PauseGame();
        playerInputController.SwitchActionMap("MenuAndInventory");
    }

    public void DisableEquipmentAndStatsPanel()
    {
        equipmentAndStatsPanel.SetActive(false);
        gameplayManager.ResumeGame();
        playerInputController.SwitchActionMap("Gameplay");
    }

    // Métodos para activar y desactivar el panel de crafteo
    public void EnableCraftingPanel()
    {
        craftingPanel.SetActive(true);
        gameplayManager.PauseGame();
        playerInputController.SwitchActionMap("MenuAndInventory");
    }

    public void DisableCraftingPanel()
    {
        craftingPanel.SetActive(false);
        gameplayManager.ResumeGame();
        playerInputController.SwitchActionMap("Gameplay");
    }

    #endregion

    #region Panel temporal

    // Método para establecer el panel actual en el ScreenFrameController
    public void SetCurrentPanel(RectTransform panel)
    {
        currentElement = panel;
    }

    // Métodos para activar y desactivar el panel del elemento de juego seleccionado
    public void EnableCurrentPanel()
    {
        if (currentElement != null)
        {
            currentElement.gameObject.SetActive(true);
            playerInputController.SwitchActionMap("MenuAndInventory");
        }
        else
        {
            Debug.LogError("Current panel not asignet, can't be activated!");
        }
        
        gameplayManager.PauseGame();
    }

    public void DisableCurrentPanel()
    {
        // Si el panel actual es distinto de null lo desactivamos y lo hacemos null
        if (currentElement != null)
        {
            currentElement.gameObject.SetActive(false);
            playerInputController.SwitchActionMap("Gameplay");

            currentElement = null;
        }
        
        gameplayManager.ResumeGame();
    }

    #endregion

    // Metodo para desactivar todos los paneles 
    public void DisableAllPanels()
    {
        DisableCraftingPanel();
        DisableEquipmentAndStatsPanel();
        DisableInventoryPanel();

        DisableCurrentPanel();

        playerInputController.SwitchActionMap("Gameplay");

        gameplayManager.ResumeGame();
    }
}
