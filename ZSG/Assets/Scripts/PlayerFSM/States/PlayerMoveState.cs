using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    protected PlayerData playerData;

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName, string animFeetBoolName, AudioClip audioClip, ParticleSystem particleSystem, PlayerData playerData) : base(player, stateMachine, animBoolName, animFeetBoolName, audioClip, particleSystem)
    {
        this.playerData = playerData;
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

        if (moveInput.sqrMagnitude <= Mathf.Epsilon)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.SetVelocity(playerData.movementVelocity * moveInput);
    }
}
