using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    // Atributos del movimiento
    public Vector2 RawMovementInput { get; private set; }

    public int NormInputX { get; private set; } // queremos normalizar el movimiento en x para que sea siempre el mismo con teclado o con mando (en este caso)
    public int NormInputY { get; private set; }

    
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        //Debug.Log("Move input");
        RawMovementInput = context.ReadValue<Vector2>();

        // Separamos las componentes del input
        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }
}
