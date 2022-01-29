using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FeetState
{
    // Referencia al script de control y a la maquina de estados
    protected Feet feet;
    protected FeetStateMachine stateMachine;

    //protected GameData gameData;
    protected AudioClip audioClip;
    protected ParticleSystem particleSystem;

    protected float startTime; // Referencia para saber cuanto lleva en cada estado

    private string animBoolName; // En esta variable se guardará informacion para las animaciones, así el animator sabrá que animación deberá usar.

    // Variables de movimiento
    public float horizontalInput;
    public float verticalInput;

    // Constructor de estados
    public FeetState(Feet feet, FeetStateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem)
    {
        this.feet = feet; // Referencia a los pies
        this.stateMachine = stateMachine; // Referencia a la maquina de estados
        this.animBoolName = animBoolName; // nombre de la animación
        this.audioClip = audioClip; // Referencia al clip de audio del estado
        this.particleSystem = particleSystem; // Referencia al sistema de particulas
    }


    // Funciones que funcionarán para cada estado cuando se entre(analoga a Start(), salga, se actualize la lógica(analoga a update), se actualize la física(analoga a FixedUpdate)
    // Enter() se ejecutará al entrar en un estado
    public virtual void Enter()
    {
        feet.Anim.SetBool(animBoolName, true); // ponemos el animator en true al entrar

        startTime = Time.time; // Guardamos el instante en el que entra al estado

        //Debug.Log(animBoolName);
    }

    // Exit() se ejecutará al salir del estado
    public virtual void Exit()
    {
        feet.Anim.SetBool(animBoolName, false); // ponemos el animator en false al salir
    }

    // LogicUpdate() se ejecutará en cada Update()
    public virtual void LogicUpdate()
    {
        horizontalInput = feet.RawInput.x;
        verticalInput = feet.RawInput.y;

        if (feet.RawInput.magnitude == 0 && stateMachine.CurrentState != feet.IdleState)
        {
            stateMachine.ChangeState(feet.IdleState);
        }
        else if(feet.RawInput.magnitude != 0)
        {
            RelativeMovementSelector(CalculateRelativeAngle());
        }
    }

    // Metodo para calcular el angulo relativo entre el input de movimiento y la dirección del ratón
    public float CalculateRelativeAngle()
    {
        var playerPos = feet.transform.position;
        var mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        var mousePositionCorrected = new Vector3(mousePosition.x, mousePosition.y, 0f);
        var mouseDir = (mousePositionCorrected - playerPos).normalized;

        Vector2 movement = feet.RawInput;

        var angleTarget = Vector2.SignedAngle(movement, mouseDir);

        angleTarget = ConvertSignedAngleTo360Deg(angleTarget);

        //RelativeMovementSelector(angleTarget);

        return angleTarget;
    }

    // metodo para convertir un angulo con signo a 360 grados
    public float ConvertSignedAngleTo360Deg(float angle)
    {
        if (angle < 0)
        {
            return 360 + angle;
        }

        return angle;
    }

    // Metodo para calcular el estado de los pies en función del angulo de la direccion del ratón respecto a la dirección de movimiento.
    public void RelativeMovementSelector(float angle)
    {
        if ((angle >= 315f || angle < 45f) && stateMachine.CurrentState != feet.MoveState)
        {
            Debug.Log("move");
            stateMachine.ChangeState(feet.MoveState);
        }
        else if (angle >= 45f && angle < 135f && stateMachine.CurrentState != feet.StrafeRightState)
        {
            Debug.Log("strafe right");
            stateMachine.ChangeState(feet.StrafeRightState);
        }
        else if (angle >= 135f && angle < 225f && stateMachine.CurrentState != feet.MoveState)
        {
            Debug.Log("move");
            stateMachine.ChangeState(feet.MoveState);
        }
        else if (angle >= 225f && angle < 315f && stateMachine.CurrentState != feet.StrafeLeftState)
        {
            Debug.Log("strafe left");
            stateMachine.ChangeState(feet.StrafeLeftState);
        }
    }
}
