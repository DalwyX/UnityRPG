using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagment
{
    [RequireComponent(typeof(SavingSystem))]
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        SavingSystem savingSystem;

        private IEnumerator Start()
        {
            savingSystem = GetComponent<SavingSystem>();
            SceneTransition fader = FindObjectOfType<SceneTransition>();
            yield return fader.FastFadeOut();
            yield return savingSystem.LoadLastScene(defaultSaveFile);
            yield return fader.FadeIn();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Save();
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
        }

        public void Load()
        {
            savingSystem.Load(defaultSaveFile);
        }

        public void Save()
        {
            savingSystem.Save(defaultSaveFile);
        }
    }
}
