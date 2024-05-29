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
        //      V：終端速度
        //
        //      k = |f| / V

        [SerializeField, Tooltip("目標とする速度")] float targetVel;
        [SerializeField, Tooltip("この数値が大きいほど、目標の速度に早く達する")] float forcePower;
        [SerializeField, Tooltip("入力がない時、速度を0にする")] bool isStopSelfly;
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