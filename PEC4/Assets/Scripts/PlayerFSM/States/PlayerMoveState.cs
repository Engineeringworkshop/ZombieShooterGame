using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    protected PlayerData playerData;

    protected int xInput;
    protected int yInput;

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem, PlayerData playerData) : base(player, stateMachine, animBoolName, audioClip, particleSystem)
    {
        this.playerData = playerData;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;

        var movementVelocity = playerData.movementVelocity;
        player.SetVelocity(new Vector2(movementVelocity * xInput, movementVelocity * yInput));
        
        // Transición al estado idle si no hay imput.
        if (xInput == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
