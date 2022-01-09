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



    public LayerMask whatsIsBlockingWalk;


    public Zombie1_TurnState(Zombie1 zombie1, Zombie1_StateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem) : base(zombie1, stateMachine, animBoolName, audioClip, particleSystem)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        PickRandomPosition();
        CheckValidPosition();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Vector3.Angle(zombie1.transform.right, zombie1.targetDirection) < 0.1)
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

        } while (!CheckValidPosition());

        // Guardamos la dirección del objetivo
        zombie1.targetDirection = dir;

        // Giramos el zombie
        zombie1.transform.right = zombie1.targetDirection;
    }

    // Metodo para comprobar que es una posición destino válida
    private bool CheckValidPosition()
    {
        var col = Physics2D.OverlapCircle(zombie1.targetPosition, 3f, whatsIsBlockingWalk);
        if (col == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}


