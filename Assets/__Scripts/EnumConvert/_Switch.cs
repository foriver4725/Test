using Ex;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnumConvert
{
    public class _Switch : MonoBehaviour
    {
        enum Score
        {
            Small = -100,
            Medium = 0,
            Big = 100,
            VeryBig = 10000
        }

        int score = 50;
        int score2 = 100;

        void Start()
        {
            if ((int)Score.Medium <= score && score < (int)Score.Big)
            {
                "0 <= x < 100".Show();
            }

            switch (score2)
            {
                case (int)Score.Small:
                    "Small!".Show();
                    break;
                case (int)Score.Medium:
                    "Medium!".Show();
                    break;
                case (int)Score.Big:
                    "Big!".Show();
                    break;
                case (int)Score.VeryBig:
                    "Very Big!!!".Show();
                    break;
            }
        }
    }
}