using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Alo
{
    public class StarGenerator : MonoBehaviour
    {
        #region 変数の定義

        // 星の複製元を格納する変数を用意
        [SerializeField] private GameObject starOrigin;

        // 生成した星を格納する配列を作成
        private GameObject[] stars;

        // 星の親を指定
        [SerializeField] private GameObject starParent;

        // 星の明暗モード:trueなら明るくする;falseなら暗くする
        private bool isLighten;

        // 星の明るさベクトルの成分を定義 (0~255の整数)
        private byte colorComp;

        // 星のSpriteRendererを格納しておく配列を作成
        private SpriteRenderer[] starsSR;

        #endregion

        void Start()
        {
            // 星を格納する配列の長さを100にする
            stars = new GameObject[100];

            // 星のSpriteRendererを格納する配列の長さを100にする
            starsSR = new SpriteRenderer[100];

            // 星を生成
            StarGenerate();

            // 星の明るさは最初は最小
            colorComp = 0;

            // 明るくするモードにする
            isLighten = true;

            // 星の明暗を変更するコルーチンをスタート
            StartCoroutine(ChangeStarColor());
        }

        #region 星を生成，破壊する関数の定義

        /// <summary>
        /// 星を生成する関数
        /// </summary>
        void StarGenerate()
        {
            // 100回繰り返す
            for (int i = 0; i < stars.Length; i++)
            {
                // 画面内の，ランダムな2次元位置ベクトル（星の位置になる）を作成
                float vector2_x = Random.Range(-8.7f, 8.7f);

                float vector2_y = Random.Range(-4.8f, 4.8f);

                Vector2 starPos = new Vector2(vector2_x, vector2_y);

                // 星を生成，前から配列に格納
                stars[i] = Instantiate(starOrigin, starPos, transform.rotation);

                starsSR[i] = stars[i].GetComponent<SpriteRenderer>();

                // 生成した星の親を指定
                stars[i].transform.parent = starParent.transform;
            }
        }

        /// <summary>
        /// 星を破壊する関数
        /// </summary>
        void StarDestroy()
        {
            // 100回繰り返す
            for (int i = 0; i < stars.Length; i++)
            {
                // 星を前から破壊
                Destroy(stars[i]);

                stars[i] = null;

                starsSR[i] = null;
            }
        }

        #endregion

        /// <summary>
        /// 星の明暗を変更するコルーチン
        /// </summary>
        IEnumerator ChangeStarColor()
        {
            // このコルーチンが実行中の間以下を繰り返す
            while (true)
            {
                // もし明るくするモードなら
                if (isLighten)
                {
                    // 明るさが最大なら，暗くするモードにする
                    if (colorComp >= 255)
                    {
                        colorComp = 255;

                        isLighten = false;
                    }
                    else // そうでないなら
                    {
                        // 星の明るさを設定
                        Color starColor = new Color32(colorComp, colorComp, colorComp, 255);

                        // _stars内の全てのオブジェクトの明るさを変更
                        for (int i = 0; i < stars.Length; i++)
                        {
                            starsSR[i].color = starColor;
                        }

                        // 明るさの値を3ずつ増やす
                        colorComp += 3;
                    }
                }
                else // もし暗くするモードなら
                {
                    // 明るさが最小なら，星を再生成して明るくするモードにする
                    if (colorComp <= 0)
                    {
                        colorComp = 0;

                        StarDestroy();

                        StarGenerate();

                        isLighten = true;
                    }
                    else // そうでないなら
                    {
                        // 星の明るさを設定
                        Color starColor = new Color32(colorComp, colorComp, colorComp, 255);

                        // _stars内の全てのオブジェクトの明るさを変更
                        for (int i = 0; i < stars.Length; i++)
                        {
                            starsSR[i].color = starColor;
                        }

                        // 明るさの値を3ずつ減らす
                        colorComp -= 3;
                    }
                }

                // 2フレーム待つ
                for (int i = 0; i < 2; i++)
                {
                    yield return null;
                }
            }
        }
    }
}

