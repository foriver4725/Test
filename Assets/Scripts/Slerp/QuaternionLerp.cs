using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaternionLerp : MonoBehaviour
{
    [SerializeField] Transform from;
    [SerializeField] Transform to;
    [SerializeField] float duration = 1f;

    void Update()
    {
        float t = Mathf.PingPong(Time.time / duration, 1);
        transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, t);
    }
}
