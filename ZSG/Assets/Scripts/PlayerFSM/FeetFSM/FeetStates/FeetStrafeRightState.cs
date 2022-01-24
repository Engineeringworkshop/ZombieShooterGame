using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetStrafeRightState : FeetState
{
    public FeetStrafeRightState(Feet feet, FeetStateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem) : base(feet, stateMachine, animBoolName, audioClip, particleSystem)
    {
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

        if (verticalInput == 0 && horizontalInput == 0)
        {
            stateMachine.ChangeState(feet.IdleState);
        }

    }
}
