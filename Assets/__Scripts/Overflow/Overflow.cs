using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Overflow
{
    public class Overflow : MonoBehaviour
    {
        byte byteMax = byte.MaxValue;
        byte byteMin = byte.MinValue;
        int intMax = int.MaxValue;
        int intMin = int.MinValue;

        void Start()
        {
            //Debug.Log(++byteMax);
            //Debug.Log(--byteMin);
            //Debug.Log(++intMax);
            //Debug.Log(--intMin);

            checked
            {
                Debug.Log(++byteMax);
                Debug.Log(--byteMin);
                Debug.Log(++intMax);
                Debug.Log(--intMin);
            }
        }
    }
}
