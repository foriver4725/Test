using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slerp
{
    public class Vector3Lerp : MonoBehaviour
    {
        // 線形補間の始点
        [SerializeField] private Transform _from;

        // 線形補間の終点
        [SerializeField] private Transform _to;

        // 移動時間[s]
        [SerializeField] private float _duration = 1;

        private void Update()
        {
            // 始点・終点の位置取得
            var a = _from.position;
            var b = _to.position;

            // 補間位置計算
            var t = Mathf.PingPong(Time.time / _duration, 1);

            // 補間位置を反映
            transform.position = Vector3.Lerp(a, b, t);
        }
    }
}
