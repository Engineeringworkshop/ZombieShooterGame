using UnityEngine;
using UnityEngine.UI;

// En esta clase declararemos todas los atributos que el Player tiene: Ej velocidad de movimiento, resistencia...
// Esta clase heredará de ScriptableObject para poder crear asset del script, nos permitirá crear los objetos de "base de datos"

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data")]

public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;
    public AudioClip walkSound;

    [Header("Health")]
    public float maxHealthBase = 200f;
    public float startHealth = 40f;

    public int startAidKits = 3;
    public int maxAidKits = 5;
    public float maxHealthRestoredAidKit = 40f;
    public float healTime = 5f;

    public ParticleSystem healEffect;
    public AudioClip healSound;

    [Header("Weapon")]
    public AudioClip reloadWeaponSound;

    [Header("Dead")]
    public AudioClip deadSound;
    public ParticleSystem deadEffect;
    public ParticleSystem deadSkull;
}
