using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    // データベース
    [CreateAssetMenu(menuName = "ParamsSO/SO/Test", fileName = "TestParamsSO")]
    public class TestParamsSO : ScriptableObject
    {
        #region ParamsSOのQOL向上処理
        //ParamsSOが保存してある場所のパス
        public const string PATH = "SO/TestParamsSO";

        //ParamsSOの実体
        private static TestParamsSO _entity;
        public static TestParamsSO Entity
        {
            get
            {
                //初アクセス時にロードする
                if (_entity == null)
                {
                    _entity = Resources.Load<TestParamsSO>(PATH);

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

        public List<Database> DataBases;

        public List<Database> GetData(Database.Type type)
        {
            List<Database> found = DataBases.FindAll(e => e.type == type);
            if (found != null)
            {
                return found;
            }
            else
            {
                Debug.LogError($"type:{type}のクラスが見つかりません");
                return null;
            }
        }
    }

    [Serializable]
    public class Database
    {
        public enum Type
        {
            SLIME,
            HUMAN,
            DRAGON
        }
        public Type type;

        public string name;
        public int hp;
        public int attack;
        [TextArea(1, 100)] public string text;
    }
}
