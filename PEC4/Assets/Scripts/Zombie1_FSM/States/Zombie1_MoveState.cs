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

        // TODO cuando entre escoge posición objetivo valida con un random raycast
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
        
        // TODO Cuando alcanza la posicion final cambia a estado idle
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
        float distance = Random.Range(minRandomTravelDistance, maxRandomTravelDistance);
        float angle = Random.Range(0f, 2*Mathf.PI);
        var dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);

        // Calculamos la posición objetivo
        targetPosition = currentPosition + distance * dir;

        // Calculamos la dirección del objetivo
        targetDirection = (targetPosition - zombie1.transform.position).normalized;
    }

    // Metodo para comprobar que es una posición destino válida
    private void CheckValidPosition()
    {
        var col = Physics2D.OverlapCircle(targetPosition, 3f, whatsIsBlockingWalk);
        if(col == null)
        {
            MoveToTarget();
        }
    }

    // Metodo para mover y girar el enemigo en la dirección del movimiento
    private void MoveToTarget()
    {
        zombie1.SetVelocity(targetDirection * movementSpeed);
        zombie1.transform.right = targetDirection;
    }

    // Metodo para girar el zombie de forma progresiva
    private void ProgresiveTurn()
    {
        var turnDegByFrames = 5f;
        var amountOfSkipedFrames = 10;
        var numberOfParcialTurns;
        Vector3.Angle(zombie1.transform.right, targetDirection);
    }
}
