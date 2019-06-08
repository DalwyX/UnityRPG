using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Saving;
using UnityEngine.AI;

namespace RPG.SceneManagment
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int portalIndex;
        [SerializeField] int teleportSceneIndex = 0;
        [SerializeField] int teleportPortalIndex = 0;
        [SerializeField] float teleportDelay = 0.5f;

        SavingWrapper saving;

        private void Start()
        {
            saving = FindObjectOfType<SavingWrapper>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        public int GetPortalIndex()
        {
            return portalIndex;
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);
            SceneTransition st = FindObjectOfType<SceneTransition>();

            yield return new WaitForSeconds(teleportDelay);
            yield return st.FadeOut();
            saving.Save();
            yield return SceneManager.LoadSceneAsync(teleportSceneIndex);
            saving.Load();
            UpdatePlayer(GetOtherPortal());
            saving.Save();
            yield return st.FadeIn();
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.transform.GetChild(0).transform.position;
            player.transform.rotation = otherPortal.transform.GetChild(0).transform.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        private Portal GetOtherPortal()
        {
            Portal[] portals = FindObjectsOfType<Portal>();
            foreach (Portal portal in portals)
            {
                if (portal != this && portal.GetPortalIndex() == this.teleportPortalIndex)
                {
                    return portal;
                }
            }
            return null;
        }
    }
}
