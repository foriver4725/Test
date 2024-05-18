using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Alo
{
    public class ShowTextSample : MonoBehaviour
    {
        public TextMeshProUGUI tmpro; // UIのテキストを設定
        public int stageNum; // 何番目のステージのテキストを使うか
        public int textNum; // 何番目のストーリーのテキストを使うか
        public string stageText; // テキスト全文
        public List<string> eachText; // 改行ごとのテキスト
        public int rowNum = 0; // 何行目を表示するか
        

        public void Start()
        {
            // このストーリーのテキストを取得
            if (stageNum == 1)
            {
                stageText = ParamsSO.Entity.Stage1[textNum - 1];
            }
            else if (stageNum == 2)
            {
                stageText = ParamsSO.Entity.Stage2[textNum - 1];
            }
            string[] cutText = stageText.Split("\n"); // 改行ごとにカット(配列)
            eachText.AddRange(cutText); // リストに変換

            tmpro.text = eachText[0]; // 表示
        }

        public void Update()
        {
            // エンターキーを押したら次の行を表示
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(rowNum <= eachText.Count - 2)
                {
                    rowNum += 1;
                    tmpro.text = eachText[rowNum]; // 表示
                }
                else
                {
                    SceneManager.LoadScene("StorySelect"); // 表示し終わったらストーリー選択画面に戻る
                }
            }
        }
    }
}