using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSami : MonoBehaviour
{
    
    AudioSource AudioPlay;
    // Start is called before the first frame update

    private void Start()
    {
        AudioPlay = GetComponent<AudioSource>();
    }

    private void FlyingEnemyattack()
    {
        AudioPlay.Play();
    }
}
