using UnityEngine;

// En esta clase declararemos todas los atributos que el Player tiene: Ej velocidad de movimiento, resistencia...
// Esta clase heredará de ScriptableObject para poder crear asset del script, nos permitirá crear los objetos de "base de datos"

[CreateAssetMenu(fileName = "newZombieData", menuName = "Data/Zombie Data")]

public class Zombie1Data : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;
    public LayerMask whatsIsBlockingWalk;

    [Header("Atributes")]
    public float maxHealth = 50;
    public int score = 5;
    public LayerMask whatsIsPlayer;

    [Header("Effects")]
    public ParticleSystem bloodEffect;
    public ParticleSystem deadEffect;

    [Header("Combat")]
    public float rangeDetectionRadius = 3f;
    public float meleeRange = 2f;
    public float minMeleeDamage = 5f;
    public float maxMeleeDamage = 8f;
    public float timeBetweenAttacks = 1f;

    [Header("Sounds")]
    public AudioClip IdleRandomSound;
    public AudioClip AttackSound;
    public AudioClip deadSound;
    public AudioClip PlayerDetectedSound;
}
