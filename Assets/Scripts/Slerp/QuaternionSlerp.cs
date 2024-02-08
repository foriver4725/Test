using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaternionSlerp : MonoBehaviour
{
    [SerializeField] Transform from;
    [SerializeField] Transform to;
    [SerializeField] float duration = 1f;

    void Update()
    {
        float t = Mathf.PingPong(Time.time / duration, 1);
        transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, t);
    }
}
