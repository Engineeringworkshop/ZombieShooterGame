using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestZombieDummyController : MonoBehaviour, IDamageable
{
    //Datos

    public float maxHealth;

    // Variables:

    private float health;

    private void Start()
    {
        health = maxHealth;
    }

    public void Damage(float amount)
    {
        if (health - amount <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            health -= amount;

            Debug.Log("DummyZombie health: " + health);
        }
    }
}
