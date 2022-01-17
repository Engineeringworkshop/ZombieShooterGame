using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    // Data
    // TODO base de datos para balas.
    public float speed = 20f;

    public float damageMin = 5;
    public float damageMax = 10;

    public float maxPower = 100;

    public ParticleSystem wallImpactEffect;

    // Atributos

    public float currPower; // Capacidad de hacer daño de la bala

    // Componentes

    private Rigidbody2D rb;

    #region Unity Callback methods
    // Unity Callback methods

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.right * speed;

        currPower = maxPower;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // cogemos la componente interfade IDamageable del objeto impactado
        IDamageable damageable = collision.GetComponent<IDamageable>();

        // si hemos podeido obtener la componente, es decir, el objeto es dañable
        if (damageable != null)
        {
            // calcula el daño del impacto y lo realiza
            damageable.Damage(ImpactDamage());
        }
        else if (collision.CompareTag("Walls"))
        {
            Instantiate(wallImpactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }

    #endregion

    // Metodo para calcular el daño del impacto
    private float ImpactDamage()
    {
        float damage = Random.Range(damageMin, damageMax);

        // si no queda poder suficiente en la bala, devolvera el poder actual y destruirá la bala
        if (currPower < damage)
        {
            Destroy(gameObject);
            return currPower;
        }
        // si queda suficiente poder en la bala hará el daño total
        else
        {
            currPower -= damageMax;
            return damage;
        }
    }

}
