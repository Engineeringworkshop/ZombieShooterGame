using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    Player player;

    [SerializeField] ScreenFrameController screenFrameController;

    public Vector2 RawMovementInput { get; private set; }

    [HideInInspector] public bool IsShooting { get; private set; }

    private void OnValidate()
    {
        if (screenFrameController == null)
        {
            screenFrameController = FindObjectOfType<ScreenFrameController>(includeInactive: true);
        }
    }

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        Debug.Log(player.playerInput.currentActionMap);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        IsShooting = context.ReadValueAsButton();
    }

    public void OnReloadWeapon(InputAction.CallbackContext context)
    {
        player.WeaponComponent.ReloadWeapon();
    }

    public void OnHealPlayer(InputAction.CallbackContext context)
    {
        player.HealPlayer();
    }

    public void OnOpenGameMenu(InputAction.CallbackContext context)
    {
        // Tenemos que asegurarnos que solo haga el cambio de mapa una vez por frame o dará un problema de overflow (player.gameplayManager.OpenGameMenu();) podría ir fuera del if
        if (context.started)
        {
            screenFrameController.EnableGameMenu();
        }
    }

    public void OnCloseGameMenu(InputAction.CallbackContext context)
    {
        // Tenemos que asegurarnos que solo haga el cambio de mapa una vez por frame o dará un problema de overflow (player.gameplayManager.OpenGameMenu();) podría ir fuera del if
        if (context.started)
        {
            screenFrameController.DisableGameMenu();

            //Cierra toda las ventanas abiertas con escape
            screenFrameController.DisableAllPanels();
        }
    }

    public void OnPrimaryWeapon(InputAction.CallbackContext context)
    {
        player.PrimaryWeapon();
    }
    public void OnSecondaryWeapon(InputAction.CallbackContext context)
    {
        player.SecondaryWeapon();
    }
    public void OnKnifeWeapon(InputAction.CallbackContext context)
    {

    }

    public void OnInventoryOpen(InputAction.CallbackContext context)
    {
        // Tenemos que asegurarnos que solo haga el cambio de mapa una vez por frame o dará un problema de overflow (player.gameplayManager.OpenGameMenu();) podría ir fuera del if
        if (context.started)
        {
            // Toggle a los paneles (se activarán)
            screenFrameController.EnableInventoryPanel();
            screenFrameController.EnableEquipmentAndStatsPanel();
        }
    }

    public void OnInventoryClose(InputAction.CallbackContext context)
    {
        // Tenemos que asegurarnos que solo haga el cambio de mapa una vez por frame o dará un problema de overflow (player.gameplayManager.OpenGameMenu();) podría ir fuera del if
        if (context.started)
        {
            // Toggle a los paneles (se cerrarán)
            screenFrameController.DisableInventoryPanel();
            screenFrameController.DisableEquipmentAndStatsPanel();
            //screenFrameController.ToggleCraftingPanel();
        }
    }

    // Método para cambiar de mapa de acciones
    public void SwitchActionMap(string actionMap)
    {
        player.playerInput.SwitchCurrentActionMap(actionMap);
    }
}
