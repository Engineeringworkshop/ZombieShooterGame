using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    #region State Variables

    // Variable para la maquina de estados
    public FeetStateMachine StateMachine { get; private set; } // Objeto de maquina de estados

    // Estados
    public FeetIdleState IdleState { get; private set; }
    public FeetMoveState MoveState { get; private set; }

    #endregion

    #region Components

    public Animator Anim { get; private set; } // Referencia al animator

    [HideInInspector] public Player player { get; private set; }

    #endregion

    #region Unity Callback Methods

    private void Awake()
    {
        // Creamos el objeto state machine
        StateMachine = new FeetStateMachine();

        // Creamos los objetos estado
        IdleState = new FeetIdleState(this, StateMachine, "idle_feet", null, null);
        MoveState = new FeetMoveState(this, StateMachine, "move_feet", null, null);
    }

    void Start()
    {
        Anim = GetComponent<Animator>();
        player = GetComponentInParent<Player>();

        // Inicializamos la maquina de estados
        StateMachine.Initialize(IdleState);
    }

    void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    #endregion

}
