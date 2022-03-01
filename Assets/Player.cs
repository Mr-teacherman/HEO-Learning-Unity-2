using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 1f;
    public float rotationSpeed = 1f;
    public Animator anim;
    float horizontal;
    float vertical;
    Vector3 direction;

    // Update is called once per frame
    void Update()
    {
        vertical = 0f;
        horizontal = 0f;
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
        Debug.DrawRay(transform.position, direction, Color.red,0.02f);
    }
}
