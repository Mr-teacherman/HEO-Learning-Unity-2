using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicDeathEffect : MonoBehaviour
{
    public Health health;
    public GameObject deathEffect;

    private bool triggered;

    //This script is kinda silly, but I didn't want to modify the health script.
    //This way the Mimic is easier to import into our group project.
    //That's why it wastefully checks stuff on every frame instead of the Health script calling it.

    private void Awake()
    {
        health = GetComponent<Health>();
        
    }

    private void Update()
    {
        if (health.currentHealth <= 0 && triggered == false) 
        {
            triggered = true;
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
    }
}
