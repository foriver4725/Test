using Ex;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnumConvert
{
    public class _Tag : MonoBehaviour
    {
        enum Tag
        {
            EnumConvert_Player,
            EnumConvert_Enemy,
            EnumConvert_Slime,
            EnumConvert_Null
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.HasTag(Tag.EnumConvert_Player.ToString()))
            {
                "Hit the player!".Show();
            }

            if (collision.HasTag(Tag.EnumConvert_Enemy.ToString()))
            {
                "Hit the enemy!".Show();
            }

            if (collision.HasTag(Tag.EnumConvert_Slime.ToString()))
            {
                "Hit the slime!".Show();
            }
        }
    }
}