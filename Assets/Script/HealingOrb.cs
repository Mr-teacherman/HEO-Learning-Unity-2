using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealingOrb : MonoBehaviour
{
    public CollisionEvents collisionEvents;

    private void Start()
    {
        collisionEvents.Collided.AddListener(OnCollision);
    }

    void OnCollision(Collision collision)
    {
        
        var healable = collision.gameObject.GetComponent<IHealable>();
        var heal = new Heal();
        heal.healthPoints = 1f;  
        healable.SendHeal(heal);
        Debug.Log("Orb collided player");
        Destroy(gameObject);
    }

    
}
