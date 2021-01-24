using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 6f;

    public void TakeDamage(float amount)
    {
        //Debug.Log(amount);
        health -= amount;
        //Debug.Log(health);
        if(health <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
