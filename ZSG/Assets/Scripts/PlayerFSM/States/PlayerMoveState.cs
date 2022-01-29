using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    protected PlayerData playerData;

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem, PlayerData playerData) : base(player, stateMachine, animBoolName, audioClip, particleSystem)
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

        // si el jugador está muerto
        if (player.isDead)
        {
            stateMachine.ChangeState(player.DeadState);
        }
        // Si no hay input de movimiento
        else if (moveInput.sqrMagnitude <= Mathf.Epsilon)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        // Si está recargando
        else if (player.WeaponComponent.isReloading)
        {
            stateMachine.ChangeState(player.ReloadState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.SetVelocity(playerData.movementVelocity * moveInput);
    }
}
