using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomMaze
{
    public class PlayerLight : MonoBehaviour
    {
        [SerializeField] GameObject[] lights;

        int handingNum = 0;
        bool isOn = false;

        void Update()
        {
            if (GameManager.Instance.IsClear) { GetComponent<PlayerLight>().enabled = false; }

            // �}�E�X�̃z�C�[���Ń��C�g�؂�ւ�
            float value = Input.GetAxisRaw("Mouse ScrollWheel");
            if (value < 0f)
            {
                handingNum += 1;
                if (handingNum > lights.Length - 1)
                {
                    handingNum = 0;
                }

                isOn = false;
            }
            else if (value > 0f)
            {
                handingNum -= 1;
                if (handingNum < 0)
                {
                    handingNum = lights.Length - 1;
                }

                isOn = false;
            }

            // �E�N���b�N�ŁA���C�g�̃I���I�t
            if (Input.GetMouseButtonDown(1))
            {
                isOn = !isOn;
            }

            // ���C�g�̐ݒ�
            for (int i = 0; i < lights.Length; i++)
            {
                if (i == handingNum)
                {
                    if (isOn)
                    {
                        lights[i].SetActive(true);
                    }
                    else
                    {
                        lights[i].SetActive(false);
                    }
                }
                else
                {
                    lights[i].SetActive(false);
                }
            }

        }
    }
}
