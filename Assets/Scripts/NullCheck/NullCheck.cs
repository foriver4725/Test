using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullCheck
{
    public class NullCheck : MonoBehaviour
    {
        [SerializeField] GameObject obj;

        void Start()
        {
#if true
            DestroyImmediate(obj);

            if (obj != null) { Debug.Log(obj.name); }
            Debug.Log(obj?.name);
#else
            Destroy(obj);

            if (obj != null) { Debug.Log(obj.name); }
            Debug.Log(obj?.name);
#endif
        }
    }
}