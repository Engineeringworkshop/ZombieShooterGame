using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DeadState : Enemy_State
{
    public Enemy_DeadState(Enemy enemy, Enemy_StateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem) : base(enemy, stateMachine, animBoolName, audioClip, particleSystem)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("estado muerte zombie");

        // incrementamos la puntuación del jugador
        enemy.player.IncreseScore(enemy.enemyData.score);

        // Sonido de muerte
        enemy.AudioSource.PlayOneShot(enemy.enemyData.deadSound);

        // Paramos el game object
        enemy.StopGameObject();
        enemy.Agent.isStopped = true;

        // Paramos el animator
        enemy.Anim.speed = 0;

        // instanciamos el efecto de charco de sangre
        enemy.InstantiateDeadEffect();

        // Desactivo rigidbody y colliders
        enemy.RB.isKinematic = true;
        enemy.GetComponent<CircleCollider2D>().enabled = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // esperamos a que termine el efecto de sonido y destruimos el zombie
        if (Time.time >= startTime + enemy.enemyData.deadSound.length)
        {
            enemy.DestroyGameObject();
            Debug.Log("Destruido zombie");
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
