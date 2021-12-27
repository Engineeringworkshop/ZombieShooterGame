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

        StopPlayer(); // Paramos al jugador al entrar
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

        StopPlayer(); // Mantenemos el jugador parado (para que no se mueva al ser impactado
    
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    // Metodo para parar al jugador
    private void StopPlayer()
    {
        player.SetVelocity(new Vector2(0f, 0f)); // Paramos el movimiento si pasa a idle state
    }
}