using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alo
{
    public class ShowText : ShowTextSample
    {
        private void Awake()
        {
            // 初期設定で、ステージ1-1のテキストを使う
            stageNum = 1;
            textNum = 1;
        }
    }
}
