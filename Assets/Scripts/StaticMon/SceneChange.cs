using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StaticMon
{
    public class SceneChange : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(Data.a++);
                SceneManager.LoadScene("StaticMonAfter");
            }
        }
    }
}
