using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slerp
{
    public class Vector3Lerp : MonoBehaviour
    {
        // ���`��Ԃ̎n�_
        [SerializeField] private Transform _from;

        // ���`��Ԃ̏I�_
        [SerializeField] private Transform _to;

        // �ړ�����[s]
        [SerializeField] private float _duration = 1;

        private void Update()
        {
            // �n�_�E�I�_�̈ʒu�擾
            var a = _from.position;
            var b = _to.position;

            // ��Ԉʒu�v�Z
            var t = Mathf.PingPong(Time.time / _duration, 1);

            // ��Ԉʒu�𔽉f
            transform.position = Vector3.Lerp(a, b, t);
        }
    }
}
