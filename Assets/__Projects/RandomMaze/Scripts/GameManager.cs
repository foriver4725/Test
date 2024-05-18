using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RandomMaze
{
    public class GameManager : MonoBehaviour
    {
        #region staticかつシングルトンにする
        public static GameManager Instance { get; set; }

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

        [System.NonSerialized] public bool IsClear = false;

        [SerializeField] GameObject gameUI;
        [SerializeField] GameObject clearUI;

        // 時間が繰り上がる境界（sec => min, com => min）
        readonly int[] timeConst = new int[] { 60, 100 };
        // 時間の表記を何桁揃えにするか
        const int digits = 2;

        // min,sec,com
        int[] time = new int[] { 0, 0, 0 };
        TextMeshProUGUI timer;
        bool isClearUIShowed = false;

        void Start()
        {
            timer = gameUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            StartCoroutine(TimeCount());

            gameUI.SetActive(true);
            clearUI.SetActive(false);
        }

        void Update()
        {
            if (IsClear)
            {
                if (!isClearUIShowed)
                {
                    isClearUIShowed = true;

                    gameUI.SetActive(false);
                    clearUI.SetActive(true);
                    if (time[2] >= timeConst[1])
                    {
                        time[1] += 1;
                        time[2] -= timeConst[1];
                    }

                    if (time[1] >= timeConst[0])
                    {
                        time[0] += 1;
                        time[1] -= timeConst[0];
                    }
                    string min = time[0].ToString();
                    string sec = time[1].ToString().PadLeft(digits, '0');
                    string com = time[2].ToString().PadLeft(digits, '0');
                    TextMeshProUGUI clearText = clearUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
                    clearText.text = $"Clear\r\n<size=36>{min}:{sec}:{com}</size>";
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    // リトライ
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }

        // 経過時間をカウント
        IEnumerator TimeCount()
        {
            while (!IsClear)
            {
                yield return new WaitForSeconds(1 / (float)timeConst[1]);
                time[2] += 1;

                if (time[2] >= timeConst[1])
                {
                    time[1] += 1;
                    time[2] -= timeConst[1];
                }

                if (time[1] >= timeConst[0])
                {
                    time[0] += 1;
                    time[1] -= timeConst[0];
                }

                // 時間を表示
                string min = time[0].ToString();
                string sec = time[1].ToString().PadLeft(digits, '0');
                string com = time[2].ToString().PadLeft(digits, '0');
                timer.text = $"{min}:{sec}:{com}";
            }
        }
    }
}
