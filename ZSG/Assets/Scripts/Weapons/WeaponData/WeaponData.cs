using UnityEngine;

// En esta clase declararemos todas los atributos del arma
// Esta clase heredará de ScriptableObject para poder crear asset del script, nos permitirá crear los objetos de "base de datos"

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data")]
public class WeaponData : ScriptableObject
{
    // Configuración de las balas
    [Header("Bullet")]
    public float basePrecision = 5f; // Angulo máximo de dispersión
    public float rateOfFire = 1f; // Tiempo mínimo entre disparos
    public float bulletLifeTime = 8f; // Tiempo que existirá la bala hasta que desaparezca

    // Configuración de los cargadores
    [Header("Magacine")]
    public int clipCapacity = 30; // Capacidad del cargador antes de tener que recargar
    public float clipReloadTime = 5f; // tiempo de recarga

    public int clipStartAmmount = 3; // cargadores al empezar el juego
    public int clipMaxAmmount = 8; // Capacidad máxima de cargadores de cada arma

    // Configuracion multimedia
    [Header("Effects")]
    public GameObject MuzzleFlashEffect;
    public ParticleSystem AmmoClipEmpty;
    public ParticleSystem AmmoClipNotEmpty;

    // Sonidos
    [Header("Sonidos")]
    public AudioClip fireWeaponSound;
}

