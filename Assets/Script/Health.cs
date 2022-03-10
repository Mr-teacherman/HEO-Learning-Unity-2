using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public float startingHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " has died");
        Destroy(gameObject);
    }
}
