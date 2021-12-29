using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region State Variables

    // Variable para la maquina de estados
    public PlayerStateMachine StateMachine { get; private set; } // Objeto de maquina de estados
    
    // Estados
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }

    #endregion

    #region Data
    
    [Header("Data")]

    [SerializeField] private PlayerData playerData;
    
    #endregion

    #region Componentes

    // Creamos las referencias a componentes
    public Animator Anim { get; private set; } // Referencia al animator

    [SerializeField] GameObject feets; // referencia a los pies, para poder coger si animator
    public Animator FeetAnim { get; private set; } // Referencia al animator de los pies

    public PlayerInput playerInput;
    public Rigidbody2D RB { get; private set; } //Referencia al rigidbody2D para controlar las fisicas del player

    #endregion

    #region Control Variables

    public Vector2 CurrentVelocity { get; private set; } // Creamos un vecotr que guardará la velocidad del player al inicio del frame. Para evitar hacer demasiadas consultas al rigidbody2D. Aumenta el gasto de memoria pero aumenta el rendimiento tambien.
    private Vector2 workspaceVelocity; // Creando este vector, nos evitamos tener que crearlo cada vez que queremos cambiar de velocidad 

    #endregion

    #region Unity Callback Methods

    private void Awake()
    {
        // Creamos el objeto state machine
        StateMachine = new PlayerStateMachine();

        // Creamos el mapa de acciones (controles)
        playerInput = new PlayerInput();

        // Creamos los objetos estado
        IdleState = new PlayerIdleState(this, StateMachine, "idle_rifle", "idle_feet", null, null);
        MoveState = new PlayerMoveState(this, StateMachine, "move_rifle", "move_feet", null, null, playerData);
    }

    // Start 
    void Start()
    {
        Anim = GetComponent<Animator>();
        FeetAnim = feets.GetComponent<Animator>();
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

        CurrentVelocity = RB.velocity; // Guardamos la velocidad del rigidbody2D al inicio del frame

        FaceMouse(); // Gira el personaje para que apunte a la posición del cursor.
    }

    private void OnEnable()
    {
        // Activamos el mapa de acciones
        playerInput.Gameplay.Enable();
    }

    private void OnDisable()
    {
        // Desactivamos el mapa de acciones
        playerInput.Gameplay.Disable();
    }

    #endregion

    #region Set Functions

    // Metodo para modificar la velocidad de movimiento en ambas direcciones, horizontal y vertical
    public void SetVelocity(Vector2 velocity)
    {
        workspaceVelocity.Set(velocity.x, velocity.y);
        RB.velocity = workspaceVelocity; //Actualziamos la velocidad del player
        CurrentVelocity = workspaceVelocity; //Actualizamos la velocidad actual
    }

    // Metodo para modificar la velocidad de movimiento horizontal
    public void SetVelocityX(float velocity)
    {
        workspaceVelocity.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspaceVelocity; //Actualziamos la velocidad del player
        CurrentVelocity = workspaceVelocity; //Actualizamos la velocidad actual
    }

    // Metodo para calcular la velocidad de movimiento vertical
    public void SetVelocityY(float velocity)
    {
        workspaceVelocity.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspaceVelocity;
        CurrentVelocity = workspaceVelocity;
    }

    // Metodo para girar el personaje en la dirección del raton.
    public void FaceMouse()
    {
        //Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
            );

        transform.right = direction;
    }

    #endregion
}
