using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// interface para cada player state (estado de jugador) 
public class Enemy_State
{
    // Referencia al zombie (protected para poder ser accedido desde las clases que incorporen el interface)
    protected Enemy enemy;
    protected Enemy_StateMachine stateMachine;

    //protected GameData gameData;
    protected AudioClip audioClip;
    protected ParticleSystem particleSystem;

    protected float startTime; // Referencia para saber cuanto lleva en cada estado

    private string animBoolName; // En esta variable se guardar� informacion para las animaciones, as� el animator sabr� que animaci�n deber� usar.

    // Constructor de estados
    public Enemy_State(Enemy enemy, Enemy_StateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem)
    {
        this.enemy = enemy; // Referencia al enemigo
        this.stateMachine = stateMachine; // Referencia a la maquina de estados
        this.animBoolName = animBoolName; // nombre de la animaci�n
        this.audioClip = audioClip; // Referencia al clip de audio del estado
        this.particleSystem = particleSystem; // Referencia al sistema de particulas
    }


    // Funciones que funcionar�n para cada estado cuando se entre(analoga a Start(), salga, se actualize la l�gica(analoga a update), se actualize la f�sica(analoga a FixedUpdate)
    // Enter() se ejecutar� al entrar en un estado
    public virtual void Enter()
    {
        DoChecks();

        // Comprobamos si el cambio tiene animaci�n para evitar advertencias 
        if (animBoolName != "")
        {
            enemy.Anim.SetBool(animBoolName, true); // ponemos el animator en true al entrar
        }

        startTime = Time.time; // Guardamos el instante en el que entra al estado

        //Debug.Log("Enemy: " + animBoolName);
    }

    // Exit() se ejecutar� al salir del estado
    public virtual void Exit()
    {
        // Comprobamos si el cambio tiene animaci�n para evitar advertencias 
        if (animBoolName != "")
        {
            enemy.Anim.SetBool(animBoolName, false); // ponemos el animator en false al salir
        }
    }

    // LogicUpdate() se ejecutar� en cada Update()
    public virtual void LogicUpdate()
    {

    }

    // PhysicsUpdate se ejecutar� en cada FixedUpdate()
    public virtual void PhysicsUpdate()
    {
        DoChecks();

        // Para dar prioridad al estado muerte, afectar� a cualquier estado menos al propio estado muerte
        if (enemy.isDead && stateMachine.CurrentState != enemy.DeadState)
        {
            stateMachine.ChangeState(enemy.DeadState);
        }
    }

    // Se llamar� desde Enter o PhysicsUpdate cuando queramos buscar otros objetos, por ejemplo
    public virtual void DoChecks()
    {

    }

}