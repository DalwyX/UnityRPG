using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


namespace RPG.Core
{
    public class AutoCameraSwitch : MonoBehaviour
    {

        [SerializeField] CinemachineVirtualCamera cinemachineFar;
        [SerializeField] CinemachineVirtualCamera cinemachineClose;
        CinemachineFramingTransposer composer;
        Transform player;

        private void Start()
        {
            player = cinemachineFar.Follow;
        }

        private void Update()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(cinemachineFar.transform.position, player.position - cinemachineFar.transform.position, out hit);
            Debug.DrawRay(cinemachineFar.transform.position, player.position - cinemachineFar.transform.position, Color.blue);
            if (hasHit)
            {
                if (hit.collider.tag == "Obstacle")
                    SwitchToCloseCamera();
                else
                    SwitchToFarCamera();
            }
        }

        private void SwitchToCloseCamera()
        {
            cinemachineClose.Priority = 150;
        }

        private void SwitchToFarCamera()
        {
            cinemachineClose.Priority = 50;
        }

    }
}
