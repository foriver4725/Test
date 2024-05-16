using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloatBehaviour
{
    public class Test : MonoBehaviour
    {
        void Start()
        {
            float a = 1f;
            int A = 1;
            Debug.Log(a == 1f);
            Debug.Log(a == A);

            Debug.Log(1f.GetType());
            int b = 1;
            Vector3 v = new Vector3(b, 4, 5);
            Debug.Log(v.x.GetType());
            Debug.Log(v.y.GetType());

            float c = 2.0f;
            float C = 2f;
            Debug.Log((int)c);
            Debug.Log((int)C);
        }
    }
}
