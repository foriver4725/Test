using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Alo
{
    public class StarGenerator : MonoBehaviour
    {
        #region �ϐ��̒�`

        // ���̕��������i�[����ϐ���p��
        [SerializeField] private GameObject starOrigin;

        // �������������i�[����z����쐬
        private GameObject[] stars;

        // ���̐e���w��
        [SerializeField] private GameObject starParent;

        // ���̖��Ã��[�h:true�Ȃ疾�邭����;false�Ȃ�Â�����
        private bool isLighten;

        // ���̖��邳�x�N�g���̐������` (0~255�̐���)
        private byte colorComp;

        // ����SpriteRenderer���i�[���Ă����z����쐬
        private SpriteRenderer[] starsSR;

        #endregion

        void Start()
        {
            // �����i�[����z��̒�����100�ɂ���
            stars = new GameObject[100];

            // ����SpriteRenderer���i�[����z��̒�����100�ɂ���
            starsSR = new SpriteRenderer[100];

            // ���𐶐�
            StarGenerate();

            // ���̖��邳�͍ŏ��͍ŏ�
            colorComp = 0;

            // ���邭���郂�[�h�ɂ���
            isLighten = true;

            // ���̖��Â�ύX����R���[�`�����X�^�[�g
            StartCoroutine(ChangeStarColor());
        }

        #region ���𐶐��C�j�󂷂�֐��̒�`

        /// <summary>
        /// ���𐶐�����֐�
        /// </summary>
        void StarGenerate()
        {
            // 100��J��Ԃ�
            for (int i = 0; i < stars.Length; i++)
            {
                // ��ʓ��́C�����_����2�����ʒu�x�N�g���i���̈ʒu�ɂȂ�j���쐬
                float vector2_x = Random.Range(-8.7f, 8.7f);

                float vector2_y = Random.Range(-4.8f, 4.8f);

                Vector2 starPos = new Vector2(vector2_x, vector2_y);

                // ���𐶐��C�O����z��Ɋi�[
                stars[i] = Instantiate(starOrigin, starPos, transform.rotation);

                starsSR[i] = stars[i].GetComponent<SpriteRenderer>();

                // �����������̐e���w��
                stars[i].transform.parent = starParent.transform;
            }
        }

        /// <summary>
        /// ����j�󂷂�֐�
        /// </summary>
        void StarDestroy()
        {
            // 100��J��Ԃ�
            for (int i = 0; i < stars.Length; i++)
            {
                // ����O����j��
                Destroy(stars[i]);

                stars[i] = null;

                starsSR[i] = null;
            }
        }

        #endregion

        /// <summary>
        /// ���̖��Â�ύX����R���[�`��
        /// </summary>
        IEnumerator ChangeStarColor()
        {
            // ���̃R���[�`�������s���̊Ԉȉ����J��Ԃ�
            while (true)
            {
                // �������邭���郂�[�h�Ȃ�
                if (isLighten)
                {
                    // ���邳���ő�Ȃ�C�Â����郂�[�h�ɂ���
                    if (colorComp >= 255)
                    {
                        colorComp = 255;

                        isLighten = false;
                    }
                    else // �����łȂ��Ȃ�
                    {
                        // ���̖��邳��ݒ�
                        Color starColor = new Color32(colorComp, colorComp, colorComp, 255);

                        // _stars���̑S�ẴI�u�W�F�N�g�̖��邳��ύX
                        for (int i = 0; i < stars.Length; i++)
                        {
                            starsSR[i].color = starColor;
                        }

                        // ���邳�̒l��3�����₷
                        colorComp += 3;
                    }
                }
                else // �����Â����郂�[�h�Ȃ�
                {
                    // ���邳���ŏ��Ȃ�C�����Đ������Ė��邭���郂�[�h�ɂ���
                    if (colorComp <= 0)
                    {
                        colorComp = 0;

                        StarDestroy();

                        StarGenerate();

                        isLighten = true;
                    }
                    else // �����łȂ��Ȃ�
                    {
                        // ���̖��邳��ݒ�
                        Color starColor = new Color32(colorComp, colorComp, colorComp, 255);

                        // _stars���̑S�ẴI�u�W�F�N�g�̖��邳��ύX
                        for (int i = 0; i < stars.Length; i++)
                        {
                            starsSR[i].color = starColor;
                        }

                        // ���邳�̒l��3�����炷
                        colorComp -= 3;
                    }
                }

                // 2�t���[���҂�
                for (int i = 0; i < 2; i++)
                {
                    yield return null;
                }
            }
        }
    }
}

