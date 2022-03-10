using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{
    public UnityEvent AttackEvent = new UnityEvent();
    public UnityEvent RecoverEvent = new UnityEvent();

    void Attack()
    {
        AttackEvent.Invoke();
    }

    void Recover()
    {
        RecoverEvent.Invoke();
    }

}
