using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatGetPotato : MonoBehaviour
{
    [Header("References")]
    public Transform paw;
    public Transform rat;

    [Header("Settings")]
    public float speed;
    public float thinkingTime;
    public float potatoGrabbingSlowness;

    public bool hasPotato;
    bool carryingPotato;
    Vector3 ratPawHome;


    void Start()
    {
        hasPotato = true;
        carryingPotato = false;
        ratPawHome = paw.position;
    }

    void Update()
    {
        if (hasPotato == false)
        {
            // gotta look for potato!
            Transform potato = GameObject.Find("potatis(Clone)").transform;
            float step = speed * Time.deltaTime;
            if (transform.position != potato.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, potato.position, step);
            }
            else
            {
                Invoke(nameof(TookPotato), potatoGrabbingSlowness);
                hasPotato = true;
            }
        }
        if (carryingPotato == true)
        {
            // potato in mah grasp!
            GameObject potato = GameObject.Find("potatis(Clone)");
            if (transform.position == ratPawHome)
            {
                // has potato, all good!
                carryingPotato = false;
                Destroy(potato);
                rat.GetComponent<Rat>().HasPotatoBack();

            }
            else
            {
                // bringing potato back!
                potato.transform.position = transform.position;
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, ratPawHome, step);
            }

        }
    }

    public void OopsIThrewPotato()
    {
        CancelInvoke("PotatoThrown");
        Invoke("PotatoThrown", thinkingTime);
        //pondering potato, where it go?
    }

    void PotatoThrown()
    {
        hasPotato = false;
        //oh no rat has potato no more :(
    }

    private void TookPotato()
    {
        carryingPotato = true;
        // tiihiii potato come back to me! <3
    }
}