using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoadSceneAsync
{
    public class LoadSceneAsyncAfter : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("After:Start");
        }

        private void FixedUpdate()
        {
            Debug.Log("After:FixedUpdate");
        }

        private void Update()
        {
            Debug.Log("After:Update");
        }
    }
}
