using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetState
{
    // Referencia al script de control y a la maquina de estados
    protected Feet feet;
    protected FeetStateMachine stateMachine;

    //protected GameData gameData;
    protected AudioClip audioClip;
    protected ParticleSystem particleSystem;

    protected float startTime; // Referencia para saber cuanto lleva en cada estado

    private string animBoolName; // En esta variable se guardar� informacion para las animaciones, as� el animator sabr� que animaci�n deber� usar.

    // Constructor de estados
    public FeetState(Feet feet, FeetStateMachine stateMachine, string animBoolName, AudioClip audioClip, ParticleSystem particleSystem)
    {
        this.feet = feet; // Referencia a los pies
        this.stateMachine = stateMachine; // Referencia a la maquina de estados
        this.animBoolName = animBoolName; // nombre de la animaci�n
        this.audioClip = audioClip; // Referencia al clip de audio del estado
        this.particleSystem = particleSystem; // Referencia al sistema de particulas
    }


    // Funciones que funcionar�n para cada estado cuando se entre(analoga a Start(), salga, se actualize la l�gica(analoga a update), se actualize la f�sica(analoga a FixedUpdate)
    // Enter() se ejecutar� al entrar en un estado
    public virtual void Enter()
    {
        feet.Anim.SetBool(animBoolName, true); // ponemos el animator en true al entrar

        startTime = Time.time; // Guardamos el instante en el que entra al estado

        Debug.Log(animBoolName);
    }

    // Exit() se ejecutar� al salir del estado
    public virtual void Exit()
    {
        feet.Anim.SetBool(animBoolName, false); // ponemos el animator en false al salir
    }

    // LogicUpdate() se ejecutar� en cada Update()
    public virtual void LogicUpdate()
    {

    }
}
