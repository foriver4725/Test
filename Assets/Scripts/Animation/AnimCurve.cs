using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animation
{
    public class AnimCurve : MonoBehaviour
    {
        [SerializeField] AnimationCurve curve; // �܂�Inspector�ɃJ�[�u��\��������
        [SerializeField] float amplitude = 3f;

        void Update()
        {
            // Evaluate�ŃJ�[�u�̐��l���擾�ł���
            // �����̓O���t�̎��Ԏ�
            // Time.timeSinceLevelLoad�iTime.time�Ǝ��Ă��邪�A�V�[���؂�ւ�������0�ɖ߂�j
            transform.position = Vector3.up * amplitude * (curve.Evaluate(Time.timeSinceLevelLoad));
        }
    }
}
