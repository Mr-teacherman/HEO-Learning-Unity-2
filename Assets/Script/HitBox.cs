using System.Collections;
using System.Collections.Generic;
using UnityEngine;







public class HitBox : MonoBehaviour
{

    public int health = 100;
    private int CurrentHealth;

    private void Start()
    {
        CurrentHealth = health;
        print("start" + CurrentHealth);
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FlyingEnemy")
        {
            CurrentHealth -= 10;
            print("Enter" + CurrentHealth);
        }
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "FlyingEnemy")
        {
            CurrentHealth -= 1;
            print("stay" + CurrentHealth);
        }
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "FlyingEnemy")
        {
            print("exit" + CurrentHealth);
        }
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
