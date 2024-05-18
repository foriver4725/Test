using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomMaze
{
    [CreateAssetMenu(menuName = "ParamsSO/RandomMaze/Player", fileName = "PlayerParamsSO")]
    public class PlayerParamsSO : ScriptableObject
    {
        #region QOL向上処理
        // ParamsSOが保存してある場所のパス
        public const string PATH = "RandomMaze/PlayerParamsSO";

        // ParamsSOの実体
        private static PlayerParamsSO _entity = null;
        public static PlayerParamsSO Entity
        {
            get
            {
                // 初アクセス時にロードする
                if (_entity == null)
                {
                    _entity = Resources.Load<PlayerParamsSO>(PATH);

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

        [Header("速さ")] public float Speed = 100f;
        [Header("視点感度(x,y)")] public float[] Sensitivity = new float[] { 3f, 3f };
        [Header("鉛直視点の制限(オイラー角：min,max)")] public float[] CameraRotXLimit = new float[] { -90, 90 };
        [Header("ダッシュ時に、速さが何倍になるか")] public float SpeedCoef = 2f;
        [Header("振り返っている時に、速さが何倍になるか")] public float OnLookingBackSpeedCoef = 1f;
    }
}
