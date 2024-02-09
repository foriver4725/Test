using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slerp
{
    public class Vector3Slerp : MonoBehaviour
    {
        // ���ʐ��`��Ԃ̎n�_
        [SerializeField] private Transform _from;

        // ���ʐ��`��Ԃ̏I�_
        [SerializeField] private Transform _to;

        // �ړ�����[s]
        [SerializeField] private float _duration = 1;

        // �~�^���̒��S�_
        [SerializeField] private Transform _sphereCenter;

        private void Update()
        {
            // �n�_�E�I�_�̈ʒu�擾
            var a = _from.position;
            var b = _to.position;

            // ��Ԉʒu�v�Z
            var t = Mathf.PingPong(Time.time / _duration, 1);

            // �~�^���̒��S�_�擾
            var center = _sphereCenter.position;

            // �~�^��������O�ɒ��S�_�����_�ɗ���悤�Ɏn�_�E�I�_���ړ�
            a -= center;
            b -= center;

            // ���_���S�ŉ~�^��
            var slerpPos = Vector3.Slerp(a, b, t);

            // ���S�_�������炵���ʒu��߂�
            slerpPos += center;

            // ��Ԉʒu�𔽉f
            transform.position = slerpPos;
        }
    }
}
