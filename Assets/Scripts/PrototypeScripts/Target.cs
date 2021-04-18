using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IHittable
{
    public int health = 6;

    public int Health => throw new System.NotImplementedException();

    public void TakeDamage(int amount)
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

    public void GetHit(WeaponItemSO weapon, Vector3 hitpoint)
    {
        throw new System.NotImplementedException();
    }

    public void GetHit(WeaponItemSO weapon)
    {
        throw new System.NotImplementedException();
    }

    public void GetAttacked(int amount)
    {
        TakeDamage(amount);
    }
}
