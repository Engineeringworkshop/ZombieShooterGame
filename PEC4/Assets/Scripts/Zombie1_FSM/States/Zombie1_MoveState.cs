using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie1_MoveState : Zombie1_State
{
    protected Vector3 targetPosition;
    public Zombie1_MoveState(Zombie1 zombie1, Zombie1_StateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem) : base(zombie1, stateMachine, animBoolName, audioClip, particleSystem)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        // TODO cuando entre escoge posición objetivo valida con un random raycast
        PickRandomPosition();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        // TODO Cuando alcanza la posicion final cambia a estado idle
        stateMachine.ChangeState(zombie1.IdleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void PickRandomPosition()
    {
        Vector3 currentPosition = zombie1.transform.position;
        float distance = Random.Range(1f, 7f);
        float angle = Random.Range(0f, 2*Mathf.PI);
        var dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);
        targetPosition = currentPosition + distance * dir;

        //Physics2D.Raycast(zombie1.transform, )
  
    }
}
