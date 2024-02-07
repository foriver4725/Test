using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaternionSlerp : MonoBehaviour
{
    public Transform from;
    public Transform to;
    public float duration = 1f;

    void Update()
    {
        transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, Time.time / duration);
    }
}
