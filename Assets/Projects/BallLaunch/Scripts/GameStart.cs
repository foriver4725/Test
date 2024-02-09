using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

namespace BallLaunch
{
    public class GameStart : MonoBehaviour
    {
        [SerializeField] private GameObject text; // スタートボタンのテキストを入れる
        [SerializeField] private GameObject ballPrefab; // ボールのプレハブを格納
        private bool flag = true;

        private float force = 52.5f;// ボールが打ちあがる力
        private float towardY = 4.5f; // ボールが打ちあがる目的地

        private List<GameObject> balls = new List<GameObject>(); // 複製物を格納

        void Update()
        {
            // 初めてスペースキーが押されたら一回だけ行う
            if (Input.GetKey(KeyCode.Space) && flag)
            {
                // コルーチンを開始し，ボタンを破壊する
                Destroy(text);
                StartCoroutine(BallGenerate());
                flag = false;
            }

            #region 画面外に行ったボールを削除
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
        /// 1.5秒ごとに，ランダムな色の球を生成
        /// </summary>
        IEnumerator BallGenerate()
        {
            while (true)
            {
                // 生成
                GameObject ball = Instantiate(ballPrefab, new Vector3(29f * (Random.value - 0.5f), -5.5f, 0f), Quaternion.identity);
                balls.Add(ball);
                // ランダムな色を付与
                Color randomColor = new Color(Random.value, Random.value, Random.value, 1.0f);
                ball.GetComponent<Renderer>().material.color = randomColor;
                // ランダムな位置にする
                Rigidbody rb = ball.GetComponent<Rigidbody>();
                Vector3 position = ball.GetComponent<Transform>().position;
                rb.AddForce(force * (towardY * Vector3.up - position));

                yield return new WaitForSeconds(1.5f);
            }
        }
    }
}
