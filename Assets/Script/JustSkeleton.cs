using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustSkeleton : MonoBehaviour
{
    public Health health;
    public DamageIndicator dmgIndicatorPrefab;

    public void SetAttack(Attack attack)
    {
        attack.collision.rigidbody.AddForce(attack.attacker.forward * attack.kickbackForward + Vector3.up * attack.kickbackUpward, ForceMode.VelocityChange);
        attack.collision.rigidbody.angularVelocity = new Vector3(5f, 0f, 5f);
        health.currentHealth -= 1f;
        var dmgIndicator = Instantiate(dmgIndicatorPrefab);
        dmgIndicator.SetAttack(attack);
    }
}
