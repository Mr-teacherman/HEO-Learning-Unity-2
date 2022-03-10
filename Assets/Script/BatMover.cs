using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BatMover : MonoBehaviour
{
    
    public GameObject myTarget;

    private bool Attack;
   
    // Start is called before the first frame update
    void Start()
    {
        Attack = false;
        
    }

    // Update is called once per frame
    void Update()
    {
     
        GetComponent<NavMeshAgent>().SetDestination(myTarget.transform.position);
        

   


    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == ("Player") && Attack == false)
        {
            Attack = true;
            print(Attack);
        }
        if (Attack == true)
        {
            GetComponent<NavMeshAgent>().speed = 10;

        }
            


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == ("Player") && Attack == true)
        {
            GetComponent<NavMeshAgent>().speed = 5;
            Attack = false;
            print(Attack);
        }



    }
    



}
