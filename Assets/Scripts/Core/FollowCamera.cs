using UnityEngine;

namespace RPG.Core
{

    public class FollowCamera : MonoBehaviour
    {

        Transform player;
        Vector3 cameraToPlayerVector;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            cameraToPlayerVector = Camera.main.transform.position - player.transform.position;
        }

        void LateUpdate()
        {
            transform.position = player.transform.position + cameraToPlayerVector;
        }
    }

}
