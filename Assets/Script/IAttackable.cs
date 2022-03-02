using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable 
{
    void SendAttack(Attack attack);
}

public class Attack
{
    public float damage;
    public Collision collision;
    public Transform attacker;
    public float kickbackForward;
    public float kickbackUpward;
}
