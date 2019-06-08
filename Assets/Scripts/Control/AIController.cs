using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Movement;
using RPG.Combat;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {

        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float returnDelay = 5f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float dwellingTime = 2.5f;
        [SerializeField] float patrollingSpeedModifier = 0.8f;

        GameObject player;
        ActionScheduler actionScheduler;
        Health health;
        Mover mover;
        Fighter fighter;

        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = 0;
        int currentWaypointIndex = 0;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            actionScheduler = GetComponent<ActionScheduler>();
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();

            guardPosition = transform.position;
        }

        private void Update()
        {
            if (health.isDead) return;
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= chaseDistance && !player.GetComponent<Health>().isDead)
            {
                timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if(timeSinceLastSawPlayer <= returnDelay)
            {
                SuspisionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;

            if (patrolPath != null)
            {
                if(AtWaypoint())
                {
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            mover.StartMoveAction(nextPosition, patrollingSpeedModifier);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            timeSinceArrivedAtWaypoint += Time.deltaTime;
            if (timeSinceArrivedAtWaypoint >= dwellingTime)
            {
                currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
                timeSinceArrivedAtWaypoint = 0;
            }
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void SuspisionBehaviour()
        {
            actionScheduler.CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player.gameObject);
        }

        private void OnDrawGizmosSelected ()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

    }
}
