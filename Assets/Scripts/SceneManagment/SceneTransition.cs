using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.SceneManagment
{
    public class SceneTransition : MonoBehaviour
    {

        Animator animator;
        Animation anim;
        CanvasGroup canvasGroup;
        
        bool faded = false;
        bool ended = false;

        private void Start()
        {
            animator = GetComponent<Animator>();
            canvasGroup = GetComponent<CanvasGroup>();
            animator.speed = 0;
        }

        public IEnumerator FadeOut()
        {
            animator.speed = 1;
            animator.Play("Expand");
            yield return new WaitUntil(() => faded == true);
            animator.speed = 0;
            yield return new WaitForSeconds(1);
        }

        public IEnumerator FastFadeOut()
        {
            animator.speed = 1;
            animator.Play("Loop");
            yield return new WaitUntil(() => faded == true);
            animator.speed = 0;
            yield return new WaitForSeconds(1);
        }

        public IEnumerator FadeIn()
        {
            animator.speed = 1;
            yield return new WaitUntil(() => ended == true);
            animator.speed = 0;
            faded = false;
            ended = false;
        }

        public void Faded()
        {
            faded = true;
        }

        public void Ended()
        {
            ended = true;
        }
    }
}
