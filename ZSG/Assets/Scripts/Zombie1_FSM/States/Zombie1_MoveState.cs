using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie1_MoveState : Zombie1_State
{
    protected Zombie1Data zombie1Data;

    public float movementSpeed = 3f;

    public Zombie1_MoveState(Zombie1 zombie1, Zombie1_StateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem, Zombie1Data zombie1Data) : base(zombie1, stateMachine, animBoolName, audioClip, particleSystem)
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

        if (zombie1.isPlayerDetected)
        {
            // Sonido de detección de jugador
            zombie1.AudioSource.PlayOneShot(zombie1Data.PlayerDetectedSound);
        }


        MoveToTarget();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Giramos el zombie
        zombie1.transform.right = (zombie1.targetPosition - zombie1.transform.position).normalized;

        // SI ha alcanzado el destino vuelve al estado idle
        if (Vector3.Distance(zombie1.transform.position, zombie1.targetPosition) < 0.5f)
        {
            Debug.Log("Se ha alcanzado la posición");
            stateMachine.ChangeState(zombie1.IdleState);
        }
        else if (zombie1.isPlayerOnMeleeRange)
        {
            Debug.Log("Jugador en rango de melee");
            stateMachine.ChangeState(zombie1.AttackState);
        }
        else if (zombie1.isPlayerDetected)
        {
            MoveToTarget();
        }
        else if (Time.time - startTime >= zombie1.maxTimeBetweenActions)
        {
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
