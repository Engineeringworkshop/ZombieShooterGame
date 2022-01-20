using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    protected Vector2 movementInput;

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

        player.StopPlayer(); // Paramos al jugador al entrar
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        movementInput = player.playerInput.Gameplay.Movement.ReadValue<Vector2>();

        // Transición al estado Move si la magnitud (al cuadrado) es distinta de 0.
        if (movementInput.sqrMagnitude != 0)
        {
            stateMachine.ChangeState(player.MoveState);
        }
        else if (player.playerInput.Gameplay.ReloadWeapon.ReadValue<float>() > 0.5f)
        {
            stateMachine.ChangeState(player.ReloadIdleState);
        }

        player.StopPlayer(); // Mantenemos el jugador parado (para que no se mueva al ser impactado)
    
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}