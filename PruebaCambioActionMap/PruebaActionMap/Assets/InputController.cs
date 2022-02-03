using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public static PlayerInput playerInput;// = new PlayerInput();
    public static event Action<InputActionMap> actionMapChange;    
    //PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        //playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        // Activamos el mapa de acciones
        //playerInput.currentActionMap.Enable();
    }

    private void OnDisable()
    {
        // Desactivamos el mapa de acciones
        //playerInput.currentActionMap.Disable();
    }

    public static void ToggleActionMap(InputActionMap actionMap)
    {
        if (actionMap.enabled)
        {
            return;
        }

        playerInput.actions.Disable();
        actionMapChange?.Invoke(actionMap);
        actionMap.Enable();
    }
}
