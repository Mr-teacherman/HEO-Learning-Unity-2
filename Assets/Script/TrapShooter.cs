using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrapShooter : MonoBehaviour
{
    public UnityEvent triggerTrap;
    public GameObject trapPlatform;

    public GameObject trapArrowPrefab;
    AudioSource ads;
    public AudioClip audioShooter;

    private void Start()
    {
        ads = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (trapPlatform == true)
        {
            triggerTrap.Invoke();
            Shoot();
        }
    }
    
    public void Shoot()
    {
        Sounds();
        Instantiate(trapArrowPrefab, transform.position, transform.rotation);
        Debug.Log("shooting");
    }

    public void Sounds()
    {
        ads.clip = audioShooter;
        ads.PlayOneShot(audioShooter);
    }
}
