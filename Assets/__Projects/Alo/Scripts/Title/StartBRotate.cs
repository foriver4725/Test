using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alo
{
    public class StartBRotate : MonoBehaviour
    {
        #region ‚±‚Ìƒ{ƒ^ƒ“‰æ‘œ‚ÌRectTransform‚ðstartImageRT‚ÉŠi”[
        private RectTransform startImageRT;

        private void Start()
        {
            startImageRT = GetComponent<RectTransform>();
        }
        #endregion

        [SerializeField] private float speed = -0.05f;

        private void Update()
        {
            startImageRT.Rotate(Vector3.forward * speed);
        }
    }
}

