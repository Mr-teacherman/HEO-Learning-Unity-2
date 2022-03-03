using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour, IAttackable
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
}
