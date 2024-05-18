using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomMaze
{
    [CreateAssetMenu(menuName = "ParamsSO/RandomMaze/Prefab", fileName = "PrefabParamsSO")]
    public class PrefabParamsSO : ScriptableObject
    {
        #region QOL向上処理
        // ParamsSOが保存してある場所のパス
        public const string PATH = "RandomMaze/PrefabParamsSO";

        // ParamsSOの実体
        private static PrefabParamsSO _entity = null;
        public static PrefabParamsSO Entity
        {
            get
            {
                // 初アクセス時にロードする
                if (_entity == null)
                {
                    _entity = Resources.Load<PrefabParamsSO>(PATH);

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

        [Header("回廊の親オブジェクト")] public GameObject Room;
        [Header("スタート/ゴール")] public GameObject[] StartAndGoal;
        [Header("隅の部屋")] public GameObject[] CornerRooms;
        [Header("辺の部屋")] public GameObject[] SideRooms;
        [Header("中央の部屋")] public GameObject[] CenterRooms;
    }
}
