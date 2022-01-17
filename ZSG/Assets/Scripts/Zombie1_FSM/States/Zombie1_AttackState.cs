using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie1_AttackState : Zombie1_State
{
    protected Zombie1Data zombie1Data;

    public Zombie1_AttackState(Zombie1 zombie1, Zombie1_StateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem, Zombie1Data zombie1Data) : base(zombie1, stateMachine, animBoolName, audioClip, particleSystem)
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Para mantener el GameObject quieto mientras ataca.
        zombie1.StopGameObject();

        zombie1.transform.right = zombie1.targetDirection;

        // Si ha pasado el tiempo entre ataques, ha terminado la habilidad de atacar
        if (Time.time - startTime >= zombie1Data.timeBetweenAttacks)
        {
            // Si está detectado y no está a rango de melee se acercará al jugador
            if (zombie1.isPlayerDetected && !zombie1.isPlayerOnMeleeRange)
            {
                stateMachine.ChangeState(zombie1.MoveState);
            }
            // Si no esta detectado, se quedará confuso en idle hasta que vuelva a patrullar
            else if (!zombie1.isPlayerDetected)
            {
                stateMachine.ChangeState(zombie1.IdleState);
            }
            else
            {
                stateMachine.ChangeState(zombie1.IdleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void CheckVisibility()
    {

    }
}
