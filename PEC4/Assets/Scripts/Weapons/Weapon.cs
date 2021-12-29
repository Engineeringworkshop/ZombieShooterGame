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

    // Atributos
    int currentBulletsInMagazine; // Guarda la cantidad de balas en el cargador

    float prevShootTime = 0f;

    private void Start()
    {
        // el arma se inicia con el cargador lleno
        currentBulletsInMagazine = weaponData.magacineCapacity; 
    }
    

    void Update()
    {
        // Si se ha presionado la tecla de disparo
        if (player.playerInput.Gameplay.Shoot.ReadValue<float>() > 0.5f)
        {
            ShootBullet();
        }
        // Si se ha presionado la tecla de recarga
        else if (player.playerInput.Gameplay.ReloadWeapon.ReadValue<float>() > 0.5f)
        {
            ReloadWeapon();
        }
    }

    // Metodo para instanciar una bala
    void ShootBullet()
    {
        // Crea una bala si ha pasado el tiempo entre disparos
        if (currentBulletsInMagazine > 0 && Time.time >= prevShootTime + weaponData.rateOfFire)
        {
            // Instanciamos la bala
            var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

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
        // TODO instanciar efecto de cargador vacio en el suelo
    }

    // Coroutine para retrasar la recarga segun el tiempo que tarda en recargar, as� no podremos disparar antes de tiempo
    IEnumerator ReloadWeaponTimer()
    {
        yield return new WaitForSecondsRealtime(weaponData.magacineReloadTime);
        currentBulletsInMagazine = weaponData.magacineCapacity;
    }

    // Metodo para girar la bala en la direcci�n de disparo teniendo en cuenta la dispersi�n del arma
    void SetBulletOnDirection(GameObject bullet)
    {
        var shootPrecision = weaponData.basePrecision; // TODO a�adir la dispersi�n por el nivel del jugador
        var shootAngle = Random.Range(-shootPrecision, shootPrecision);
        bullet.transform.Rotate(0f, 0f, shootAngle);
    }
}
