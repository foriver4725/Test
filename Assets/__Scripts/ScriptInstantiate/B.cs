using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptInstantiate
{
    public class B : MonoBehaviour
    {
        void Start()
        {
            A a = new A();
            a.A1();
        }
    }
}
