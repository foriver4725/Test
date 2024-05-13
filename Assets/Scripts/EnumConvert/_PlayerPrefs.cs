using Ex;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnumConvert
{
    public class _PlayerPrefs : MonoBehaviour
    {
        enum Keys { Difficulty, IsOnGame };
        enum Difficulty { East, Normal, Hard };
        enum IsOnGame { True, False };

        Difficulty difficulty = Difficulty.Hard;
        IsOnGame isOnGame = IsOnGame.True;

        void Start()
        {
            PlayerPrefs.SetInt(Keys.Difficulty.ToString(), (int)difficulty);
            PlayerPrefs.SetInt(Keys.IsOnGame.ToString(), (int)isOnGame);

            ((Difficulty)Enum.ToObject(typeof(Difficulty), PlayerPrefs.GetInt(Keys.Difficulty.ToString()))).Show();
            ((IsOnGame)Enum.ToObject(typeof(IsOnGame), PlayerPrefs.GetInt(Keys.IsOnGame.ToString()))).Show();
        }
    }
}