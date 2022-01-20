using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    Player player;

    public Vector2 RawMovementInput { get; private set; }

    private void Start()
    {
        player = GetComponent<Player>();
    }

    void OnMovement(InputValue value)
    {
        RawMovementInput = value.Get<Vector2>();
    }
    void OnShoot()
    {

    }
    void OnReloadWeapon()
    {
        Debug.Log("Reload");
        player.WeaponComponent.ReloadWeapon();
    }
    void OnCameraZoom()
    {

    }
    void OnHealPlayer()
    {
        player.HealPlayer();
    }
    void OnGameMenu()
    {

    }


}
