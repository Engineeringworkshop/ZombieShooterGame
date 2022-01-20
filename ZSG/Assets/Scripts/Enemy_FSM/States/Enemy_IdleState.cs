using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_IdleState : Enemy_State
{
    public Enemy_IdleState(Enemy enemy, Enemy_StateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem) : base(enemy, stateMachine, animBoolName, audioClip, particleSystem)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        enemy.CheckPlayerInFrontRange();
        enemy.CheckPlayerInMeleeRange();
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

        if (enemy.isPlayerDetected && !enemy.isPlayerOnMeleeRange)
        {
            stateMachine.ChangeState(enemy.MoveState);
        }
        else if (enemy.isPlayerOnMeleeRange && !enemy.player.isDead)
        {
            stateMachine.ChangeState(enemy.AttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
