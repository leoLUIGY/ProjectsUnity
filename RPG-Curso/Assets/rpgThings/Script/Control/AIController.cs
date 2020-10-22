using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using RPG.Resources;


namespace RPG.Control{
public class AIController : MonoBehaviour
{
    [SerializeField] float chaseDistance = 5f;
    [SerializeField] float suspicionTime = 3f;
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float waypointTolerance = 1f;
    [SerializeField] float waypointDwellTime = 3f;
    [Range(0, 1)]
    [SerializeField] float patrolSpeedFraction = 0.2f;
    // Start is called before the first frame update
    Fighter fighter;
    Mover mover;
    Health health;
    GameObject player;
    float timeSinceLastSawPlayer = Mathf.Infinity;
    float timeSinceArrivedWaypoint = Mathf.Infinity;
    int currentWaypointIndex = 0;

    Vector3 guardPosition;
    void Start()
    {
        fighter =GetComponent<Fighter>();
        health = GetComponent<Health>();
        mover = GetComponent<Mover>();
        player = GameObject.FindWithTag("Player");

        guardPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(health.IsDead()) return;
        if(InAttackRangeOfPlayer() && fighter.CanAttack(player)){
            AttackBehaviour();
        } else if(timeSinceLastSawPlayer < suspicionTime){
            SuspicionBehaviour();
        }else{
            PatrolBehaviour();
        }
        UpdateTimers();
    }

    private void UpdateTimers(){
          timeSinceLastSawPlayer += Time.deltaTime;
        timeSinceArrivedWaypoint += Time.deltaTime;
    }
    private void PatrolBehaviour(){
        Vector3 nextPosition = guardPosition;

        if(patrolPath != null)
        {
            if(AtWaypoint())
            {
                timeSinceArrivedWaypoint = 0;
                CycleWaypoint();
            }
            nextPosition = GetCurrentWaypoint();
        }

        if(timeSinceArrivedWaypoint>waypointDwellTime)
        {
            mover.StartMoveAction(nextPosition, patrolSpeedFraction);
        }
    }
    private bool AtWaypoint(){
        float distanceToWaypoint = Vector3.Distance(transform.position,  GetCurrentWaypoint());
        return distanceToWaypoint < waypointTolerance;
    }
    private void CycleWaypoint(){
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    private Vector3 GetCurrentWaypoint()
    {
        return patrolPath.GetWaypoint(currentWaypointIndex);
    }

    private void SuspicionBehaviour(){
        GetComponent<ActionSchedule>().CancelCurrentAction();
    }

    private void AttackBehaviour(){
        timeSinceLastSawPlayer = 0;
        GetComponent<Fighter>().Attack(player);
    }
    private bool InAttackRangeOfPlayer(){
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        return distanceToPlayer < chaseDistance;    
        }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
}