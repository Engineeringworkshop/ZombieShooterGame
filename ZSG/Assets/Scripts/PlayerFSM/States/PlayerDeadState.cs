using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player player, PlayerStateMachine stateMachine, string animBoolName, string animFeetBoolName, AudioClip audioClip, ParticleSystem particleSystem) : base(player, stateMachine, animBoolName, animFeetBoolName, audioClip, particleSystem)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        // Grito de muerte
        player.playerAudioSource.PlayOneShot(audioClip);

        // Parar jugador
        player.RB.isKinematic = true;
        player.PlayerSpriteRenderer.enabled = false;
        //player.feets.SetActive(false);
        player.playerBoxCollider2D.enabled = false;

        // Invocar charco de sangre y cadaver
        player.InstantiateDeadEffect();
        player.InstantiateDeadBody();

        // Cargar UI flotante despues de X tiempo
        player.StartCoroutine(player.ReloadScene(2f));

        // Desactivo al jugador
        player.enabled = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
