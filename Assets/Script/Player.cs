using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EZCameraShake;

public class Player : MonoBehaviour
{
    bool attacking = false;
    public Rigidbody rb;
    public float speed = 1f;
    public float rotationSpeed = 1f;
    public float kickbackForward, kickbackUpward;
    public Animator anim;
    float horizontal;
    float vertical;
    Vector3 direction;
    public CollisionEvents collisionEvents;
    static int kickState = Animator.StringToHash("BencaoKick");
    static int runningState = Animator.StringToHash("Running");
    static int idleState = Animator.StringToHash("Idle");
    public Collider AttackBox;
    private void Start()
    {
        collisionEvents.Collided.AddListener(OnCollision);
    }

    void OnCollision(Collision collision)
    {
        var currentBaseState = anim.GetCurrentAnimatorStateInfo(0);

        if (currentBaseState.shortNameHash == kickState)
        {
            var attack = new Attack();
            attack.damage = 1f;
            attack.collision = collision;
            attack.attacker = transform;
            attack.kickbackForward = kickbackForward;
            attack.kickbackUpward = kickbackUpward;
            var attackable = collision.gameObject.GetComponent<IAttackable>();
            attackable.SendAttack(attack);
            CameraShaker.Instance.ShakeOnce(4f, 4f, 0.4f, 0.4f);
            Debug.Log("we hit the box");
        }
    }
    void Attack()
    {
        AttackBox.gameObject.SetActive(true);
        Debug.Log("Attack event");
    }

    void Recover()
    {
        AttackBox.gameObject.SetActive(false);
        Debug.Log("Recover event");
    }
    // Update is called once per frame
    void Update()
    {
        var currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
        if (Input.GetKeyDown(KeyCode.J) && currentBaseState.shortNameHash != kickState)
        {
            anim.SetTrigger("KickTrigger");
        }
        vertical = 0f; // = Input.GetAxis("Y");
        horizontal = 0f; // = Input.GetAxis("X");
        if (Input.GetKey(KeyCode.W)) vertical += 1f;
        if (Input.GetKey(KeyCode.S)) vertical += -1f;
        if (Input.GetKey(KeyCode.A)) horizontal += -1f;
        if (Input.GetKey(KeyCode.D)) horizontal += 1f;
        direction = new Vector3(horizontal, 0, vertical);
        direction = Quaternion.AngleAxis(-45, Vector3.up) * direction;
        anim.SetBool("Running", direction != Vector3.zero);
    }

    //only happens every 20 ms
    void FixedUpdate()
    {
        if (direction != Vector3.zero)
        {
            rb.velocity = transform.forward * speed;
            //var currentRotation = Quaternion.LookRotation(transform.forward);
            var targetRotation = Quaternion.LookRotation(direction);
            var angleA = Mathf.Atan2(transform.forward.x, transform.forward.z)  * Mathf.Rad2Deg;
            var angleB = Mathf.Atan2(direction.x, direction.z)  * Mathf.Rad2Deg;
            // get the signed difference in these angles
            var angleDiff = Mathf.DeltaAngle(angleA, angleB);
            if (!(angleDiff > -5 && angleDiff < 5))
            {
                var step = rotationSpeed * Time.deltaTime;
                rb.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        //Vector3 direction = goalRotation * Vector3.forward;
        //Debug.DrawRay(transform.position, direction, Color.red,0.02f);
    }
}
