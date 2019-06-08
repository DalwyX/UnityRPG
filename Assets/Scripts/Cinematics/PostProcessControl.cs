using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace RPG.Cinematic
{
    [ExecuteInEditMode]
    public class PostProcessControl : MonoBehaviour
    {

        [SerializeField] bool isWorking = false;
        [SerializeField] float bloomValue;
        [SerializeField] float dofValue;

        Bloom bloomLayer;
        DepthOfField dof;


        private void Start()
        {
            PostProcessVolume ppv = GetComponent<PostProcessVolume>();
            ppv.profile.TryGetSettings(out bloomLayer);
            ppv.profile.TryGetSettings(out dof);
        }

        private void Update()
        {
            if (isWorking)
            {
                bloomLayer.intensity.value = bloomValue;
                dof.focusDistance.value = dofValue;
            }
        }

    }
}
