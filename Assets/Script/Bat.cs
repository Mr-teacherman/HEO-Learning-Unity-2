using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{

    private Animator anim;
    

    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("BatAttack", false);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetBool("BatAttack", true);
            
        }

        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetBool("BatAttack", false);
            
        }
        
    }


    

}
