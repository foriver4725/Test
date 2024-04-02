using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomSeed
{
    public class MakeNumbers : MonoBehaviour
    {
        void Start()
        {
            Random.InitState(204618424);
        }

        void Update()
        {
            Debug.Log(Random.Range(1, 100));
        }
    }
}
