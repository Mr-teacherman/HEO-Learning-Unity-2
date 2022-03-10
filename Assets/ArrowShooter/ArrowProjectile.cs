using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    Rigidbody rb;
    CollisionEvents collisionEvent;

    [HideInInspector] public float damage;
    [HideInInspector] public float lifeTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collisionEvent = GetComponent<CollisionEvents>();

        collisionEvent.Collided.AddListener(OnCollision);

        StartCoroutine(Destroy(lifeTime));
    }

    void OnCollision(Collision collision)
    {
        if (collision.transform.root == transform.root) return;

        var attack = new Attack();
        attack.damage = damage;
        attack.collision = collision;
        attack.attacker = transform;
        attack.kickbackForward = (Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z) - rb.velocity.y) / 4;
        attack.kickbackUpward = rb.velocity.y / 4;
        var attackable = collision.gameObject.GetComponent<IAttackable>();
        attackable.SendAttack(attack);
        Destroy(gameObject);
    }

    IEnumerator Destroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
