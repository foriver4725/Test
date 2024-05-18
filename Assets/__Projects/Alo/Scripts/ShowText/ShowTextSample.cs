using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Alo
{
    public class ShowTextSample : MonoBehaviour
    {
        public TextMeshProUGUI tmpro; // UI�̃e�L�X�g��ݒ�
        public int stageNum; // ���Ԗڂ̃X�e�[�W�̃e�L�X�g���g����
        public int textNum; // ���Ԗڂ̃X�g�[���[�̃e�L�X�g���g����
        public string stageText; // �e�L�X�g�S��
        public List<string> eachText; // ���s���Ƃ̃e�L�X�g
        public int rowNum = 0; // ���s�ڂ�\�����邩
        

        public void Start()
        {
            // ���̃X�g�[���[�̃e�L�X�g���擾
            if (stageNum == 1)
            {
                stageText = ParamsSO.Entity.Stage1[textNum - 1];
            }
            else if (stageNum == 2)
            {
                stageText = ParamsSO.Entity.Stage2[textNum - 1];
            }
            string[] cutText = stageText.Split("\n"); // ���s���ƂɃJ�b�g(�z��)
            eachText.AddRange(cutText); // ���X�g�ɕϊ�

            tmpro.text = eachText[0]; // �\��
        }

        public void Update()
        {
            // �G���^�[�L�[���������玟�̍s��\��
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(rowNum <= eachText.Count - 2)
                {
                    rowNum += 1;
                    tmpro.text = eachText[rowNum]; // �\��
                }
                else
                {
                    SceneManager.LoadScene("StorySelect"); // �\�����I�������X�g�[���[�I����ʂɖ߂�
                }
            }
        }
    }
}