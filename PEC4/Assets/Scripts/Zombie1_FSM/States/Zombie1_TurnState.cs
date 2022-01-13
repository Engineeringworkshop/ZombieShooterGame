using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este estado escoge una posición aleatoria y gira progresivamente el enemigo hasta que la encara.
public class Zombie1_TurnState : Zombie1_State
{

    public float minRandomTravelDistance = 2f;
    public float maxRandomTravelDistance = 8f;

    public float turnRadByFrames = 0.5f;
    public int amountOfSkipedFrames = 10;
    protected Zombie1Data zombie1Data;

    protected bool isValidPosition;

    public Zombie1_TurnState(Zombie1 zombie1, Zombie1_StateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem, Zombie1Data zombie1Data) : base(zombie1, stateMachine, animBoolName, audioClip, particleSystem)
    {
        this.zombie1Data = zombie1Data;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        isValidPosition = false;

        PickRandomPosition();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isValidPosition)
        {
            stateMachine.ChangeState(zombie1.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    // Metodo para calcular una posición destino aleatoria
    private void PickRandomPosition()
    {
        Vector3 dir;

        do
        {
            float distance = Random.Range(minRandomTravelDistance, maxRandomTravelDistance);
            float angleToTurn = Random.Range(0f, 2 * Mathf.PI);

            // Calculamos la dirección
            dir = new Vector3(Mathf.Cos(angleToTurn), Mathf.Sin(angleToTurn), 0f);
            
            // Calculamos la posición objetivo
            zombie1.targetPosition = zombie1.transform.position + distance * dir;

            // Guardamos la dirección del objetivo
            zombie1.targetDirection = dir;

            isValidPosition = CheckValidPosition();

        } while (!isValidPosition);

        // Giramos el zombie
        zombie1.transform.right = zombie1.targetDirection;
    }

    // Metodo para comprobar que es una posición destino es válida
    private bool CheckValidPosition()
    {
        RaycastHit2D hit = Physics2D.CircleCast(zombie1.targetPosition, 1f, zombie1.targetDirection, Vector3.Distance(zombie1.targetPosition, zombie1.transform.position), zombie1Data.whatsIsBlockingWalk);

        if (hit.collider != null && hit.collider.tag == "Walls")
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}


