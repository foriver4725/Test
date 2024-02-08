using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PingPong : MonoBehaviour
{
    [SerializeField] float length = 5f;
    [SerializeField] float amplitude = 5f; // �U��
    [SerializeField] float period = 2f; // �ꉝ����������i�b�j
    [SerializeField] float phase = 0.25f; // �ʑ��i�P�łP�����j

    enum Axis { X, Y, Z };
    [SerializeField] Axis axis = Axis.Y;

    void Update()
    {
        Vector3 pos = transform.position;

        // ��葬�x�� 0 ~ length ����������
        // pos.y = Mathf.PingPong(Time.time, length);

        // �U���A�����A�ʑ��̐ݒ�
        switch (axis)
        {
            case Axis.X:
                pos.x = Mathf.PingPong(4 * amplitude * (Time.time / period + phase + 0.25f), 2 * amplitude) - amplitude;
                break;

            case Axis.Y:
                pos.y = Mathf.PingPong(4 * amplitude * (Time.time / period + phase + 0.25f), 2 * amplitude) - amplitude;
                break;

            case Axis.Z:
                pos.z = Mathf.PingPong(4 * amplitude * (Time.time / period + phase + 0.25f), 2 * amplitude) - amplitude;
                break;

            default:
                Debug.Log("axis���ݒ肳��Ă��܂���");
                break;
        }

        transform.position = pos;
    }
}
