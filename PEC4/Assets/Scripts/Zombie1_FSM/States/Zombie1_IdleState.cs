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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // TODO movimiento del zombie    movementInput = zombie1.playerInput.Gameplay.Movement.ReadValue<Vector2>();

        stateMachine.ChangeState(zombie1.MoveState);
    
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}