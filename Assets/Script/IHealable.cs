using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealable
{
    void SendHeal(Heal heal);
}
public class Heal
{
    public Collision collision;
    public float healthPoints;
    
}