using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IAttackable
{
    public Health health;
    public DamageIndicator dmgIndicatorPrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendAttack(Attack attack)
    {
        //I got an attack sent to me
        attack.collision.rigidbody.AddForce(attack.attacker.forward * attack.kickbackForward + Vector3.up * attack.kickbackUpward, ForceMode.VelocityChange);
        attack.collision.rigidbody.angularVelocity = new Vector3(5f, 0f, 5f);
        health.currentHealth -= 1f;
        var dmgIndicator = Instantiate(dmgIndicatorPrefab);
        dmgIndicator.SetAttack(attack);
    }
}
