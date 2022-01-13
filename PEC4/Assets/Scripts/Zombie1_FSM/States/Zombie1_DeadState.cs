using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie1_DeadState : Zombie1_State
{
    protected Zombie1Data zombie1Data;

    public Zombie1_DeadState(Zombie1 zombie1, Zombie1_StateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem, Zombie1Data zombie1Data) : base(zombie1, stateMachine, animBoolName, audioClip, particleSystem)
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

        Debug.Log("estado muerte zombie");

        // incrementamos la puntuación del jugador
        zombie1.player.increseScore(zombie1Data.score);

        // Sonido de muerte
        zombie1.AudioSource.PlayOneShot(zombie1Data.DeadSound);

        // Paramos el game object
        zombie1.StopGameObject();

        // Paramos el animator
        zombie1.Anim.speed = 0;

        // instanciamos el efecto de charco de sangre
        zombie1.InstantiateHealEffect();

        // Desactivo rigidbody y colliders
        zombie1.RB.isKinematic = true;
        zombie1.GetComponent<CircleCollider2D>().enabled = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        zombie1.StopGameObject();

        // esperamos a que termine el efecto de sonido y destruimos el zombie
        if (Time.time >= startTime + zombie1Data.DeadSound.length)
        {
            zombie1.DestroyGameObject();
            Debug.Log("Destruido zombie");
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
