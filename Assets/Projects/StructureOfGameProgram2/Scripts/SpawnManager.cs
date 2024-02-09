using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StructureOfGameProgram2
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject player; //プレイヤーをアタッチ
        [SerializeField] private GameObject enemyPrefab; // 敵のプレハブをアタッチ
        [SerializeField] private int spawnNum; // 何個生成するか
        [SerializeField] private float spawnRange; // プレイヤーを中心とした，1辺2*spawnRangeの正方形内に生成される
        [SerializeField] private float banRange; // プレイヤーを中心とした，この半径の円内には生成されない（中心座標について）

        void Update()
        {
            // 敵の数が生成上限に達するまで毎フレーム敵を生成
            if (GameObject.FindGameObjectsWithTag("Enemy").Length < spawnNum && GameManager.Instance.IsGameActive)
            {
                // 条件を満たすまで敵の座標を生成，評価
                while (true)
                {
                    float x = Random.Range(-spawnRange, spawnRange);
                    float z = Random.Range(-spawnRange, spawnRange);
                    Vector3 newEnemyPos = new Vector3(x, 0.5f, z);
                    // もしプレイヤーと敵の距離がプレイヤーの直径未満でないなら条件クリア
                    if (Vector3.Distance(player.transform.position, newEnemyPos) > banRange)
                    {
                        // 敵を1体生成(まれに重なるが，処理が面倒なので無視する)
                        Instantiate(enemyPrefab, newEnemyPos, Quaternion.identity);
                        break;
                    }
                }
            }
        }
    }
}
