using UnityEngine;

// En esta clase declararemos todas los atributos del arma
// Esta clase heredar� de ScriptableObject para poder crear asset del script, nos permitir� crear los objetos de "base de datos"

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data")]
public class WeaponData : ScriptableObject
{
    // Configuraci�n de las balas
    [Header("Bullet")]
    public float basePrecision = 5f; // Angulo m�ximo de dispersi�n
    public float rateOfFire = 1f; // Tiempo m�nimo entre disparos
    public float bulletLifeTime = 8f; // Tiempo que existir� la bala hasta que desaparezca

    // Configuraci�n de los cargadores
    [Header("Magacine")]
    public int magacineCapacity = 3; // Capacidad del cargador antes de tener que recargar
    public float magacineReloadTime = 5f; // tiempo de recarga

    // Configuracion multimedia
    public GameObject MuzzleFlashEffect;
    public ParticleSystem AmmoClipEmpty;
    public ParticleSystem AmmoClipNotEmpty;
}

