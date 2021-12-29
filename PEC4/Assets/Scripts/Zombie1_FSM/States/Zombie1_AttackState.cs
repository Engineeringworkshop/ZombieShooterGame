using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie1_AttackState : Zombie1_State
{
    public Zombie1_AttackState(Zombie1 zombie1, Zombie1_StateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem) : base(zombie1, stateMachine, animBoolName, audioClip, particleSystem)
    {
    }
}
