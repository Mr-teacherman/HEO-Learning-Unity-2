using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkeletonAI : MonoBehaviour
{
    public enum States
    {
        None,
        Patrolling,
        Chasing,
        Attacking
    }
    public Character character;
    public List<Transform> waypoints;
    public States state;
    public float detectionRange;
    public float chaseRange;
    public float attackRange;
    Transform targetWaypoint;
    float idleTime;
    Player player;
    bool didAttack;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        state = States.Patrolling;
        targetWaypoint = waypoints.First();
        didAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == States.Patrolling)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < detectionRange)
            {
                state = States.Chasing;
                character.direction = Vector3.zero;
                return;
            }

            if (idleTime > 0)
            {
                character.direction = Vector3.zero;
                idleTime -= Time.deltaTime;
                return;
            }
            var direction = (targetWaypoint.position - transform.position).normalized;
            direction = new Vector3 (direction.x, 0, direction.z);
            character.direction = direction;
            if (Vector3.Distance(transform.position, targetWaypoint.position) < .5f)
            {
                idleTime = 3;

                if (targetWaypoint == waypoints.First())
                {

                    targetWaypoint = waypoints.Last();
                }
                else
                {
                    targetWaypoint = waypoints.First();
                }
            }
        }
        else if (state == States.Chasing)
        {
            var direction = (player.transform.position - transform.position).normalized;
            direction = new Vector3(direction.x, 0, direction.z);
            var angleA = Mathf.Atan2(transform.forward.x, transform.forward.z) * Mathf.Rad2Deg;
            var angleB = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            // get the signed difference in these angles
            var angleDiff = Mathf.DeltaAngle(angleA, angleB);
            if (Vector3.Distance(transform.position, player.transform.position) < attackRange &&
                angleDiff > -15 && angleDiff < 15
            )
            {
                state = States.Attacking;
                character.direction = Vector3.zero;
                return;
            }

            if (Vector3.Distance(transform.position, player.transform.position) > chaseRange)
            {
                state = States.Patrolling;
                character.direction = Vector3.zero;
                return;
            }
            character.direction = direction;
        }
        else if (state == States.Attacking)
        {
            if (didAttack == false)
            {
                character.DoAttack();
                didAttack = true;
            }
            var currentBaseState = character.anim.GetCurrentAnimatorStateInfo(0);
            if (currentBaseState.shortNameHash != character.attackState)
            {
                state = States.Patrolling;
                didAttack = false;
                return;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0,0.5f);
        Gizmos.DrawSphere(transform.position, chaseRange);
    }
}
