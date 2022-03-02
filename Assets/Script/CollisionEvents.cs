using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvents : MonoBehaviour
{
    public UnityEvent<Collision> Collided = new UnityEvent<Collision>();

    void OnCollisionEnter(Collision collision)
    {
        Collided.Invoke(collision);
    }
}
