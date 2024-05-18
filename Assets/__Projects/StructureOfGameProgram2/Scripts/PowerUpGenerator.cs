using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StructureOfGameProgram2
{
    public class PowerUpGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject player; //プレイヤーをアタッチ
        [SerializeField] private GameObject powerUpPrefab; // 敵のプレハブをアタッチ
        [SerializeField] private int spawnNum; // 何個生成するか
        [SerializeField] private float spawnRange; // プレイヤーを中心とした，1辺2*spawnRangeの正方形内に生成される
        [SerializeField] private float banRange; // プレイヤーを中心とした，この半径の円内には生成されない（中心座標について）
        private bool isGenerated = false; // パワーアップアイテムをすでに生成しているかどうか

        void Update()
        {
            // パワーアップアイテムの数が生成上限に達するまで毎フレームパワーアップアイテムを生成（ただし達したらもう生成しない）
            if (GameObject.FindGameObjectsWithTag("PowerUp").Length < spawnNum && GameManager.Instance.IsGameActive && !isGenerated)
            {
                // 条件を満たすまでパワーアップアイテムの座標を生成，評価
                while (true)
                {
                    float x = Random.Range(-spawnRange, spawnRange);
                    float z = Random.Range(-spawnRange, spawnRange);
                    Vector3 newPowerUpPos = new Vector3(x, 0.5f, z);
                    // もしプレイヤーとパワーアップアイテムの距離がプレイヤーの直径未満でないなら条件クリア
                    if (Vector3.Distance(player.transform.position, newPowerUpPos) > banRange)
                    {
                        // パワーアップアイテムを1つ生成(まれに重なるが，処理が面倒なので無視する)
                        Instantiate(powerUpPrefab, newPowerUpPos, Quaternion.identity);
                        break;
                    }
                }
            }
            else
            {
                isGenerated = true;
            }
        }
    }
}
