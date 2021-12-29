using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// interface para cada player state (estado de jugador) 
public class Zombie1_State
{
    // Referencia al zombie (protected para poder ser accedido desde las clases que incorporen el interface)
    protected Zombie1 zombie1;
    protected Zombie1_StateMachine stateMachine;

    //protected GameData gameData;
    protected AudioClip audioClip;
    protected ParticleSystem particleSystem;

    protected float startTime; // Referencia para saber cuanto lleva en cada estado

    private string animBoolName; // En esta variable se guardará informacion para las animaciones, así el animator sabrá que animación deberá usar.

    // Constructor de estados
    public Zombie1_State(Zombie1 zombie1, Zombie1_StateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem)
    {
        this.zombie1 = zombie1; // Referencia al zombie1
        this.stateMachine = stateMachine; // Referencia a la maquina de estados
        this.animBoolName = animBoolName; // nombre de la animación
        this.audioClip = audioClip; // Referencia al clip de audio del estado
        this.particleSystem = particleSystem; // Referencia al sistema de particulas
    }


    // Funciones que funcionarán para cada estado cuando se entre(analoga a Start(), salga, se actualize la lógica(analoga a update), se actualize la física(analoga a FixedUpdate)
    // Enter() se ejecutará al entrar en un estado
    public virtual void Enter()
    {
        DoChecks();

        zombie1.Anim.SetBool(animBoolName, true); // ponemos el animator en true al entrar

        startTime = Time.time; // Guardamos el instante en el que entra al estado

        //Debug.Log("Zombie1: " + animBoolName);
    }

    // Exit() se ejecutará al salir del estado
    public virtual void Exit()
    {
        zombie1.Anim.SetBool(animBoolName, false); // ponemos el animator en false al salir
    }

    // LogicUpdate() se ejecutará en cada Update()
    public virtual void LogicUpdate()
    {

    }

    // PhysicsUpdate se ejecutará en cada FixedUpdate()
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    // Se llamará desde Enter o PhysicsUpdate cuando queramos buscar otros objetos, por ejemplo
    public virtual void DoChecks()
    {

    }

}