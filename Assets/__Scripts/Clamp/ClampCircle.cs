using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clamp
{
    public class ClampSircle : MonoBehaviour
    {
        // �ړ��͈͂̍ő唼�a
        [SerializeField] float maxRadius = 1;

        private void Update()
        {
            InputGet();

            // �w�肳�ꂽ���a�̉~���Ɉʒu���ۂ߂�
            Vector3 pos = transform.position;
            Vector3 clampedPos = Vector2.ClampMagnitude(pos, maxRadius);
            transform.position = new Vector3(clampedPos.x, clampedPos.y, pos.z);
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
