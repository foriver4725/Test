using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnim : MonoBehaviour
{
    Animator anim;
    bool isScale = false;
    bool isRot = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!isRot)
            {
                if (!isScale)
                {
                    isScale = true;
                    anim.SetBool("Scale", true);
                }
                else
                {
                    isScale = false;
                    anim.SetBool("Scale", false);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isScale)
            {
                if (!isRot)
                {
                    isRot = true;
                    anim.SetBool("Rot", true);
                }
                else
                {
                    isRot = false;
                    anim.SetBool("Rot", false);
                }
            }
        }
    }
}
