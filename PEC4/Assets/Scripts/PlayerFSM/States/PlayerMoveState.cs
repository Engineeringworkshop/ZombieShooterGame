using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    protected PlayerData playerData;

    protected Vector2 movementInput;

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem, PlayerData playerData) : base(player, stateMachine, animBoolName, audioClip, particleSystem)
    {
        this.playerData = playerData;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Obtenemos el movimiento de los controles
        movementInput = player.playerInput.Gameplay.Movement.ReadValue<Vector2>();

        // lo multiplicamos por la velocidad del personaje
        player.SetVelocity(movementInput * playerData.movementVelocity);

        // Transición al estado idle si la magnitud (al cuadrado) es igual a 0. No hay input de movimiento 
        if (movementInput.sqrMagnitude == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
