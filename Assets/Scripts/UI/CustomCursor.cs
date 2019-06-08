using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{

    [RequireComponent(typeof(Image))]
    public class CustomCursor : MonoBehaviour
    {
        Image image;

        void Awake()
        {
            image = GetComponent<Image>();
        }

        void Update()
        {
            Cursor.visible = false;
            transform.position = Input.mousePosition;
        }

        public void CursorSwitch()
        {
            if (image == null)
                image = GetComponent<Image>();
            image.enabled = !image.enabled;
        }
    }

}
