using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MoveState : Enemy_State
{
    public Enemy_MoveState(Enemy enemy, Enemy_StateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem) : base(enemy, stateMachine, animBoolName, audioClip, particleSystem)
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

        enemy.Agent.SetDestination(enemy.targetPosition);

        // Cargamos la coroutine para perseguir al jugador una vez detectado 
        //enemy.StartCoroutine(enemy.UpdateAgentDestination(0.5f));
    }

    public override void Exit()
    {
        base.Exit();

        // Desactivamos la coroutine cuando salimos de move
        //enemy.StopCoroutine(enemy.UpdateAgentDestination(0.5f));
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Giramos el zombie
        enemy.transform.right = enemy.Agent.desiredVelocity;

        if (enemy.Agent.remainingDistance < 0.5f && !enemy.isPlayerDetected)
        {
            Debug.Log("remaining distance" + enemy.Agent.remainingDistance + "Epsilon: " + Mathf.Epsilon);
            stateMachine.ChangeState(enemy.IdleState);
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
