using UnityEngine;

public class FeetIdleState : FeetState
{
    public FeetIdleState(Feet feet, FeetStateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem) : base(feet, stateMachine, animBoolName, audioClip, particleSystem)
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

        // Si recibe input de movimiento 
        /*if (verticalInput != 0.0f && horizontalInput == 0)
        {
            stateMachine.ChangeState(feet.MoveState);
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
