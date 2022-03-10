using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tapeScore : MonoBehaviour
{
    public AudioSource collectSound;
    public Animator tapeAnim;
    public string Collected = "collected";
    private void Start()
    {
        collectSound = GetComponent<AudioSource>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Player")
        {
            collectibleSystem.instance.IncreaseScore();
            tapeAnim.Play("collected", 0, 0.0f);
            collectSound.Play();
            Object.Destroy(gameObject, 2.0f);
        }
    }
}
