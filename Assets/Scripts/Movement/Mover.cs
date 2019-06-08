using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;

namespace RPG.Movement
{

    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        NavMeshAgent navMeshAgent;
        Animator animator;
        Health health;
        ActionScheduler actionScheduler;

        float defaultSpeed;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
            actionScheduler = GetComponent<ActionScheduler>();

            defaultSpeed = navMeshAgent.speed;
        }

        private void Update()
        {
            navMeshAgent.enabled = !health.isDead;
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedMod = 1)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedMod);
        }

        private void UpdateAnimator()
        {
            float speed = navMeshAgent.velocity.magnitude;
            animator.SetFloat("forwardSpeed", speed);
        }

        public void MoveTo(Vector3 position, float speedMod = 1)
        {
            navMeshAgent.speed = defaultSpeed * speedMod;
            navMeshAgent.destination = position;
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            navMeshAgent.enabled = false;
            SerializableVector3 v3 = (SerializableVector3)state;
            transform.position = v3.ToVector();
            navMeshAgent.enabled = true;
            actionScheduler.CancelCurrentAction();
        }
    }

}
