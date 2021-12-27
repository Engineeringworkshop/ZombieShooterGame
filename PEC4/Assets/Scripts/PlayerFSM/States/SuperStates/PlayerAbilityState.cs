using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone; // bool para saber si el estado ability ha terminado o no

    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, string animBoolName, string animFeetBoolName, AudioClip audioClip, ParticleSystem particleSystem) : base(player, stateMachine, animBoolName, animFeetBoolName, audioClip, particleSystem)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Si la el estado habilidad ha terminado, vuelve al estado por defecto idle.
        if (isAbilityDone)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
