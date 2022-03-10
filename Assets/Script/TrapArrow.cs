using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapArrow : MonoBehaviour
{
    public Vector3 arrowDirection;
    public float speed;
    public float arrowDestroy;
    public float arrowDamage;
    AudioSource ads;
    public AudioClip audioArrow;
    ParticleSystem particle;
    public ParticleCollisionEvent particleCollision;


    public Health health;
    // public TrapPlatform trapPlatform;
    public GameObject trapPlatformPrefab;
    private float arrowEnd;

    private void Start()
    {
        ads = GetComponent<AudioSource>();
        particle = GetComponent<ParticleSystem>();
        //arrowEnd = trapPlatformPrefab.transform.position.x + arrowDestroy;
        // arrowEnd = trapPlatform.transform.position.x + arrowDestroy;
    }

    private void Update()
    {
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Sounds();
        var attack = new Attack();
        attack.damage = 1f;
        attack.collision = collision;
        attack.attacker = transform;
        attack.kickbackForward = 0;
        attack.kickbackUpward = 0;
        var attackable = collision.gameObject.GetComponent<IAttackable>();
        attackable.SendAttack(attack);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {

        //health.currentHealth -= arrowDamage;
        Sounds();
        Destroy(gameObject);
        
    }

    private void ArrowDestroy()
    {
        if (transform.position.x > arrowEnd)
        {
            Sounds();
            Destroy(gameObject);
        }

    }

    public void Sounds()
    {
        ads.clip = audioArrow;
        ads.PlayOneShot(audioArrow);
    }

    public void ArrowParticles()
    {
       
    }
}
