using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MimicAI : MonoBehaviour
{
    public enum States
    {
        None,
        Asleep, //allows player to get closer before "waking up"
        Idle,
        Chasing
    }

    private MimicMovement mimicMovement;

    public States state;
    Player player;
    public float detectionRange;
    public float backToIdleRange;
    public float wakeupRange;


    public float sleepTimer; //Makes Mimic go to a "deeper sleep" after a long time idle
    private float currentSleepTimer;


    private void Awake()
    {
        mimicMovement = GetComponent<MimicMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        currentSleepTimer = sleepTimer;
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case States.None:
                break;
            case States.Asleep:
                if (Vector3.Distance(transform.position, player.transform.position) <= wakeupRange)
                {
                    state = States.Chasing;
                    mimicMovement.direction = Vector3.zero;
                    return;
                }
                break;

            case States.Idle:

                if (Vector3.Distance(transform.position, player.transform.position) <= detectionRange)
                {
                    state = States.Chasing;
                    mimicMovement.direction = Vector3.zero;
                    return;
                }

                if (currentSleepTimer > 0)
                {
                    currentSleepTimer -= Time.deltaTime;
                }
                if (currentSleepTimer <= 0)
                {
                    state = States.Asleep;
                    currentSleepTimer = sleepTimer;
                }

                break;
            case States.Chasing:
                //////
                var direction = (player.transform.position - transform.position).normalized;
                direction = new Vector3(direction.x, 0, direction.z);
                //var angleA = Mathf.Atan2(transform.forward.x, transform.forward.z) * Mathf.Rad2Deg;
                //var angleB = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                // get the signed difference in these angles
                if (Vector3.Distance(transform.position, player.transform.position) >= backToIdleRange)
                {
                    state = States.Idle;
                    mimicMovement.direction = Vector3.zero;
                    return;
                }
                mimicMovement.direction = direction;
                break;
                /////////
            default:
                break;
        }

    }
}
