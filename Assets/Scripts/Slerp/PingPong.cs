using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PingPong : MonoBehaviour
{
    [SerializeField] float length = 5f;
    [SerializeField] float amplitude = 5f; // U•
    [SerializeField] float period = 2f; // ˆê‰•œ‚·‚éüŠúi•bj
    [SerializeField] float phase = 0.25f; // ˆÊ‘Ši‚P‚Å‚Pü•ªj

    enum Axis { X, Y, Z };
    [SerializeField] Axis axis = Axis.Y;

    void Update()
    {
        Vector3 pos = transform.position;

        // ˆê’è‘¬“x‚Å 0 ~ length ‚ğ‰•œ‚·‚é
        // pos.y = Mathf.PingPong(Time.time, length);

        // U•AüŠúAˆÊ‘Š‚Ìİ’è
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
                Debug.Log("axis‚ªİ’è‚³‚ê‚Ä‚¢‚Ü‚¹‚ñ");
                break;
        }

        transform.position = pos;
    }
}
