using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace RPG.Core
{
    public class AutoDistance : MonoBehaviour
    {

        [SerializeField] CinemachineVirtualCamera cam;
        [SerializeField] float minDistance = 1f;
        [SerializeField] float maxDistance = 10f;
        CinemachineFramingTransposer composer;
        Transform player;


        private void Start()
        {
            player = cam.Follow;
            composer = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
        }

        private void Update()
        {
            RaycastHit hit;
            bool hasHit = Physics.Linecast(player.position, cam.transform.position, out hit);
            //Debug.DrawRay(cinemachineFar.transform.position, player.position - cinemachineFar.transform.position, Color.blue);
            if (hasHit)
            {
                if (hit.collider.tag == "Obstacle")
                    composer.m_CameraDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
            }
            else
                composer.m_CameraDistance = maxDistance;
        }

    }
}
