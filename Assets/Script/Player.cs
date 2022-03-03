using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour, IAttackable
{
    public Health health;
    public DamageIndicator dmgIndicatorPrefab;
    public Character character;
    float horizontal;
    float vertical;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.J))
        {
            character.DoAttack();
        }
        vertical = 0f; // = Input.GetAxis("Y");
        horizontal = 0f; // = Input.GetAxis("X");
        if (Input.GetKey(KeyCode.W)) vertical += 1f;
        if (Input.GetKey(KeyCode.S)) vertical += -1f;
        if (Input.GetKey(KeyCode.A)) horizontal += -1f;
        if (Input.GetKey(KeyCode.D)) horizontal += 1f;
        character.direction = new Vector3(horizontal, 0, vertical);
        character.direction = Quaternion.AngleAxis(-45, Vector3.up) * character.direction;
        
    }
    public void SendAttack(Attack attack)
    {
        //I got an attack sent to me
        attack.collision.rigidbody.AddForce(attack.attacker.forward * attack.kickbackForward + Vector3.up * attack.kickbackUpward, ForceMode.VelocityChange);
        health.currentHealth -= 1f;
        var dmgIndicator = Instantiate(dmgIndicatorPrefab);
        dmgIndicator.SetAttack(attack);
    }

    private void OnDrawGizmos()
    {
        //Vector3 direction = goalRotation * Vector3.forward;
        //Debug.DrawRay(transform.position, direction, Color.red,0.02f);
    }
}
