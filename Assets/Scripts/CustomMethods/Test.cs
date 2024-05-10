using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ex;

namespace CustomMethods
{
    public class Test : MonoBehaviour
    {
        void Start()
        {
            List<string> a = new() { "a", "b", "c" };
            List<string> b = new() { "x", "y", "z" };
            Collection.Make((e) => e, (e) => e.Item1 != "b", Collection.Zip(a, b)).Look();

            Collection.Make((e) => e % 2 == 1 ? "odd" : "even", Collection.Range(10)).Look((e) => '"' + e + '"');

            Collection.Map((e) => e % 2 == 0, Collection.Make((e) => e, Collection.Range(10))).Look();
        }

        void Update()
        {

        }
    }
}