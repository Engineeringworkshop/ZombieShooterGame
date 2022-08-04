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
    public PlayerReloadState ReloadState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }

    #endregion

    #region Data

    [Header("Data")]

    [SerializeField] public PlayerData playerData;

    #endregion

    #region Componentes

    // Creamos las referencias a componentes
    public Animator Anim { get; private set; } // Referencia al animator

    [HideInInspector] public PlayerInput playerInput;

    [HideInInspector] public PlayerInputController playerInputController;

    public Rigidbody2D RB { get; private set; } //Referencia al rigidbody2D para controlar las fisicas del player
    public SpriteRenderer PlayerSpriteRenderer { get; private set; }


    [HideInInspector] public AudioSource playerAudioSource;

    [HideInInspector] public CapsuleCollider2D playerCapsuleCollider2D;

    [SerializeField] public GameplayManager gameplayManager;

    [SerializeField] public Character character;

    [SerializeField] HUDController hudController;

    [SerializeField] ScoreController scoreController;

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

    private void OnValidate()
    {
        if (character == null)
        {
            character = FindObjectOfType<Character>(includeInactive: true);
        }

        if (hudController == null)
        {
            hudController = FindObjectOfType<HUDController>(includeInactive: true);
        }

        if (scoreController == null)
        {
            scoreController = FindObjectOfType<ScoreController>(includeInactive: true);
        }

        if (gameplayManager == null)
        {
            gameplayManager = FindObjectOfType<GameplayManager>(includeInactive: true);
        }
    }

    private void Awake()
    {
        // Creamos el objeto state machine
        StateMachine = new PlayerStateMachine();

        // Creamos los objetos estado
        IdleState = new PlayerIdleState(this, StateMachine, "idle", null, null);
        MoveState = new PlayerMoveState(this, StateMachine, "move", playerData.walkSound, null, playerData);
        ReloadState = new PlayerReloadState(this, StateMachine, "reload", playerData.reloadWeaponSound, null, playerData);
        //melee
        //shoot
        DeadState = new PlayerDeadState(this, StateMachine, "", playerData.deadSound, null);

        isHealing = false;
        isDead = false;

        // Enlaza las componentes
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        playerAudioSource = GetComponent<AudioSource>();
        WeaponComponent = GetComponent<Weapon>();
        PlayerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerCapsuleCollider2D = GetComponent<CapsuleCollider2D>();

        playerInput = GetComponent<PlayerInput>();

        playerInputController = GetComponent<PlayerInputController>();

        character = GetComponent<Character>();
    }

    // Start 
    void Start()
    {
        

        // Inicializamos la maquina de estados
        StateMachine.Initialize(IdleState);

        // Cargamos los datos del jugador de prefs
        LoadPlayerPrefs();

        // Carga el contador de puntos y el HUD
        scoreController.UpdateScoreDisplay(score);
        hudController.SetMaxHealthOnHealthBar(playerData.maxHealthBase);
    }

    // En cada Update llamamos al LogicUpdate() del estado correspondiente
    void Update()
    {
        StateMachine.CurrentState.LogicUpdate();

        CurrentVelocity = RB.velocity; // Guardamos la velocidad del rigidbody2D al inicio del frame

        // Actualiza el contador de puntos y el HUD
        scoreController.UpdateScoreDisplay(score);

        // Carga los datos del cargador y munición
        // TODO Llamar a estas funciones solo cuando haga falta cambiar los valores, no en cada frame (Con la implementación del sistema de armas)
        hudController.SetCurrentClipAmmo(WeaponComponent.currentBulletsInMagazine);
        hudController.SetCurrentClipAmmount(WeaponComponent.currentClipAmount);

        hudController.SetAidKitAmount(currAidKitAmount);

        hudController.SetCurrentHealthOnHealthBar(currHealth);
    }

    // En cada Update llamamos al PhysicsUpdate() del estado correspondiente
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickableItem"))
        {
            if (character.Inventory.AddItem(collision.GetComponent<PickableItem>().GetPickableItem()))
            {
                Destroy(collision.gameObject);
            }
        }
        else if (collision.CompareTag("FirstAidKit") && currAidKitAmount < playerData.maxAidKits)
        {
            // aumenta la cantidad en 1 e invoca el metodo de recoleccion del item
            currAidKitAmount++;
            collision.GetComponent<PickableController>().PickupItem(this);
        }
        else if (collision.CompareTag("AmmoBox") && WeaponComponent.currentClipAmount < WeaponComponent.weaponData.clipMaxAmmount)
        {
            WeaponComponent.currentClipAmount++;
            collision.GetComponent<PickableController>().PickupItem(this);
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
        //playerInput.
    }

    private void OnDisable()
    {
        // Desactivamos el mapa de acciones
        //playerInput.Gameplay.Disable();
    }
    
    #endregion

    #region Movement & orientation

    // Metodo para modificar la velocidad de movimiento en ambas direcciones, horizontal y vertical
    public void SetVelocity(Vector2 velocity)
    {
        workspaceVelocity.Set(velocity.x, velocity.y);
        RB.velocity = workspaceVelocity; //Actualziamos la velocidad del player
        CurrentVelocity = workspaceVelocity; //Actualizamos la velocidad actual
    }

    // Metodo para parar al jugador
    public void StopPlayer()
    {
        SetVelocity(new Vector2(0.0f, 0.0f));
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

    #region Gameplay methods

    public void IncreseScore(int scoreIncrese)
    {
        score += scoreIncrese;
        //Debug.Log("Score: " + score);
    }

    public void Damage(float amount)
    {
        currHealth -= amount;

        // Si la vida llega a 0 muere
        if (currHealth < 0 && !isDead)
        {
            StateMachine.ChangeState(DeadState);
            isDead = true;
            gameplayManager.ActivateDeadFrame();
        }
    }

    #endregion

    #region Heal methods

    // metodo para curar al jugador
    public void HealPlayer()
    {
        if (!GameplayManager.gameIsPaused && currHealth < playerData.maxHealthBase && currAidKitAmount > 0 && !isHealing)
        {
            isHealing = true;
            StartCoroutine(HealingBoolControl(playerData.healTime));
            InstantiateHealEffect();
            currAidKitAmount--;
            playerAudioSource.PlayOneShot(playerData.healSound);

            if (playerData.maxHealthBase - currHealth <= playerData.maxHealthRestoredAidKit)
            {
                currHealth = playerData.maxHealthBase;
            }
            else
            {
                currHealth += playerData.maxHealthRestoredAidKit;
            }
        }
    }

    // coroutine para controlar el tiempo de animación de la curación
    public IEnumerator HealingBoolControl(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        isHealing = false;
    }

    // Methodo para instanciar el efecto de curación
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

    #endregion

    #region DeadState Methods

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

    #region Weapons

    public void PrimaryWeapon()
    {
        Anim.runtimeAnimatorController = playerData.primaryWeapon;
    }

    public void SecondaryWeapon()
    {
        Anim.runtimeAnimatorController = playerData.secondaryWeapon;
    }

    #endregion

    #region PlayerPrefs

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
}
