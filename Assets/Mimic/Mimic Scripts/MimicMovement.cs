using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicMovement : MonoBehaviour
{

    [Header ("References")]
    public CollisionEvents collisionEvents;
    public Collider attackBox;
    public Animator anim;
    public Rigidbody rb;
    MimicAI mimicAI;


    [Header ("Movement")]
    public float runSpeed;
    public float rotationSpeed;
    float animSpeed;
    public Vector3 direction;

    [Header("Attacking")]
    public float pushForceForwards;
    public float pushForceUpwards;
    public int attackState = Animator.StringToHash("Chasing");


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mimicAI = GetComponent<MimicAI>();
    }

    void Attack()
    {
        attackBox.gameObject.SetActive(true);
    }

    void Recover()
    {
        attackBox.gameObject.SetActive(false);
    }

    void Update()
    {
        switch (mimicAI.state)
        {
            case MimicAI.States.None:
                break;
            case MimicAI.States.Idle:
                anim.SetBool("Moving", false);
                break;
            case MimicAI.States.Chasing:
                animSpeed = rb.velocity.magnitude / runSpeed;
                anim.SetBool("Moving", true);
                break;
            default:
                break;
        }

    }

    void FixedUpdate()
    {

        if (direction != Vector3.zero)
        {

            var forwardVector = transform.forward * runSpeed;

            rb.velocity = new Vector3(forwardVector.x, rb.velocity.y, forwardVector.z);
            var goalRotation = Quaternion.LookRotation(direction);

            float angleA = Mathf.Atan2(transform.forward.x, transform.forward.z) * Mathf.Rad2Deg;
            float AngleB = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angleDiff = Mathf.DeltaAngle(angleA, AngleB);

            if (!(angleDiff > -5 && angleDiff < 5))
            {
                float step = rotationSpeed * Time.deltaTime;
                rb.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, step);
            }
        }

    }
}
