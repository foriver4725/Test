using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

namespace BallLaunch
{
    public class GameStart : MonoBehaviour
    {
        [SerializeField] private GameObject text; // �X�^�[�g�{�^���̃e�L�X�g������
        [SerializeField] private GameObject ballPrefab; // �{�[���̃v���n�u���i�[
        private bool flag = true;

        private float force = 52.5f;// �{�[�����ł��������
        private float towardY = 4.5f; // �{�[�����ł�������ړI�n

        private List<GameObject> balls = new List<GameObject>(); // ���������i�[

        void Update()
        {
            // ���߂ăX�y�[�X�L�[�������ꂽ���񂾂��s��
            if (Input.GetKey(KeyCode.Space) && flag)
            {
                // �R���[�`�����J�n���C�{�^����j�󂷂�
                Destroy(text);
                StartCoroutine(BallGenerate());
                flag = false;
            }

            #region ��ʊO�ɍs�����{�[�����폜
            List<GameObject> trushBalls = new List<GameObject>();

            foreach (GameObject ball in balls)
            {
                Vector3 ballPs = ball.GetComponent<Transform>().position;
                if (Mathf.Abs(ballPs.x) >= 14.5f || Mathf.Abs(ballPs.y) >= 7f)
                {
                    trushBalls.Add(ball);
                }
            }

            foreach (GameObject ball in trushBalls)
            {
                balls.Remove(ball);
                Destroy(ball);
            }
            #endregion
        }

        /// <summary>
        /// 1.5�b���ƂɁC�����_���ȐF�̋��𐶐�
        /// </summary>
        IEnumerator BallGenerate()
        {
            while (true)
            {
                // ����
                GameObject ball = Instantiate(ballPrefab, new Vector3(29f * (Random.value - 0.5f), -5.5f, 0f), Quaternion.identity);
                balls.Add(ball);
                // �����_���ȐF��t�^
                Color randomColor = new Color(Random.value, Random.value, Random.value, 1.0f);
                ball.GetComponent<Renderer>().material.color = randomColor;
                // �����_���Ȉʒu�ɂ���
                Rigidbody rb = ball.GetComponent<Rigidbody>();
                Vector3 position = ball.GetComponent<Transform>().position;
                rb.AddForce(force * (towardY * Vector3.up - position));

                yield return new WaitForSeconds(1.5f);
            }
        }
    }
}
