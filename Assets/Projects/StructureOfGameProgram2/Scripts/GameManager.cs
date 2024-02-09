using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace StructureOfGameProgram2
{
    public class GameManager : MonoBehaviour
    {
        #region staticかつシングルトンにする
        public static GameManager Instance { get; set; } = null;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion

        public TextMeshProUGUI TextScore; // テキストを表示するTMProをセット
        public GameObject GameOverScreen; // ゲームオーバーUI
        public bool IsGameActive; // ゲームの状態
        public int Score = 0; // スコア
        public int Point = 1; // パワーアップ中に敵に触れた際，スコアに加算されるポイント

        // チェイス状態（初期値はオン。trueなら敵がプレイヤーを追いかけ，falseなら敵がプレイヤーから逃げる）
        public bool IsChasing = true;

        private void Start()
        {
            IsGameActive = true;
            GameOverScreen.SetActive(false);
        }

        private void Update()
        {
            if (!IsGameActive)
            {
                GameOverScreen.SetActive(true);
            }
            else
            {
                TextScore.text = "Score: " + Score;
            }
        }

        public void ReStart()
        {
            // 現在のシーンを再ロード
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
