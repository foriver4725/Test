using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interface
{
    /// <summary>
    /// ダメージ処理メソッドを定義するインターフェース
    /// </summary>
    public interface IDamagable
    {
        bool Invincible { get; }
        void Damage(float damage);
    }


    /// <summary>
    /// 敵Aの制御を行うクラス
    /// </summary>
    public class EnemyA : MonoBehaviour, IDamagable    // IDamagableを継承
    {
        private bool _invincible;    // 無敵判定

        /// <value>無敵判定の読み込み専用プロパティ</value>
        public bool Invincible => _invincible;

        /// <summary>
        /// IDamagableで実装されるダメージを受ける処理
        /// </summary>
        /// <param name="damage">受けるダメージ</param>
        public void Damage(float damage)
        {

        }
    }


    /// <summary>
    /// プレイヤーを制御するクラス
    /// </summary>
    public class Player : MonoBehaviour
    {
        private float _atkPower;    // 攻撃力

        /// <summary>
        /// 攻撃によってダメージを与える処理
        /// </summary>
        /// <param name="enemyObj">攻撃対象の敵オブジェクト</param>
        private void Attack(GameObject enemyObj)
        {
            IDamagable damagable = enemyObj.GetComponent<IDamagable>();    // IDamagableを取得

            if (damagable.Invincible) return;    // 無敵なら無視する

            damagable.Damage(_atkPower);
        }
    }

    /// <summary>
    /// 説明だよ
    /// </summary>
    /// <param name="a">aという引数</param>
    /// <param name="b">bという引数</param>
    /// <return>戻り値だよ</return>
    /// <remarks>追記だよ</remarks>
    /// <value>プロパティについて説明するよ</value>
    public class Temp : MonoBehaviour
    {
        /// <summary>
        /// 2数の大小を比較する。
        /// </summary>
        /// <param name="a">左辺の整数</param>
        /// <param name="b">右辺の変数</param>
        /// <returns>左が右よりも大きいときのみtrueを返す。</returns>
        /// <remarks>等しいときはfalseを返すことに注意！</remarks>
        bool Compare(int a, int b)
        {
            if (a > b) return true;
            else return false;
        }

        /// <value>テスト用の、小数のプロパティ</value>
        public float Property { get; set; } = 3.0f;

        void Start()
        {
            Debug.Log(Compare(100, 30));
            Debug.Log(Property *= 2);
        }
    }
}
