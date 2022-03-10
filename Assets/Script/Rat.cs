using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    [Header("References")]
    public Transform rat;
    public Transform paw;
    public Transform throwPoint;
    public GameObject objectToThrow;
    public Transform player;

    [Header("Settings")]
    public float throwCooldown;
    public bool throwAtPlayer;

    [Header("Throwing")]
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;
    float cooldown = 0;

    private void Start()
    {
        if (throwAtPlayer == false)
        {
            readyToThrow = true;
        }
        else
        {
            readyToThrow = false;
        }
       
    }

    private void Update()
    {
        if (throwAtPlayer == true)
        {
            if (Vector3.Distance(rat.position, player.position) < 10)
            {
                transform.LookAt(player);

                if (cooldown < throwCooldown)
                {
                    cooldown += Time.deltaTime;
                }

                else
                {
                    Throw();
                    cooldown = 0;
                }
            }
            else
            {
                readyToThrow = false;
            }
            //rat follow player lul
            //player is not too far
            //ready to throw = true
        }
        if (throwAtPlayer == false && readyToThrow)
        {
            Throw();
        }
    }


        private void Throw()
    {
        readyToThrow = false;

        // take potato from ratpouch tm
        GameObject projectile = Instantiate(objectToThrow, throwPoint.position, rat.rotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // where to throw?
        Vector3 forceDirection = rat.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(rat.position, rat.forward, out hit, 500f))
        {
            forceDirection = (hit.point - throwPoint.position).normalized;
        }

        // HIIIIOP!
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        if (throwAtPlayer == false)
        {
            // potato AWAYYYY
            paw.GetComponent<RatGetPotato>().OopsIThrewPotato();
        }


        if (throwAtPlayer == true)
        {
            // throwing is hard work
            Invoke(nameof(ResetThrow), throwCooldown);
        }

    }

    public void HasPotatoBack()
    {
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
