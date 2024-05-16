using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Particle
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        [SerializeField] float jumpPower = 10f;

        Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += -transform.right * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position += -transform.forward * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += transform.right * speed * Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            }
        }
    }
}
