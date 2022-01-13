using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamageable
{
    #region State Variables

    // Variable para la maquina de estados
    public PlayerStateMachine StateMachine { get; private set; } // Objeto de maquina de estados
    
    // Estados
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerReloadState ReloadIdleState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }

    #endregion

    #region Data

    [Header("Data")]

    [SerializeField] public PlayerData playerData;
    
    #endregion

    #region Componentes

    // Creamos las referencias a componentes
    public Animator Anim { get; private set; } // Referencia al animator

    [SerializeField] public GameObject feets; // referencia a los pies, para poder coger si animator
    public Animator FeetAnim { get; private set; } // Referencia al animator de los pies

    public PlayerInput playerInput;
    public Rigidbody2D RB { get; private set; } //Referencia al rigidbody2D para controlar las fisicas del player
    public SpriteRenderer PlayerSpriteRenderer { get; private set; }

    [HideInInspector] public AudioSource playerAudioSource;

    [HideInInspector] public BoxCollider2D playerBoxCollider2D;

    [SerializeField] GameplayManager gameplayManager;

    public Weapon WeaponComponent { get; private set; }

    #endregion

    #region Control Variables

    public Vector2 CurrentVelocity { get; private set; } // Creamos un vecotr que guardará la velocidad del player al inicio del frame. Para evitar hacer demasiadas consultas al rigidbody2D. Aumenta el gasto de memoria pero aumenta el rendimiento tambien.
    private Vector2 workspaceVelocity; // Creando este vector, nos evitamos tener que crearlo cada vez que queremos cambiar de velocidad 

    #endregion

    #region Player atributes

    [HideInInspector] public float currHealth;

    [HideInInspector] public int score;

    [HideInInspector] public int currAidKitAmount;

    [HideInInspector] public bool isHealing;

    [HideInInspector] public bool isDead;

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
        MoveState = new PlayerMoveState(this, StateMachine, "move_rifle", "move_feet", playerData.walkSound, null, playerData);
        ReloadIdleState = new PlayerReloadState(this, StateMachine, "reload_rifle", "idle_feet", playerData.reloadWeaponSound, null, playerData);
        DeadState = new PlayerDeadState(this, StateMachine, "", "", playerData.deadSound, null);

        isHealing = false;
        isDead = false;
    }

    // Start 
    void Start()
    {
        Anim = GetComponent<Animator>();
        FeetAnim = feets.GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        playerAudioSource = GetComponent<AudioSource>();
        WeaponComponent = GetComponent<Weapon>();
        PlayerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerBoxCollider2D = GetComponent<BoxCollider2D>();

        // Inicializamos la maquina de estados
        StateMachine.Initialize(IdleState);

        // Cargamos los datos del jugador de prefs
        LoadPlayerPrefs();
    }

    // En cada Update llamamos al LogicUpdate() del estado correspondiente
    void Update()
    {
        StateMachine.CurrentState.LogicUpdate();

        if (playerInput.Gameplay.HealPlayer.ReadValue<float>() > 0.5f)
        {
            HealPlayer();
        }
    }

    // En cada Update llamamos al PhysicsUpdate() del estado correspondiente
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();

        CurrentVelocity = RB.velocity; // Guardamos la velocidad del rigidbody2D al inicio del frame

        FaceMouse(); // Gira el personaje para que apunte a la posición del cursor.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FirstAidKit") && currAidKitAmount < playerData.maxAidKits)
        {
            currAidKitAmount++;
            //playerAudioSource.PlayOneShot(playerData.pickupFirstAidKit);
            //Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("AmmoBox") && WeaponComponent.currentClipAmount < WeaponComponent.weaponData.clipMaxAmmount)
        {
            WeaponComponent.currentClipAmount++;
            //playerAudioSource.PlayOneShot(playerData.pickupAmmo);
            //Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("ExitLevel"))
        {
            SavePlayerPrefs();
            gameplayManager.EndLevel();
        }
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

    #endregion

    #region Other methods

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

    // Metodo para parar al jugador
    public void StopPlayer()
    {
        SetVelocity(new Vector2(0f, 0f)); // Paramos el movimiento si pasa a idle state
    }

    // metodo para incrementar el score del jugador
    public void IncreseScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }

    // metodo para curar al jugador
    public void HealPlayer()
    {
        if (currHealth < playerData.maxHealthBase && currAidKitAmount > 0 && !isHealing)
        {
            isHealing = true;
            StartCoroutine(HealingBoolControl(playerData.healTime));
            InstantiateHealEffect();
            currAidKitAmount--;
            playerAudioSource.PlayOneShot(playerData.healSound);

            if(playerData.maxHealthBase - currHealth <= playerData.maxHealthRestoredAidKit)
            {
                currHealth = playerData.maxHealthBase;
            }
            else
            {
                currHealth += playerData.maxHealthRestoredAidKit;
            }
        }    
    }

    public void InstantiateHealEffect()
    {
        var effect = Instantiate(playerData.healEffect, transform.position, transform.rotation);

        // Convierto el efecto en hijo del jugador par aque lo siga
        effect.transform.parent = transform;

        // pauso el efecto para pdoer cambiar la duracíon del mismo
        effect.Stop();

        // saco la referencia de la configuración para poder modificarla
        var main = effect.main;
        main.duration = playerData.healTime;
        effect.Play();
    }

    public void LoadPlayerPrefs()
    {
        // cargar prefs, si no existen, se cargarán los valores por defecto
        WeaponComponent.currentBulletsInMagazine = PlayerPrefs.GetInt("currentBulletsInMagazine", WeaponComponent.weaponData.clipCapacity);
        WeaponComponent.currentClipAmount = PlayerPrefs.GetInt("currentClipAmount", WeaponComponent.weaponData.clipStartAmmount);
        currAidKitAmount = PlayerPrefs.GetInt("currAidKitAmount", playerData.startAidKits);
        currHealth = PlayerPrefs.GetFloat("currHealth", playerData.startHealth);
        score = PlayerPrefs.GetInt("score", 0);
    }

    public void SavePlayerPrefs()
    {
        // guardar prefs
        PlayerPrefs.SetInt("currentBulletsInMagazine", WeaponComponent.currentBulletsInMagazine); // balas en el cargador
        PlayerPrefs.SetInt("currentClipAmount", WeaponComponent.currentClipAmount); // numero de cargadores
        PlayerPrefs.SetInt("currAidKitAmount", currAidKitAmount); // numero de botiquines
        PlayerPrefs.SetFloat("currHealth", currHealth); // vida actual
        PlayerPrefs.SetInt("score", score); // puntuación actual
    }

    #endregion

    #region Coroutines

    public IEnumerator HealingBoolControl(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        isHealing = false;
    }

    public void Damage(float amount)
    {
        currHealth -= amount;

        // Si la vida llega a 0 muere
        if(currHealth < 0 && !isDead)
        {
            StateMachine.ChangeState(DeadState);
            isDead = true;
        }
    }

    // Metodo para instanciar el efecto muerte
    public void InstantiateDeadEffect()
    {
        var effect = Instantiate(playerData.deadEffect, transform.position, transform.rotation);

        // Convierto el efecto en hijo del jugador par aque lo siga
        effect.transform.parent = transform;

        // pauso el efecto para pdoer cambiar la duracíon del mismo
        effect.Stop();

        // saco la referencia de la configuración para poder modificarla
        var main = effect.main;
        main.duration = playerData.deadSound.length;
        effect.Play();
    }
    public void InstantiateDeadBody()
    {
        var effect = Instantiate(playerData.deadSkull, transform.position, transform.rotation);

        // Convierto el efecto en hijo del jugador par aque lo siga
        effect.transform.parent = transform;
    }
    public IEnumerator ReloadScene(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        gameplayManager.RestartLevel();
    }

    #endregion
}
