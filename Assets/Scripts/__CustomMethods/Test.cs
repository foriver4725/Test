using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ex;

namespace CustomMethods
{
    public class Test : MonoBehaviour
    {
        void Awake()
        {
            enabled = true;
        }

        void Start()
        {
            List<string> a = new() { "a", "b", "c" };
            List<string> b = new() { "x", "y", "z" };
            Collection.Make(Collection.Zip(a, b), (e) => e.Item1 != "b", (e) => e).Look();

            Collection.Make(Collection.Range(10), (e) => e % 2 == 1 ? "odd" : "even").Look((e) => '"' + e + '"');

            Collection.Map(Collection.Make(Collection.Range(10), (e) => e), (e) => e % 2 == 0).Look();

            List<int> c = new() { 1, 2, 3, -1, -2, -3 };
            c.Sort();
            c.Look();
        }

        void Update()
        {

        }
    }
}