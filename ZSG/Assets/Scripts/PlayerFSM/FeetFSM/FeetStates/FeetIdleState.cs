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
        if (feet.player.playerInputController.RawMovementInput.sqrMagnitude >= Mathf.Epsilon)
        {
            stateMachine.ChangeState(feet.MoveState);
        }
    }
}
