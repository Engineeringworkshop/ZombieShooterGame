using UnityEngine;

public class FeetMoveState : FeetState
{
    public FeetMoveState(Feet feet, FeetStateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem) : base(feet, stateMachine, animBoolName, audioClip, particleSystem)
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

        /*// Si no esta reciviendo input de movimiento
        if (verticalInput == 0 && horizontalInput == 0)
        {
            stateMachine.ChangeState(feet.IdleState);
        }
        else if (horizontalInput > 0.0f)
        {
            stateMachine.ChangeState(feet.StrafeRightState);
        }
        else if (horizontalInput < 0.0f)
        {
            stateMachine.ChangeState(feet.StrafeLeftState);
        }*/
    }
}
