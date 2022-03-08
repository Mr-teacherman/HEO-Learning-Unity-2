using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EZCameraShake;

public class Character : MonoBehaviour
{
    public Rigidbody rb;
    public CapsuleCollider capCollider;
    public float groundedOffset;
    public float speed = 1f;
    public float airSpeed = 1f;
    public float rotationSpeed = 1f;
    public float kickbackForward, kickbackUpward;
    public float jumpForce;
    public float breakMLP;
    public Animator anim;
    public Vector3 direction;
    public CollisionEvents collisionEvents;
    public int attackState = Animator.StringToHash("Attacking");
    public Collider AttackBox;
    RaycastHit[] results = new RaycastHit[10];
    public LayerMask groundedLayers;
    public PhysicMaterial FullFric, NoFric;
    bool triggerJump;
    private void Start()
    {
        capCollider = GetComponent<CapsuleCollider>();
        collisionEvents.Collided.AddListener(OnCollision);
    }

    void OnCollision(Collision collision)
    {
        if (collision.transform.root == transform.root)
        {
            return;
        }

        Debug.Log("On collision");

        var currentBaseState = anim.GetCurrentAnimatorStateInfo(0);

        if (currentBaseState.shortNameHash == attackState)
        {
            Debug.Log("in the if statement");
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
        var currentSpeed = rb.velocity.magnitude / speed;
        anim.SetFloat("Speed", currentSpeed);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            triggerJump = true;
        }
    }

    public void DoAttack()
    {
        Debug.Log("Do attack");
        var currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
        if (currentBaseState.shortNameHash != attackState)
        {
            anim.SetTrigger("AttackTrigger");
            rb.rotation = (direction != Vector3.zero) ? Quaternion.LookRotation(direction) : Quaternion.LookRotation(transform.forward);
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    void FixedUpdate()
    {
     
        var numberOfHits = Physics.SphereCastNonAlloc(transform.position + Vector3.up, capCollider.radius,Vector3.down, results,(capCollider.height / 2) - capCollider.radius + groundedOffset, groundedLayers);

        var grounded = (numberOfHits > 0);
        anim.SetBool("Grounded",grounded); 
        capCollider.material = grounded ? FullFric : NoFric;

        var currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
        if (currentBaseState.shortNameHash == attackState) 
            return;
        {
            var targetRotation = (direction != Vector3.zero) ? Quaternion.LookRotation(direction) : Quaternion.LookRotation(transform.forward);
            var angleA = Mathf.Atan2(transform.forward.x, transform.forward.z) * Mathf.Rad2Deg;
            var angleB = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            // get the signed difference in these angles
            var angleDiff = Mathf.DeltaAngle(angleA, angleB);
            if (!(angleDiff > -5 && angleDiff < 5))
            {
                var step = rotationSpeed * Time.deltaTime;
                rb.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
            }
        }

        

        if (grounded)
        {
            if (direction != Vector3.zero)
            {
                var forwardVector = transform.forward * speed;
                rb.velocity = new Vector3(forwardVector.x, rb.velocity.y, forwardVector.z);
            }

            if (triggerJump)
            {
                anim.SetTrigger("JumpTrigger");
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
                Debug.Log("DoJump");
            }
        }
        else
        {
            var velocityNoY = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            var targetRotation = (direction != Vector3.zero) ? Quaternion.LookRotation(direction) : Quaternion.LookRotation(transform.forward);
            var angleA = Mathf.Atan2(transform.forward.x, transform.forward.z) * Mathf.Rad2Deg;
            var angleB = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            // get the signed difference in these angles
            var angleDiff = Mathf.DeltaAngle(angleA, angleB);            
            if (velocityNoY.magnitude < speed*1.5f || !(angleDiff > -45 && angleDiff < 45))
            {
                rb.AddForce(new Vector3(direction.x, 0, direction.z) * airSpeed, ForceMode.Force);
            }
            if (direction != Vector3.zero)
            {
                var breakSpeed = new Vector3(rb.velocity.x, 0, rb.velocity.z) * breakMLP;
                rb.velocity = new Vector3(breakSpeed.x, rb.velocity.y, breakSpeed.z);
            }
        }
        triggerJump = false;
    }
}
