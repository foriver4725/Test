using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeGame
{
    public class Handler : MonoBehaviour
    {
        int[,] field; // これを用いて処理を行う。0が死、1が生
        GameObject[,] cellsList; // プレハブのインスタンス格納用配列
        GameObject cellsParent; // セル達の親
        int h, w; // 縦横のセル数
        float cellEdgeLength; // セルの1辺の長さ

        void Start()
        {
            // SOから値を取得し、縮小の計算を行う
            h = LifeGameSO.Entity.H * LifeGameSO.Entity.DrawShrinkScale;
            if (h % 2 == 0) h--;
            w = LifeGameSO.Entity.W * LifeGameSO.Entity.DrawShrinkScale;
            if (w % 2 == 0) w--;
            cellEdgeLength = LifeGameSO.Entity.CellEdgeLength / LifeGameSO.Entity.DrawShrinkScale;

            // フィールド作成、インスタンス格納用配列の作成
            field = new int[h, w];
            cellsList = new GameObject[h, w];

            // セル達の親を生成
            cellsParent = Instantiate(LifeGameSO.Entity.CellsParent);

            // 初期配置を読み込み
            foreach (Vector2 pos in LifeGameSO.Entity.FirstStateInput)
            {
                field[-(int)pos.y + (h - 1) / 2, (int)pos.x + (w - 1) / 2] = 1;
            }

            // 描画
            FirstDraw(field);

            // シミュレーション開始
            StartCoroutine(Simulate());
        }

        IEnumerator Simulate()
        {
            while (true)
            {
                yield return new WaitForSeconds(LifeGameSO.Entity.SimulationInterval);

                List<Vector2> memoToLive = new List<Vector2>(); // メモ用：変化した箇所だけ記録する。死=>生
                List<Vector2> memoToDie = new List<Vector2>();  // メモ用：変化した箇所だけ記録する。生=>死
                for (int i = 0; i < h; i++)
                {
                    for (int j = 0; j < w; j++)
                    {
                        #region 全てのセルについて、次の状態を決定し、メモっておく。
                        if (field[i, j] == 0)
                        {
                            // 死んでいるセル（＝このセル）に隣接する生きたセルの数をカウント
                            int dieCount = 0;
                            #region フィールドの外には何もないものとし、カウントを増やさない。
                            if (i >= 1     && j >= 1     && field[i - 1, j - 1] == 1) dieCount++;
                            if (i >= 1                   && field[i - 1, j    ] == 1) dieCount++;
                            if (i >= 1     && j <= w - 2 && field[i - 1, j + 1] == 1) dieCount++;
                            if (              j >= 1     && field[i    , j - 1] == 1) dieCount++;
                            if (              j <= w - 2 && field[i    , j + 1] == 1) dieCount++;
                            if (i <= h - 2 && j >= 1     && field[i + 1, j - 1] == 1) dieCount++;
                            if (i <= h - 2               && field[i + 1, j    ] == 1) dieCount++;
                            if (i <= h - 2 && j <= w - 2 && field[i + 1, j + 1] == 1) dieCount++;
                            #endregion

                            // 誕生：死んでいるセルに隣接する生きたセルがちょうど3つあれば、次の世代が誕生する。
                            if (dieCount == 3)
                            {
                                memoToLive.Add(new Vector2(i, j));
                            }
                        }
                        else
                        {
                            // 生きているセル（＝このセル）に隣接する生きたセルの数をカウント
                            int liveCount = 0;
                            #region フィールドの外には何もないものとし、カウントを増やさない。
                            if (i >= 1     && j >= 1     && field[i - 1, j - 1] == 1) liveCount++;
                            if (i >= 1                   && field[i - 1, j    ] == 1) liveCount++;
                            if (i >= 1     && j <= w - 2 && field[i - 1, j + 1] == 1) liveCount++;
                            if (              j >= 1     && field[i    , j - 1] == 1) liveCount++;
                            if (              j <= w - 2 && field[i    , j + 1] == 1) liveCount++;
                            if (i <= h - 2 && j >= 1     && field[i + 1, j - 1] == 1) liveCount++;
                            if (i <= h - 2               && field[i + 1, j    ] == 1) liveCount++;
                            if (i <= h - 2 && j <= w - 2 && field[i + 1, j + 1] == 1) liveCount++;
                            #endregion

                            // 過疎：生きているセルに隣接する生きたセルが1つ以下ならば、過疎により死滅する。
                            // 過密：生きているセルに隣接する生きたセルが4つ以上ならば、過密により死滅する。
                            // 生存：生きているセルに隣接する生きたセルが2つか3つならば、次の世代でも生存する。
                            if (liveCount <= 1 || liveCount >= 4)
                            {
                                memoToDie.Add(new Vector2(i, j));
                            }
                        }
                        #endregion
                    }
                }

                // 描画、及びfieldの更新
                Draw(memoToLive, memoToDie);
            }
        }

        // fieldの情報を元に、シーンにセル達を描画する。黒が死、白が生
        void FirstDraw(int[,] field)
        {
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    Vector2 pos = new Vector2((j - (w - 1) / 2) * cellEdgeLength, (i - (h - 1) / 2) * -cellEdgeLength);

                    if (field[i, j] == 0)
                    {
                        GameObject newCell = Instantiate(LifeGameSO.Entity.DiedCellPrefab, pos, Quaternion.identity, cellsParent.transform);
                        newCell.transform.localScale = Vector3.one * cellEdgeLength;
                        cellsList[i, j] = newCell;
                    }
                    else
                    {
                        GameObject newCell = Instantiate(LifeGameSO.Entity.LivingCellPrefab, pos, Quaternion.identity, cellsParent.transform);
                        newCell.transform.localScale = Vector3.one * cellEdgeLength;
                        cellsList[i, j] = newCell;
                    }
                }
            }
        }

        // 変化した場所を受け取り、シーンにセル達を描画する。黒が死、白が生
        void Draw(List<Vector2> memoToLive, List<Vector2> memoToDie)
        {
            foreach(Vector2 memoPos in memoToLive)
            {
                int i = (int)memoPos.x;
                int j = (int)memoPos.y;

                Destroy(cellsList[i, j]);

                Vector2 pos = new Vector2((j - (w - 1) / 2) * cellEdgeLength, (i - (h - 1) / 2) * -cellEdgeLength);
                GameObject newCell = Instantiate(LifeGameSO.Entity.LivingCellPrefab, pos, Quaternion.identity, cellsParent.transform);
                newCell.transform.localScale = Vector3.one * cellEdgeLength;
                cellsList[i, j] =  newCell;

                field[i, j] = 1;
            }

            foreach (Vector2 memoPos in memoToDie)
            {
                int i = (int)memoPos.x;
                int j = (int)memoPos.y;

                Destroy(cellsList[i, j]);

                Vector2 pos = new Vector2((j - (w - 1) / 2) * cellEdgeLength, (i - (h - 1) / 2) * -cellEdgeLength);
                GameObject newCell = Instantiate(LifeGameSO.Entity.DiedCellPrefab, pos, Quaternion.identity, cellsParent.transform);
                newCell.transform.localScale = Vector3.one * cellEdgeLength;
                cellsList[i, j] = newCell;

                field[i, j] = 0;
            }
        }
    }
}
