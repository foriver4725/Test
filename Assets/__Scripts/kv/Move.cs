using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ex;

namespace kv
{
    public class Move : MonoBehaviour
    {
        //      kv <---(Obj)---> f      (k > 0)
        //
        //      V�F�I�[���x
        //
        //      k = |f| / V

        [SerializeField, Tooltip("�ڕW�Ƃ��鑬�x")] float targetVel;
        [SerializeField, Tooltip("���̐��l���傫���قǁA�ڕW�̑��x�ɑ����B����")] float forcePower;
        [SerializeField, Tooltip("���͂��Ȃ����A���x��0�ɂ���")] bool isStopSelfly;
        Rigidbody rb;
        Vector3 force;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            Vector2 _f = IO.AxisMove(isRaw: true);
            force = forcePower * new Vector3(_f.x, 0, _f.y);
            if (isStopSelfly && force == Vector3.zero) rb.velocity = Vector3.zero;

            rb.velocity.magnitude.Show("Velocity");
        }

        void FixedUpdate()
        {
            float k = force.magnitude / targetVel;
            rb.AddForce(force);
            rb.AddForce(-k * rb.velocity);
        }
    }
}