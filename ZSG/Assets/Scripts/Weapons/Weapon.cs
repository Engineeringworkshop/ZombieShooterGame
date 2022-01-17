using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public Player player;
    public GameObject bulletPrefab;
    public WeaponData weaponData;

    [SerializeField] public AudioSource weaponAudioSource;

    [HideInInspector] public bool isReloading;
    [HideInInspector] public bool isShooting;

    // Atributos
    [HideInInspector] public int currentBulletsInMagazine; // Guarda la cantidad de balas en el cargador, se inicializa desde el juagdor con prefs
    [HideInInspector] public int currentClipAmount; //se inicializa desde el juagdor con prefs

    [HideInInspector] float prevShootTime = 0f;


    private void Start()
    {
        isReloading = false;
    }
    

    void Update()
    {
        // Si se ha presionado la tecla de disparo
        if (player.playerInput.Gameplay.Shoot.ReadValue<float>() > 0.5f)
        {
            ShootBullet();
        }
        // Si se ha presionado la tecla de recarga
        else if (player.playerInput.Gameplay.ReloadWeapon.ReadValue<float>() > 0.5f && !isReloading && currentClipAmount > 0 && currentBulletsInMagazine < weaponData.clipCapacity && !player.isHealing)
        {
            isReloading = true;
            currentClipAmount--;
            ReloadWeapon();
        }
    }

    // Metodo para instanciar una bala
    void ShootBullet()
    {
        // Crea una bala si ha pasado el tiempo entre disparos y no est� recargando
        if (currentBulletsInMagazine > 0 && Time.time >= prevShootTime + weaponData.rateOfFire && !isReloading && !player.isHealing)
        {
            // Instanciamos la bala
            var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Instanciamos el effecto de disparo
            var effect = Instantiate(weaponData.MuzzleFlashEffect, firePoint.position, player.transform.rotation);
            effect.transform.parent = transform;

            // reproducimos el sonido
            weaponAudioSource.PlayOneShot(weaponData.fireWeaponSound);

            // Rotamos la bala
            SetBulletOnDirection(bullet);

            // Programamos la destrucci�n de la bala
            StartCoroutine(DestroyBullet(bullet));
            prevShootTime = Time.time;
            currentBulletsInMagazine--;
        }
        else if (currentBulletsInMagazine == 0)
        {
            // TODO ruido de gatillo sin disparo
        }
        
    }

    // Coroutine para auto destruir una bala una vez pasado el tiempo 
    IEnumerator DestroyBullet(GameObject bullet)
    {
        yield return new WaitForSecondsRealtime(weaponData.bulletLifeTime);
        Destroy(bullet);
    }

    // Metodo para recargar el arma
    void ReloadWeapon()
    {
        StartCoroutine(ReloadWeaponTimer());
        if (currentBulletsInMagazine > 0)
        {
            StartCoroutine(InstantiateAmmoClip(weaponData.AmmoClipNotEmpty));
        }
        else
        {
            StartCoroutine(InstantiateAmmoClip(weaponData.AmmoClipEmpty));
        }
    }

    // Metodo para girar la bala en la direcci�n de disparo teniendo en cuenta la dispersi�n del arma
    void SetBulletOnDirection(GameObject bullet)
    {
        var shootPrecision = weaponData.basePrecision; // TODO a�adir la dispersi�n por el nivel del jugador
        var shootAngle = Random.Range(-shootPrecision, shootPrecision);
        bullet.transform.Rotate(0f, 0f, shootAngle);
    }

    // Coroutine para retrasar la recarga segun el tiempo que tarda en recargar, as� no podremos disparar antes de tiempo
    IEnumerator ReloadWeaponTimer()
    {
        yield return new WaitForSecondsRealtime(weaponData.clipReloadTime);
        isReloading = false;
        currentBulletsInMagazine = weaponData.clipCapacity;
    }

    // Coroutine para invocar el cargador con cierto retraso al recargar
    IEnumerator InstantiateAmmoClip(ParticleSystem particleSystem)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Instantiate(particleSystem, firePoint.position, firePoint.rotation);
    }
}