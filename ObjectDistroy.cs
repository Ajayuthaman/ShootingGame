using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDistroy : MonoBehaviour
{
    public float objectHealth= 30f;

    public void objectHitDamage(float amount)
    {
        objectHealth -= amount;

        if(objectHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
