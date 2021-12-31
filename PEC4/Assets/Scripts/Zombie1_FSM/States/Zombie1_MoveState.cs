using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie1_MoveState : Zombie1_State
{
    protected Vector3 targetPosition;

    public float movementSpeed = 3f;

    public float minRandomTravelDistance = 2f;
    public float maxRandomTravelDistance = 8f;

    public Vector3 targetDirection;

    public LayerMask whatsIsBlockingWalk;

    public float turnDegByFrames = 5f;
    public int amountOfSkipedFrames = 10;

    public float angleToTurn;
    public float angleIncrement;
    public float currentTurnAngle;

    public Zombie1_MoveState(Zombie1 zombie1, Zombie1_StateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem) : base(zombie1, stateMachine, animBoolName, audioClip, particleSystem)
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

        // Si ya ha girado, inicia el movimiento
        if (currentTurnAngle > angleToTurn)
        {
            MoveToTarget();
        }

        // SI ha alcanzado el destino vuelve al estado idle
        if (Vector3.Distance(zombie1.transform.position, targetPosition) < 0.1f)
        {
            Debug.Log("Se ha alcanzado la posición");
            stateMachine.ChangeState(zombie1.IdleState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    // Metodo para calcular una posición destino aleatoria
    private void PickRandomPosition()
    {
        Vector3 currentPosition = zombie1.transform.position;

        // repite hasta que encuentre una posición válida
        while (!CheckValidPosition())
        {
            float distance = Random.Range(minRandomTravelDistance, maxRandomTravelDistance);
            float angle = Random.Range(0f, 2 * Mathf.PI);
            var dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);

            // Calculamos la posición objetivo
            targetPosition = currentPosition + distance * dir;

            // Calculamos la dirección del objetivo
            targetDirection = (targetPosition - zombie1.transform.position).normalized;
        }
    }

    // Metodo para comprobar que es una posición destino válida
    private bool CheckValidPosition()
    {
        var col = Physics2D.OverlapCircle(targetPosition, 3f, whatsIsBlockingWalk);
        if(col == null)
        {
            // Si la posición es válida, inicia la coroutine de giro.
            zombie1.StartCoroutine(zombie1.ProgresiveTurn(currentTurnAngle, angleToTurn, angleIncrement));
            return true;
        }
        else
        {
            return false;
        }
    }

    // Metodo para mover y girar el enemigo en la dirección del movimiento
    private void MoveToTarget()
    {
        zombie1.SetVelocity(targetDirection * movementSpeed);
        zombie1.transform.right = targetDirection;
        angleToTurn = Vector3.Angle(zombie1.transform.right, targetDirection);
        angleIncrement = angleToTurn / turnDegByFrames; // grados por frame
    }
}
