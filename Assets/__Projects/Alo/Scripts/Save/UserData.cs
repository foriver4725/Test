using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alo
{
    [System.Serializable]　// これでセーブできるようにする
    public class UserData　// MonoBehaviorは継承しない（シーン遷移時にこのスクリプトのインスタンスが破棄されるのを防ぐため）
    {
        #region セーブデータ一覧（プロパティにすると動かなくなる。ロードすると初期値から変更）
        // テスト値
        public int Test = 0;
        #endregion
    }
}

