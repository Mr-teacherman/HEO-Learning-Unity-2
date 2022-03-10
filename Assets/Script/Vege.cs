using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vege : MonoBehaviour, IAttackable
{
    public Health health;
    public DamageIndicator dmgIndicatorPrefab;
    public void SendAttack(Attack attack)
    {
        //I got an attack sent to me
        attack.collision.rigidbody.AddForce(attack.attacker.forward * attack.kickbackForward + Vector3.up * attack.kickbackUpward, ForceMode.VelocityChange);
        health.currentHealth -= 1f;
        var dmgIndicator = Instantiate(dmgIndicatorPrefab);
        dmgIndicator.SetAttack(attack);
    }

    public CollisionEvents collisionEvents;
    private float pushForceForward;
    private float pushForceUpward;
    private void Start()
    {
        collisionEvents.Collided.AddListener(OnCollision);
    }

    void OnCollision(Collision collision)
    {
        var attack = new Attack();
        attack.damage = 1f;
        attack.collision = collision;
        attack.attacker = transform;
        attack.kickbackForward = pushForceForward;
        attack.kickbackUpward = pushForceUpward;

        IAttackable attackable = collision.gameObject.GetComponent<IAttackable>();

        if (collision.gameObject.transform.root == transform.root)
            return;

        if (attackable != null)
        {
            attackable.SendAttack(attack);
        }
    }
}
