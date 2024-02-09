using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slerp
{
    public class Vector3Slerp : MonoBehaviour
    {
        // 球面線形補間の始点
        [SerializeField] private Transform _from;

        // 球面線形補間の終点
        [SerializeField] private Transform _to;

        // 移動時間[s]
        [SerializeField] private float _duration = 1;

        // 円運動の中心点
        [SerializeField] private Transform _sphereCenter;

        private void Update()
        {
            // 始点・終点の位置取得
            var a = _from.position;
            var b = _to.position;

            // 補間位置計算
            var t = Mathf.PingPong(Time.time / _duration, 1);

            // 円運動の中心点取得
            var center = _sphereCenter.position;

            // 円運動させる前に中心点が原点に来るように始点・終点を移動
            a -= center;
            b -= center;

            // 原点中心で円運動
            var slerpPos = Vector3.Slerp(a, b, t);

            // 中心点だけずらした位置を戻す
            slerpPos += center;

            // 補間位置を反映
            transform.position = slerpPos;
        }
    }
}
