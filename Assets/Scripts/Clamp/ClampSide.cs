using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clamp
{
    public class ClampSide : MonoBehaviour
    {
        // x²•ûŒü‚ÌˆÚ“®”ÍˆÍ‚ÌÅ¬’l
        [SerializeField] float minX = -1;

        // x²•ûŒü‚ÌˆÚ“®”ÍˆÍ‚ÌÅ‘å’l
        [SerializeField] float maxX = 1;

        private void Update()
        {
            InputGet();

            // x²•ûŒü‚ÌˆÚ“®”ÍˆÍ§ŒÀ
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, minX, maxX); // minX ~ maxX
                                                    // pos.x = Mathf.Clamp01(pos.x); // 0 ~ 1
            transform.position = pos;
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
}
