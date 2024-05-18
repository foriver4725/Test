using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alo
{
    // データベース
    [CreateAssetMenu]
    public class ParamsSO : ScriptableObject
    {
        #region ParamsSOのQOL向上処理
        //ParamsSOが保存してある場所のパス
        public const string PATH = "Alo/ParamsSO";

        //ParamsSOの実体
        private static ParamsSO _entity;
        public static ParamsSO Entity
        {
            get
            {
                //初アクセス時にロードする
                if (_entity == null)
                {
                    _entity = Resources.Load<ParamsSO>(PATH);

                    //ロード出来なかった場合はエラーログを表示
                    if (_entity == null)
                    {
                        Debug.LogError(PATH + " not found");
                    }
                }

                return _entity;
            }
        }
        #endregion

        // セーブはできない，インスペクタ上から値を変更すること!!!
        [TextArea(1, 100)] public string[] Stage1;
        [TextArea(1, 100)] public string[] Stage2;
    }
}
