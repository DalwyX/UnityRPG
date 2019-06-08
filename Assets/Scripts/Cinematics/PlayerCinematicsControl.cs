using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;

namespace RPG.Cinematic
{
    public class PlayerCinematicsControl : MonoBehaviour
    {

        [SerializeField] State state = State.DoNothing;
        [SerializeField] Vector3 destination;
        [SerializeField] float speedModifier;

        Transform player;
        Mover mover;

        public enum State { DoNothing, GoToPosition }

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            mover = player.GetComponent<Mover>();
        }

        private void Update()
        {
            if (state == State.GoToPosition)
            {
                mover.StartMoveAction(destination, speedModifier);
                state = State.DoNothing;
            }
        }

    }
}
