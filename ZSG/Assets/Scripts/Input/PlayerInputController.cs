using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    Player player;

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
        player.gameplayManager.GameMenu();
    }
}
