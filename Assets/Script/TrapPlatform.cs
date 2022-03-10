using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TrapPlatform : MonoBehaviour
{
    public TrapShooter trapShooter;
    AudioSource ads;
    public AudioClip audioPlatform;

    private void Start()
    {
        ads = GetComponent<AudioSource>();
    }
    private void Reset()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hello world");
        if (other.tag=="Player")
        {
            Sounds();
            trapShooter.Shoot();
        }
    }

    public void Sounds()
    {
        ads.clip = audioPlatform;
        ads.PlayOneShot(audioPlatform);
    }
}
