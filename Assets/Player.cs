using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 1f;
    public Animator anim;
    float horizontal;
    float vertical;
    Vector3 direction;
    Quaternion goalRotation;

    // Update is called once per frame
    void Update()
    {
        vertical = 0f;
        horizontal = 0f;
        if (Input.GetKey(KeyCode.W)) vertical += 1f;
        if (Input.GetKey(KeyCode.S)) vertical += -1f;
        if (Input.GetKey(KeyCode.A)) horizontal += -1f;
        if (Input.GetKey(KeyCode.D)) horizontal += 1f;
        //direction = new Vector3(horizontal, 0, vertical).normalized;
        var rawRotation = Quaternion.Euler(horizontal, 0, vertical);
        goalRotation = rawRotation * Quaternion.AngleAxis(-45, Vector3.up);
        anim.SetBool("Running", direction != Vector3.zero);
    }

    //only happens every 20 ms
    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    //private void OnDrawGizmos()
    //{
    //    var point = transform.position + goalRotation * transform.forward;
    //    Debug.DrawRay(transform.position, point,Color.red,0.02f);
    //}
}
