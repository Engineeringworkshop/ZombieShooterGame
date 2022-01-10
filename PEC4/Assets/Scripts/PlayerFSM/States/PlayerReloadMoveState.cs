using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReloadMoveState : PlayerAbilityState
{
    public PlayerReloadMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName, string animFeetBoolName, AudioClip audioClip, ParticleSystem particleSystem) : base(player, stateMachine, animBoolName, animFeetBoolName, audioClip, particleSystem)
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

        if (!player.WeaponComponent.isReloading)
        {
            isAbilityDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
