using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic : MonoBehaviour, IAttackable
{

    [Header("Stats")]
    public Health health;
    public float pushForceForward;
    public float pushForceUpward;

    [Header("References")]
    public DamageIndicator damageIndicatorPrefab;
    public CollisionEvents collisionEvents;


    private void Start()
    {
        collisionEvents.Collided.AddListener(OnCollision);
    }

    public void SendAttack(Attack attack) //Despite name, receives attacks
    {
        //I got an attack sent to me
        attack.collision.rigidbody.AddForce(attack.attacker.forward * attack.kickbackForward + Vector3.up * attack.kickbackUpward, ForceMode.VelocityChange);
        health.currentHealth -= 1f;
        var indicator = Instantiate(damageIndicatorPrefab);
        indicator.SetAttack(attack);
    }

    void OnCollision(Collision collision)
    {
        if (collision.gameObject.transform.root == transform.root)
            return;

            var attack = new Attack();
            attack.damage = 1f;
            attack.collision = collision;
            attack.attacker = transform;
            attack.kickbackForward = pushForceForward;
            attack.kickbackUpward = pushForceUpward;

            IAttackable attackable = collision.gameObject.GetComponent<IAttackable>();

            if (attackable != null)
            {
                attackable.SendAttack(attack);
            }
    }
}
