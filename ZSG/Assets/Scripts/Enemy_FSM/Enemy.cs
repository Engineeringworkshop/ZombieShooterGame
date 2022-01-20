using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    #region State Variables

    // Variable para la maquina de estados
    public Enemy_StateMachine StateMachine { get; private set; } // Objeto de maquina de estados

    // Estados
    public Enemy_IdleState IdleState { get; private set; }
    public Enemy_MoveState MoveState { get; private set; }
    public Enemy_AttackState AttackState { get; private set; }
    public Enemy_DeadState DeadState { get; private set; }

    #endregion

    #region Data

    [SerializeField] public Zombie1Data enemyData;

    [SerializeField] public Player player;

    #endregion

    #region Componentes

    // Creamos las referencias a componentes
    public Animator Anim { get; private set; } // Referencia al animator
    public Rigidbody2D RB { get; private set; } //Referencia al rigidbody2D para controlar las fisicas del player
    public AudioSource AudioSource { get; private set; }
    public NavMeshAgent Agent { get; private set; }

    #endregion

    #region Atributes

    [HideInInspector] public bool isPlayerDetected;
    [HideInInspector] public bool isPlayerOnMeleeRange;

    [HideInInspector] public bool isAttackFinished;

    [HideInInspector] public bool isDead;

    [HideInInspector] public Vector3 targetPosition;
    [HideInInspector] public Vector3 targetDirection;

    [SerializeField] public HealthBar healthBar; // Referencia a la barra de vida

    public float CurrHealth { get; private set; } // vida actual del zombie

    // Variables de movimeinto
    public Vector2 CurrentVelocity { get; private set; } // Creamos un vecotr que guardará la velocidad del player al inicio del frame. Para evitar hacer demasiadas consultas al rigidbody2D. Aumenta el gasto de memoria pero aumenta el rendimiento tambien.
    private Vector2 workspaceVelocity; // Creando este vector, nos evitamos tener que crearlo cada vez que queremos cambiar de velocidad 

    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        // Creo el objeto state machine
        StateMachine = new Enemy_StateMachine();

        // Creo los objetos estado
        IdleState = new Enemy_IdleState(this, StateMachine, "zombie1_idle", enemyData.IdleRandomSound, null);
        MoveState = new Enemy_MoveState(this, StateMachine, "zombie1_move", null, null);
        AttackState = new Enemy_AttackState(this, StateMachine, "zombie1_attack", enemyData.AttackSound, null);
        DeadState = new Enemy_DeadState(this, StateMachine, "zombie1_idle", enemyData.deadSound, null);

        // cargamos la vida maxima en la variable y en la barra de salud
        CurrHealth = enemyData.maxHealth;
        healthBar.SetMaxHealth(CurrHealth);

        // desactivamos la barra de salud hasta que se le haga daño
        healthBar.gameObject.SetActive(false);
    }

    void Start()
    {
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        AudioSource = GetComponent<AudioSource>();
        Agent = GetComponent<NavMeshAgent>();

        // Setup del NavMeshAgent
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

        // Inicializamos la maquina de estados
        StateMachine.Initialize(IdleState);

        // Configuración de atributos
        isDead = false;
    }

    void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();

        CurrentVelocity = RB.velocity; // Guardamos la velocidad del rigidbody2D al inicio del frame
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            var effect = Instantiate(enemyData.bloodEffect, collision.transform.position, collision.transform.rotation);
            effect.transform.up = gameObject.transform.right;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetPosition, 1);
        Gizmos.DrawRay(transform.position, targetDirection);

        Gizmos.DrawWireSphere(transform.position, enemyData.rangeDetectionRadius);
        Gizmos.DrawWireSphere(transform.position, enemyData.meleeRange);
    }
    #endregion

    #region Check methods

    // Metodo para detectar al jugador en rango de ataque melee
    public void CheckPlayerInMeleeRange()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, player.transform.position)) <= enemyData.meleeRange)
        {
            // SI está a rango de melee, marca esta posición como posición objetivo.
            targetPosition = transform.position;
            targetDirection = (player.transform.position - transform.position).normalized;
            isPlayerOnMeleeRange = true;
        }
        else
        {
            isPlayerOnMeleeRange = false;
        }
    }

    // Metodo para detectar al jugador en rango cercano (circulo alrededor del enemigo) 
    public void CheckPlayerInFrontRange()
    {
        var collider = Physics2D.OverlapCircle(transform.position, enemyData.rangeDetectionRadius, enemyData.whatsIsPlayer);
        if (collider != null && collider.CompareTag("Player"))
        {
            //Debug.Log("Zombie1 ha detectado al jugador");
            isPlayerDetected = true;
            StartCoroutine(UpdateAgentDestination(0.5f));
            targetPosition = player.transform.position;
            targetDirection = (targetPosition - transform.position).normalized;
        }
        else
        {
            isPlayerDetected = false;
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

    // Metodo para parar el GameObject
    public void StopGameObject()
    {
        SetVelocity(new Vector2(0.0f, 0.0f));
    }

    // Metodo para dañar al jugador
    public void DamagePlayer()
    {
        //Debug.Log("Daño al jugador");
        player.Damage(Random.Range(enemyData.minMeleeDamage, enemyData.maxMeleeDamage));
    }

    // Metodo para activar la barra de salud del enemigo
    private void ActivateHealthBar()
    {
        if (!healthBar.gameObject.activeSelf)
        {
            healthBar.gameObject.SetActive(true);
        }
    }

    // Metodo para poder destruir el game object desde los estados
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    // Metodo para instanciar el efecto muerte
    public void InstantiateDeadEffect()
    {
        var effect = Instantiate(enemyData.deadEffect, transform.position, transform.rotation);

        // Convierto el efecto en hijo del jugador par aque lo siga
        effect.transform.parent = transform;

        // pauso el efecto para pdoer cambiar la duracíon del mismo
        effect.Stop();

        // saco la referencia de la configuración para poder modificarla
        var main = effect.main;
        main.duration = enemyData.deadSound.length;
        effect.Play();
    }

    #endregion

    #region Animation methods

    public void AttackFinished()
    {
        isAttackFinished = true;
    }

    #endregion

    #region Coroutines

    // Coroutine para actualizar la ruta del agente con la posición destino
    public IEnumerator UpdateAgentDestination(float time)
    {
        while (isPlayerDetected)
        {
            yield return new WaitForSecondsRealtime(time);
            Agent.SetDestination(targetPosition);
            //Debug.Log("coroutine running");
        }
    }

    #endregion

    #region IDamageable methods

    // metodo del interface IDamageable para que el zombie reciba daño 
    public void Damage(float amount)
    {
        if (!isDead)
        {
            ActivateHealthBar();

            if (CurrHealth - amount <= 0)
            {
                isDead = true;
                CurrHealth = 0;
                healthBar.gameObject.SetActive(false);
                StopAllCoroutines();
                //StateMachine.ChangeState(DeadState);
            }
            else
            {
                CurrHealth -= amount;
            }

            healthBar.SetHealth(CurrHealth);
        }

    }

    #endregion


}
