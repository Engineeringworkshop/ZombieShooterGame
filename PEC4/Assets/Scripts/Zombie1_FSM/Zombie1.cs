using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie1 : MonoBehaviour, IDamageable
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
    public Zombie1_TurnState TurnState { get; private set; }

    #endregion

    #region Data

    [Header("Data")]

    [SerializeField] private Zombie1Data zombie1Data;

    #endregion

    #region Objetos extra

    [SerializeField] public HealthBar healthBar; // Referencia a la barra de vida

    #endregion

    #region Componentes

    // Creamos las referencias a componentes
    public Animator Anim { get; private set; } // Referencia al animator
    public Rigidbody2D RB { get; private set; } //Referencia al rigidbody2D para controlar las fisicas del player

    #endregion

    #region Atributes

    private float closeRangeDetectionRadius = 3f;

    public LayerMask whatsIsPlayer;

    public float currHealth { get; private set; } // vida actual del zombie

    #endregion

    #region Control variables

    public float minTimeBetweenActions = 1f;
    public float maxTimeBetweenActions = 5f;

    // Variables de movimeinto
    public Vector2 CurrentVelocity { get; private set; } // Creamos un vecotr que guardará la velocidad del player al inicio del frame. Para evitar hacer demasiadas consultas al rigidbody2D. Aumenta el gasto de memoria pero aumenta el rendimiento tambien.
    private Vector2 workspaceVelocity; // Creando este vector, nos evitamos tener que crearlo cada vez que queremos cambiar de velocidad 

    public Vector3 targetPosition;
    public Vector3 targetDirection;


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
        TurnState = new Zombie1_TurnState(this, StateMachine, "zombie1_turn", null, null);

        // cargamos la vida maxima en la variable y en la barra de salud
        currHealth = zombie1Data.maxHealth;
        healthBar.SetMaxHealth(currHealth);
        //TODO desactivar la barra de salud si el enemigo está a 100% hasta que esté dañado
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

        CurrentVelocity = RB.velocity; // Guardamos la velocidad del rigidbody2D al inicio del frame

        CheckPlayerInCloseRange();
    }

    // Dibuja en la escena
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(targetPosition, 1);
        Gizmos.DrawRay(transform.position, targetDirection);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            var effect = Instantiate(zombie1Data.bloodEffect, collision.transform.position, collision.transform.rotation);
            Debug.Log("effect pos: " + effect.transform.up);
            effect.transform.up = gameObject.transform.right;
            Debug.Log("effect pos2: " + effect.transform.up);
        }
    }

    #endregion

    #region Other functions

    // Metodo para modificar la velocidad de movimiento en ambas direcciones, horizontal y vertical
    public void SetVelocity(Vector2 velocity)
    {
        workspaceVelocity.Set(velocity.x, velocity.y);
        RB.velocity = workspaceVelocity; //Actualziamos la velocidad del player
        CurrentVelocity = workspaceVelocity; //Actualizamos la velocidad actual
    }

    // Metodo para detectar al jugador en rango cercano (circulo alrededor del enemigo) 
    void CheckPlayerInCloseRange()
    {
        var collider = Physics2D.OverlapCircle(transform.position, closeRangeDetectionRadius, whatsIsPlayer); // devovlerá true si el circulo creado toca el suelo.
        if (collider != null && collider.CompareTag("Player"))
        {
            //Debug.Log("Zombie1 ha detectado al jugador");
        }
    }

    // metodo para detectar al jugador en rango de visión (cono en dirección frontal del enemigo)
    void CheckPlayerInFrontRange()
    {
        //TODO metodo para detectar al jugador en el cono de visión
    }

    #endregion

    #region coroutines

    // Coroutine para cmabiar al estado de giro despues del tiempo especificado
    public IEnumerator ChangeToTurnStateOnTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        StateMachine.ChangeState(TurnState);
    }

    // metodo del interface IDamageable para que el zombie reciba daño 
    public void Damage(float amount)
    {
        if (currHealth - amount <= 0)
        {
            Debug.Log("Zombie muerto");
            Destroy(gameObject);
        }
        else
        {
            currHealth -= amount;
        }

        //TODO activar la barra al recibir daño
        healthBar.SetHealth(currHealth);

        Debug.Log("Health: " + currHealth);
    }

    #endregion


}

