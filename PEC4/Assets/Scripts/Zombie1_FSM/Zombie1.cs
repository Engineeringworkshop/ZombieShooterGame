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
    public Zombie1_TurnState TurnState { get; private set; }
    public Zombie1_PlayerDetectedState PlayerDetectedState { get; private set; }
    public Zombie1_AttackState AttackState { get; private set; }
    public Zombie1_DeadState DeadState { get; private set; }

    #endregion

    #region Data

    [Header("Data")]

    [SerializeField] private Zombie1Data zombie1Data;

    #endregion

    #region Objetos extra

    [SerializeField] public HealthBar healthBar; // Referencia a la barra de vida

    #endregion

    [SerializeField] public Player player;

    #region Componentes

    // Creamos las referencias a componentes
    public Animator Anim { get; private set; } // Referencia al animator
    public Rigidbody2D RB { get; private set; } //Referencia al rigidbody2D para controlar las fisicas del player
    public AudioSource AudioSource { get; private set; }

    #endregion

    #region Atributes

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

    // flags
    public bool isPlayerDetected;
    public bool isPlayerOnMeleeRange;
    public bool isAttackAnimationFinished;
    public bool isDead;

    #endregion

    #region Unity Callback Methods

    private void Awake()
    {
        // Creamos el objeto state machine
        StateMachine = new Zombie1_StateMachine();

        // Creamos los objetos estado
        IdleState = new Zombie1_IdleState(this, StateMachine, "zombie1_idle", zombie1Data.IdleRandomSound, null);
        MoveState = new Zombie1_MoveState(this, StateMachine, "zombie1_move", null, null, zombie1Data);
        TurnState = new Zombie1_TurnState(this, StateMachine, "", null, null, zombie1Data);
        PlayerDetectedState = new Zombie1_PlayerDetectedState(this, StateMachine, "", zombie1Data.PlayerDetectedSound, null);
        AttackState = new Zombie1_AttackState(this, StateMachine, "zombie1_attack", zombie1Data.AttackSound, null, zombie1Data);
        DeadState = new Zombie1_DeadState(this, StateMachine, "", zombie1Data.DeadSound, null, zombie1Data);
        

        // cargamos la vida maxima en la variable y en la barra de salud
        currHealth = zombie1Data.maxHealth;
        healthBar.SetMaxHealth(currHealth);

        // desactivamos la barra de salud hasta que se le haga daño
        healthBar.gameObject.SetActive(false);

        isPlayerDetected = false;
        isPlayerOnMeleeRange = false;
        isAttackAnimationFinished = false;
        isDead = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        AudioSource = GetComponent<AudioSource>();

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

        CheckPlayerInFrontRange();

        CheckPlayerInCloseRange();
    }

    // Dibuja en la escena
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetPosition, 1);
        Gizmos.DrawRay(transform.position, targetDirection);

        Gizmos.DrawWireSphere(transform.position, zombie1Data.rangeDetectionRadius);
        Gizmos.DrawWireSphere(transform.position, zombie1Data.meleeRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            var effect = Instantiate(zombie1Data.bloodEffect, collision.transform.position, collision.transform.rotation);
            effect.transform.up = gameObject.transform.right;
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

    // Metodo para detectar al jugador en rango de ataque melee) 
    public void CheckPlayerInCloseRange()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, player.transform.position)) <= zombie1Data.meleeRange)
        {
            isPlayerOnMeleeRange = true;
        }
        else
        {
            isPlayerOnMeleeRange = false;
        }
    }

    // Metodo para detectar al jugador en rango cercano (circulo alrededor del enemigo) 
    void CheckPlayerInFrontRange()
    {
        var collider = Physics2D.OverlapCircle(transform.position, zombie1Data.rangeDetectionRadius, zombie1Data.whatsIsPlayer); // devovlerá true si el circulo creado toca el suelo.
        if (collider != null && collider.CompareTag("Player"))
        {
            //Debug.Log("Zombie1 ha detectado al jugador");
            isPlayerDetected = true;
            targetPosition = player.transform.position;
            targetDirection = (targetPosition - transform.position).normalized;
        }
        else
        {
            isPlayerDetected = false;
        }
    }

    #endregion

    #region coroutines

    // Coroutine para cmabiar al estado de giro despues del tiempo especificado
    public IEnumerator ChangeToTurnStateOnTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        StateMachine.ChangeState(TurnState);
    }

    #endregion

    #region IDamageable methods

    // metodo del interface IDamageable para que el zombie reciba daño 
    public void Damage(float amount)
    {
        if (!isDead)
        {
            ActivateHealthBar();

            if (currHealth - amount <= 0)
            {
                isDead = true;
                currHealth = 0;
                healthBar.gameObject.SetActive(false);
                StopAllCoroutines();
                StateMachine.ChangeState(DeadState);
            }
            else
            {
                currHealth -= amount;
            }

            healthBar.SetHealth(currHealth);
        }

    }

    #endregion

    // Metodo para activar la barra de salud del enemigo
    private void ActivateHealthBar()
    {
        if (!healthBar.gameObject.activeSelf)
        {
            healthBar.gameObject.SetActive(true);
        }
    }

    // Metodo para dañar al jugador
    public void DamagePlayer()
    {
        //Debug.Log("Daño al jugador");
        player.Damage(Random.Range(zombie1Data.minMeleeDamage, zombie1Data.maxMeleeDamage));
    }

    // Metodo para indicar que una animación ha terminado
    public void AttackAnimationIsFinished()
    {
        isAttackAnimationFinished = true;
    }

    // Metodo para poder destruir el game object desde los estados
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    // Metodo para instanciar el efecto muerte
    public void InstantiateHealEffect()
    {
        var effect = Instantiate(zombie1Data.deadEffect, transform.position, transform.rotation);

        // Convierto el efecto en hijo del jugador par aque lo siga
        effect.transform.parent = transform;

        // pauso el efecto para pdoer cambiar la duracíon del mismo
        effect.Stop();

        // saco la referencia de la configuración para poder modificarla
        var main = effect.main;
        main.duration = zombie1Data.DeadSound.length;
        effect.Play();
    }

}

