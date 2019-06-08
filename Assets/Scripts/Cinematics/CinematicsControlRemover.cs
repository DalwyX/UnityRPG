using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;
using RPG.UI;

namespace RPG.Cinematic
{
    public class CinematicsControlRemover : MonoBehaviour
    {

        //[SerializeField] bool skippable = true;
        //[SerializeField] float timeToHoldButton = 1f;

        PlayableDirector playableDirector;
        CustomCursor cursor;

        private void Awake()
        {
            playableDirector = GetComponent<PlayableDirector>();
            cursor = GameObject.FindWithTag("Cursor").GetComponent<CustomCursor>();

            playableDirector.played += OnDisableControl;
            playableDirector.stopped += OnEnableControl;

            if (playableDirector.playOnAwake)
            {
                OnDisableControl(playableDirector);
            }
        }

        private void OnEnableControl(PlayableDirector pd)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<PlayerController>().enabled = true;
            CursorSwitch();
        }

        private void OnDisableControl(PlayableDirector pd)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
            CursorSwitch();
            //if (skippable)
                //StartCoroutine(skipWaiting());
        }

        //private IEnumerator skipWaiting()
        //{
        //    float holdTime = 0;
        //    while (true)
        //    {
        //        holdTime = (Input.GetKey(KeyCode.Space)) ? holdTime + Time.deltaTime : 0;
        //        //print(playableDirector.time);
        //        if (holdTime >= timeToHoldButton)
        //        {
        //            playableDirector.time = playableDirector.duration;
        //        }
        //        yield return null;
        //    }
        //}

        private void CursorSwitch()
        {
            if (cursor != null)
            {
                cursor.CursorSwitch();
            }
        }

    }
}
