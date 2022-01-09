using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie1_IdleState : Zombie1_State
{
    //protected Vector2 movementInput;

    public Zombie1_IdleState(Zombie1 zombie1, Zombie1_StateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem) : base(zombie1, stateMachine, animBoolName, audioClip, particleSystem)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        zombie1.SetVelocity(new Vector2(0f, 0f));

        zombie1.StartCoroutine(zombie1.ChangeToTurnStateOnTime(Random.Range(zombie1.minTimeBetweenActions, zombie1.maxTimeBetweenActions)));
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}