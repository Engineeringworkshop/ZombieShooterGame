using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AttackState : Enemy_State
{
    public Enemy_AttackState(Enemy enemy, Enemy_StateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem) : base(enemy, stateMachine, animBoolName, audioClip, particleSystem)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        // reseteamos el flag de ataque terminado
        enemy.isAttackFinished = false;

        // Paramos la navegación al entrar
        enemy.Agent.isStopped = true;
        enemy.Agent.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        enemy.RB.isKinematic = true;
    }

    public override void Exit()
    {
        base.Exit();

        // reactivamos el RB
        enemy.RB.isKinematic = false;
        enemy.Agent.isStopped = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (enemy.isAttackFinished)
        {
            stateMachine.ChangeState(enemy.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.StopGameObject();
    }
}
