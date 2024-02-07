using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampSphere : MonoBehaviour
{
    // à⁄ìÆîÕàÕÇÃç≈ëÂîºåa
    [SerializeField] float maxRadius = 1;

    private void Update()
    {
        InputGet();

        // éwíËÇ≥ÇÍÇΩîºåaÇÃãÖì‡Ç…à íuÇä€ÇﬂÇÈ
        Vector3 pos = transform.position;
        Vector3 clampedPos = Vector3.ClampMagnitude(pos, maxRadius);
        transform.position = clampedPos;
    }

    void InputGet()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * 10 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * 10 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * 10 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * 10 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * 10 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * 10 * Time.deltaTime;
        }
    }
}
