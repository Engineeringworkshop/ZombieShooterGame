using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie1 : MonoBehaviour
{
    #region State Variables

    // Variable para la maquina de estados
    public Zombie1_StateMachine StateMachine { get; private set; } // Objeto de maquina de estados

    // Estados
    public Zombie1_IdleState IdleState { get; private set; }
    public Zombie1_MoveState MoveState { get; private set; }
    public Zombie1_PlayerDetectedState PlayerDetectedState { get; private set; }
    public Zombie1_AttackState AttackState { get; private set; }
    public Zombie1_DeadState DeadState { get; private set; }

    #endregion

    #region Data

    [Header("Data")]

    [SerializeField] private Zombie1Data zombie1Data;

    #endregion

    #region Componentes

    // Creamos las referencias a componentes
    public Animator Anim { get; private set; } // Referencia al animator

    public Rigidbody2D RB { get; private set; } //Referencia al rigidbody2D para controlar las fisicas del player

    #endregion

    #region Atributes

    private float closeRangeDetectionRadius = 3f;
    public LayerMask whatsIsPlayer;

    #endregion

    #region Control variables

    private float minTimeBetweenActions = 2f;
    private float maxTimeBetweenActions = 5f;

    #endregion

    #region Unity Callback Methods

    private void Awake()
    {
        // Creamos el objeto state machine
        StateMachine = new Zombie1_StateMachine();

        // Creamos los objetos estado
        IdleState = new Zombie1_IdleState(this, StateMachine, "zombie1_idle", null, null);
        MoveState = new Zombie1_MoveState(this, StateMachine, "zombie1_move", null, null);
        PlayerDetectedState = new Zombie1_PlayerDetectedState(this, StateMachine, "", null, null);
        AttackState = new Zombie1_AttackState(this, StateMachine, "zombie1_attack", null, null);
        DeadState = new Zombie1_DeadState(this, StateMachine, "", null, null);
    }

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();

        // Inicializamos la maquina de estados
        StateMachine.Initialize(IdleState);
    }

    // En cada Update llamamos al LogicUpdate() del estado correspondiente
    void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    // En cada Update llamamos al PhysicsUpdate() del estado correspondiente
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();

        CheckPlayerInCloseRange();
    }

    #endregion

    #region Other functions

    // Metodo para detectar al jugador en rango cercano (circulo alrededor del enemigo) 
    void CheckPlayerInCloseRange()
    {
        var collider = Physics2D.OverlapCircle(transform.position, closeRangeDetectionRadius, whatsIsPlayer); // devovlerá true si el circulo creado toca el suelo.
        if (collider != null && collider.CompareTag("Player"))
        {
            Debug.Log("Zombie1 ha detectado al jugador");
        }
    }

    // metodo para detectar al jugador en rango de visión (cono en dirección frontal del enemigo)
    void CheckPlayerInFrontRange()
    {
        //TODO metodo para detectar al jugador en el cono de visión
    }

    #endregion
}
