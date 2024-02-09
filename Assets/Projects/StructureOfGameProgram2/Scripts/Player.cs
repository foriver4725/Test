using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StructureOfGameProgram2
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float speed; // プレイヤーのスピード（正にすること）
        [SerializeField] private float jumpSpeed; // プレイヤーのジャンプ力（正にすること）
        [SerializeField] private float powerUpTime;// プレイヤーがパワーアップしていられる時間制限
        [SerializeField, Range(0f, 1f)] private float blinkInterval; // 点滅する間隔（秒）
        private Renderer playerRd; // プレイヤーのレンダラーコンポーネント
        private Rigidbody playerRb;
        private bool isBlinking = false; // プレイヤーが点滅中（＝パワーアップ中）かどうかのフラグ

        [SerializeField] AudioClip jumpSound; // ジャンプした音
        [SerializeField] AudioClip hitByEnemySound; // パワーアップ中でない時に，敵にぶつかられた音
        [SerializeField] AudioClip hitEnemySound; // パワーアップ中に，敵にぶつかった音
        [SerializeField] AudioClip itemGetSound; // アイテムをゲットした音
        private AudioSource playerAudio;

        void Start()
        {
            playerRd = GetComponent<Renderer>(); // プレイヤーのレンダラーを格納
            playerRb = GetComponent<Rigidbody>(); // プレイヤーのRidigbodyを格納
            playerAudio = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (GameManager.Instance.IsGameActive)
            {
                // 入力検知
                float inputX = Input.GetAxis("Horizontal"); // ←と→の検知（-1f~1f）
                float inputZ = Input.GetAxis("Vertical"); // ↓と↑の検知（-1f~1f）

                // 移動
                Vector3 movement = new Vector3(inputX, 0, inputZ); // プレイヤーの運動ベクトルをセット
                if (movement.magnitude > 1.0f)
                {
                    movement.Normalize(); // 運動ベクトルを正規化
                }
                Vector3 position = playerRb.position; // プレイヤーの位置ベクトルを取得
                                                      // x,z移動（位置ベクトルに運動ベクトルを足す）（y軸回転のみに固定しているので，AddForceだとxz方向には動かない）
                playerRb.MovePosition(position + movement * (speed * Time.deltaTime));
                // ジャンプボタンが押されたかつ地面に接触しているなら，y移動（Impulseモードにすること）
                if (Input.GetButtonDown("Jump") && Mathf.Abs(playerRb.position.y - 0.5f) <= 0.01f)
                {
                    playerRb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);

                    playerAudio.PlayOneShot(jumpSound, 1.0f); // ジャンプした音を，音量1.0fで再生
                }

                // チェイスモードがオフになったときに，一回だけBlink()コルーチンをオンにする。
                if (!GameManager.Instance.IsChasing && !isBlinking)
                {
                    StartCoroutine(Blink());
                    isBlinking = true;
                }
            }
        }

        // プレイヤーに対して敵
        // （『Enemy』というタグが付いたゲームオブジェクト）
        // が接触したとき，敵を消す
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                // パワーアップ状態ならスコア可算
                if (!GameManager.Instance.IsChasing)
                {
                    GameManager.Instance.Score += GameManager.Instance.Point;

                    Destroy(collision.gameObject);

                    playerAudio.PlayOneShot(hitEnemySound, 1.0f); // パワーアップ中に，敵にぶつかった音を，音量1.0fで再生
                }
                // そうでないならゲームオーバー
                else
                {
                    GameManager.Instance.IsGameActive = false;

                    playerAudio.PlayOneShot(hitByEnemySound, 1.0f); // パワーアップ中でない時に，敵にぶつかられた音を，音量1.0fで再生
                }
            }
        }

        // プレイヤーがパワーアップアイテム
        // （『PowerUp』というタグが付いたゲームオブジェクト，トリガーとして設定しているため当たり判定はない）
        // の範囲に入ったとき，パワーアップアイテムを消し，チェイス状態をオフにする
        // 同時にパワーアップが切れるまでのカウントダウンを開始
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "PowerUp" && GameManager.Instance.IsGameActive)
            {
                Destroy(other.gameObject);
                GameManager.Instance.IsChasing = false;
                StartCoroutine(PowerUpCountDown(powerUpTime));

                playerAudio.PlayOneShot(itemGetSound, 1.0f); // アイテムをゲットした音を，音量1.0fで再生
            }
        }

        // パワーアップの時間制限が過ぎたら，チェイス状態をオンにする
        IEnumerator PowerUpCountDown(float powerUpTime)
        {
            yield return new WaitForSeconds(powerUpTime);
            GameManager.Instance.IsChasing = true;
            yield break;
        }

        IEnumerator Blink()
        {
            bool isVisible = true;

            // blikInterval（秒）ごとに以下を繰り返す
            while (true)
            {
                // チェイスモードがオンに戻ったら，プレイヤーを表示状態に戻してコルーチンを停止
                if (GameManager.Instance.IsChasing)
                {
                    playerRd.enabled = true;
                    yield break;
                }

                if (isVisible) // もし表示なら
                {
                    yield return new WaitForSeconds(blinkInterval);
                    playerRd.enabled = false; // 非表示
                    isVisible = false; // 次は表示
                }
                else // もし非表示なら
                {
                    yield return new WaitForSeconds(blinkInterval);
                    playerRd.enabled = true; // 表示
                    isVisible = true; // 次は非表示
                }
            }
        }
    }
}
