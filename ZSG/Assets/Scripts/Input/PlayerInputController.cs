using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    Player player;

    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject equipmentAndStatsPanel;
    [SerializeField] GameObject craftingPanel;

    public Vector2 RawMovementInput { get; private set; }

    [HideInInspector] public bool IsShooting { get; private set; }

    private void Start()
    {
        player = GetComponent<Player>();
    }


    void OnMovement(InputValue value)
    {
        RawMovementInput = value.Get<Vector2>();
    }

    void OnShoot(InputValue value)
    {
        IsShooting = value.isPressed;
    }

    void OnReloadWeapon()
    {
        player.WeaponComponent.ReloadWeapon();
    }

    void OnCameraZoom()
    {
        // Programado dentro de la cámara
    }

    void OnHealPlayer()
    {
        player.HealPlayer();
    }

    void OnGameMenu()
    {
        //player.gameplayManager.GameMenu();

        //player.playerInput.Gameplay.Disable();
        //player.playerInput.MenuInventory.Enable();
    }

    void OnPrimaryWeapon()
    {
        player.PrimaryWeapon();
    }
    void OnSecondaryWeapon()
    {
        player.SecondaryWeapon();
    }
    void OnKnifeWeapon()
    {

    }

    void OnInventoryOpen()
    {
        //Debug.Log(player.playerInput.currentActionMap);
        inventoryPanel.gameObject.SetActive(true);
        equipmentAndStatsPanel.SetActive(true);
        craftingPanel.SetActive(true);
        player.playerInput.SwitchCurrentActionMap("MenuAndInventory");
        //Debug.Log(player.playerInput.currentActionMap);
        //player.playerInput.Gameplay.Disable();
        //player.playerInput.MenuAndInventory.Enable();
    }

    void OnInventoryClose()
    {
        //equipmentPanelGameObject.SetActive(false);
        //characterPanelGameObject.SetActive(false);

        //Debug.Log(player.playerInput.currentActionMap);
        inventoryPanel.gameObject.SetActive(false);
        equipmentAndStatsPanel.SetActive(false);
        craftingPanel.SetActive(false);
        player.playerInput.SwitchCurrentActionMap("Gameplay");
        //Debug.Log(player.playerInput.currentActionMap);
    }
}
