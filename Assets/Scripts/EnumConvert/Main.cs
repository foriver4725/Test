using Ex;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnumConvert
{
    public class Main : MonoBehaviour
    {
        enum A { A, B, C };
        int B = 1;
        string C = "C";

        void Start()
        {
            int a = (int)A.A; // enum -> int
            string b = A.A.ToString(); // enum -> string
            A c = (A)Enum.ToObject(typeof(A), B); // int -> enum
            A d = (A)Enum.Parse(typeof(A), C); // string -> enum

            a.Show();
            b.Show();
            c.Show();
            d.Show();
        }
    }
}