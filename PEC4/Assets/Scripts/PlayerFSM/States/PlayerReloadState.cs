using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReloadState : PlayerAbilityState
{
    protected PlayerData playerData;

    protected Vector2 movementInput;

    public PlayerReloadState(Player player, PlayerStateMachine stateMachine, string animBoolName, string animFeetBoolName, AudioClip audioClip, ParticleSystem particleSystem, PlayerData playerData) : base(player, stateMachine, animBoolName, animFeetBoolName, audioClip, particleSystem)
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        movementInput = player.playerInput.Gameplay.Movement.ReadValue<Vector2>();

        // Si ha terminado de recargar
        if (!player.WeaponComponent.isReloading)
        {
            isAbilityDone = true;
        }
        // si está recrgando y se mueve
        else if (player.WeaponComponent.isReloading && movementInput.sqrMagnitude != 0)
        {
            player.SetVelocity(movementInput * playerData.movementVelocity);
            player.FeetAnim.Play("move_feet");
        }
        else if (player.WeaponComponent.isReloading && movementInput.sqrMagnitude <= Mathf.Epsilon)
        {
            player.StopPlayer();
            player.FeetAnim.Play("idle_feet");
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
