using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFlameTrapAI : MonoBehaviour
{

    public bool isShooting;
    Animator animator;
    ParticleSystem flameParticleSystem;
    public GameObject attackHitbox;

    public float timeIdle; //how long idle before next attack
    public float timeAttacking; //how long to attack before going back to idle

    private void Awake()
    {
        flameParticleSystem = GetComponentInChildren<ParticleSystem>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        //sets values to default instead of freezing Unity in case numbers get reset to 0

        if (timeIdle < 0.1)
        {
            timeIdle = 2f;
            Debug.Log("Idle time for" + gameObject.name + "less than 0.1f. Reset to 2f to prevent Unity freezing.");
        }
        if (timeAttacking < 0.1f)
        {
            timeAttacking = 3;
            Debug.Log("Attack time for" + gameObject.name + "less than 0.1f Reset to 3f to prevent Unity freezing.");
        }

        


        // (I added some notes about Coroutines for my fellow students.)

        StartCoroutine(AICycle()); //You need to start the Coroutine once. (if you run this multiple times, you get multiple copies of it, be careful.)
    }

    IEnumerator AICycle()
    {

        while (true)
        {
            animator.SetBool("Firing", false); //Trap is idle
            attackHitbox.SetActive(false);
            yield return new WaitForSeconds(timeIdle); //It is idle for this long

            animator.SetBool("Firing", true); //Trap is firing flames
            yield return new WaitForSeconds(0.5f); //Better synchs box with flame anim
            attackHitbox.SetActive(true);

            yield return new WaitForSeconds(timeAttacking); //It keeps firing for this long before going back the the begining of this loop
            // Note: This will probably freeze Unity if you set the timers to 0. It tries to run the loop infinitely fast.
        }

    }

    void StartFire()
    {
        flameParticleSystem.Play();
    }

    void StopFire()
    {
        flameParticleSystem.Stop();
    }

}