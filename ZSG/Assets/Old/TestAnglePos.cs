using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestAnglePos : MonoBehaviour
{
    Player player;

    Vector3 playerPos;
    Vector3 mouseDir;
    Vector3 mousePosition;

    void Start()
    {
        player = GetComponent<Player>();
    }


    void Update()
    {
        Vector3 xVector = new Vector3(1f, 0f, 0f);

        playerPos = player.transform.position;
        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        var mousePositionCorrected = new Vector3(mousePosition.x, mousePosition.y, 0f);
        mouseDir = (mousePositionCorrected - playerPos).normalized;

        var dotproduct = Vector3.Dot(xVector, mouseDir.normalized);

        var crossProduct = Vector3.Cross(xVector, mouseDir.normalized);

        var angle = Vector3.Angle(xVector, mouseDir);

        float correctedAngle;

        if (crossProduct.z < 0)
        {
            correctedAngle = -angle;
        }
        else
        {
            correctedAngle = angle;
        }

        Vector2 movement = player.playerInputController.RawMovementInput;

        var angleTarget = Vector2.SignedAngle(movement, mouseDir);

        angleTarget = ConvertSignedAngleTo360Deg(angleTarget);

        RelativeMovementSelector(angleTarget);

        //Debug.Log("PlayerPos: " + playerPos);
        //Debug.Log("mousePosition: " + mousePosition);
        //Debug.Log("mouseDir: " + mouseDir + " angle: " + angle + " dotProd: " + dotproduct + " crossProduct: " + crossProduct + " correctedAngle: " + correctedAngle);
        //Debug.Log("angleTarget: " + angleTarget);
    }

    // metodo para convertir un angulo con signo a 360 grados
    public float ConvertSignedAngleTo360Deg(float angle)
    {
        if(angle < 0)
        {
            return 360 + angle;
        }

        return angle;
    }

    // Metodo para calcular el estado de los pies en función del angulo de la direccion del ratón respecto a la dirección de movimiento.
    public void RelativeMovementSelector(float angle)
    {
        if (angle >= 315f || angle < 45f)
        {
            Debug.Log("move");
        }
        else if (angle >= 45f && angle < 135f)
        {
            Debug.Log("strafe right");
        }
        else if (angle >= 135f && angle < 225f)
        {
            Debug.Log("move");
        }
        else if (angle >= 225f && angle < 315f)
        {
            Debug.Log("strafe left");
        }
    }
}
