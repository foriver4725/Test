using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StructureOfGameProgram2
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float chasingSpeed; // 敵がプレイヤーを追いかけるスピード
        [SerializeField] private float notChasingSpeed; // 敵がプレイヤーから逃げるスピード
        private GameObject player;
        private Rigidbody enemyRb;

        void Start()
        {
            // プレハブにシーン内のGameObjectはアタッチできない
            player = GameObject.Find("Player");
            enemyRb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (!GameManager.Instance.IsGameActive)
            {
                Destroy(gameObject);
            }
            else
            {
                // 敵の運動ベクトルを計算(y方向には移動しないようにする)
                Vector3 playerVec = new Vector3(player.transform.position.x, 0, player.transform.position.z);
                Vector3 enemyVec = new Vector3(transform.position.x, 0, transform.position.z);
                Vector3 moveVec = (playerVec - enemyVec).normalized;

                Vector3 posVec = enemyRb.position;
                // IsChasingがtrueならプレイヤーを追いかける，falseならプレイヤーから逃げるようにする
                float speedVec = GameManager.Instance.IsChasing ? chasingSpeed : -notChasingSpeed;
                // 移動(Rigidbody.positionを変化させて移動させること！(物理演算の後に移動してくれる))
                enemyRb.MovePosition(posVec + moveVec * (speedVec * Time.deltaTime));
            }
        }
    }
}
