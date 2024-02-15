using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeGame
{
    [CreateAssetMenu(menuName = "ParamsSO/LifeGame/LifeGame", fileName = "LifeGameSO")]
    public class LifeGameSO : ScriptableObject
    {
        #region QOL向上処理
        // ParamsSOが保存してある場所のパス
        public const string PATH = "LifeGame/LifeGameSO";

        // ParamsSOの実体
        private static LifeGameSO _entity = null;
        public static LifeGameSO Entity
        {
            get
            {
                // 初アクセス時にロードする
                if (_entity == null)
                {
                    _entity = Resources.Load<LifeGameSO>(PATH);

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

        [Header("初期配置の入力：\n生きているセルの座標を、\n中心を(0, 0)、間隔を1として入力する")] public Vector2[] FirstStateInput;
        [Space(25)]
        [Header("以下の初期値による描画を、何倍に縮小するか")] public int DrawShrinkScale;
        [Header("縦のセル数（奇数）")] public int H;
        [Header("横のセル数（奇数）")] public int W;
        [Header("セルの1辺の長さ")] public float CellEdgeLength;
        [Space(25)]
        [Header("シミュレーションの間隔（秒）")] public float SimulationInterval;
        [Header("セル達の親のプレハブ")] public GameObject CellsParent;
        [Header("生きているセルのプレハブ")] public GameObject LivingCellPrefab;
        [Header("死んでいるセルのプレハブ")] public GameObject DiedCellPrefab;
    }
}
