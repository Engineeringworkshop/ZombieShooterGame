using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie1_MoveState : Zombie1_State
{
    public float movementSpeed = 3f;

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
        
        MoveToTarget();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // SI ha alcanzado el destino vuelve al estado idle
        if (Vector3.Distance(zombie1.transform.position, zombie1.targetPosition) < 0.5f)
        {
            Debug.Log("Se ha alcanzado la posición");
            stateMachine.ChangeState(zombie1.IdleState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    // Metodo para iniciar el movimeinto
    private void MoveToTarget()
    {
        zombie1.SetVelocity(zombie1.targetDirection * movementSpeed);
    }
}
