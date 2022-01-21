using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName, string animFeetBoolName, AudioClip audioClip, ParticleSystem particleSystem) : base(player, stateMachine, animBoolName, animFeetBoolName, audioClip, particleSystem)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        // Paramos al jugador
        player.StopPlayer();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Si el jugador está muerto
        if (player.isDead)
        {
            stateMachine.ChangeState(player.DeadState);
        }
        // Si no hay input de movimiento
        else if (moveInput.sqrMagnitude > Mathf.Epsilon)
        {
            stateMachine.ChangeState(player.MoveState);
        }
        // Si esta recargando
        else if (player.WeaponComponent.isReloading)
        {
            stateMachine.ChangeState(player.ReloadState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
