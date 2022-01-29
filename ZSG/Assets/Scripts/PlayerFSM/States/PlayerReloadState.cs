using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReloadState : PlayerState
{
    protected PlayerData playerData;
    public PlayerReloadState(Player player, PlayerStateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem, PlayerData playerData) : base(player, stateMachine, animBoolName, audioClip, particleSystem)
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

        player.StopPlayer(); // Paramos al jugador al entrar en el estado Idle

        // Iniciamos la reproducción de efectos de sonido
        player.playerAudioSource.clip = audioClip;
        player.playerAudioSource.Play();
    }

    public override void Exit()
    {
        base.Exit();

        player.playerAudioSource.Stop();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // si el jugador está muerto
        if (player.isDead)
        {
            stateMachine.ChangeState(player.DeadState);
        }
        // Si ha terminado de recargar y no se está moviendo
        else if (!player.WeaponComponent.isReloading && moveInput.sqrMagnitude < Mathf.Epsilon)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        // si ha terminado de recargar y se está moviendo
        else if (!player.WeaponComponent.isReloading && moveInput.sqrMagnitude >= Mathf.Epsilon)
        {
            stateMachine.ChangeState(player.MoveState);
        }
        // si está recrgando y se mueve
        else if (player.WeaponComponent.isReloading && moveInput.sqrMagnitude > Mathf.Epsilon)
        {
            player.SetVelocity(moveInput * playerData.movementVelocity);
        }
        // Si está recargando y no se mueve
        else if (player.WeaponComponent.isReloading && moveInput.sqrMagnitude <= Mathf.Epsilon)
        {
            player.StopPlayer();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
