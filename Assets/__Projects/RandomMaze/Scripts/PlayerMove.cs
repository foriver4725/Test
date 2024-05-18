using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomMaze
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] GameObject cam;

        Rigidbody rb;
        float x, z;
        Quaternion cameraRot, characterRot;
        bool isLookingBack = false;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            cameraRot = cam.transform.localRotation;
            characterRot = transform.localRotation;
        }

        void Update()
        {
            if (GameManager.Instance.IsClear) { GetComponent<PlayerMove>().enabled = false; }

            float xRot = Input.GetAxis("Mouse X");
            float yRot = Input.GetAxis("Mouse Y");

            cameraRot *= Quaternion.AngleAxis(-yRot * PlayerParamsSO.Entity.Sensitivity[1], Vector3.right);
            characterRot *= Quaternion.AngleAxis(xRot * PlayerParamsSO.Entity.Sensitivity[0], Vector3.up);

            // �X�y�[�X�������Ă���Ԃ͐U��Ԃ�
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyUp(KeyCode.Space))
            {
                isLookingBack = !isLookingBack;
                characterRot *= Quaternion.AngleAxis(180, Vector3.up);
            }

            //Update�̒��ō쐬�����֐����Ă�
            cameraRot = ClampRotation(cameraRot);

            cam.transform.localRotation = cameraRot;
            transform.localRotation = characterRot;

            UpdateCursorLock();

            // �S�[���̖ڂ̑O�܂Ń��[�v
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                transform.position = new Vector3(0f, 1.5f, 120f);
            }
        }

        private void FixedUpdate()
        {
            x = 0; z = 0;
            x = Input.GetAxisRaw("Horizontal"); // * speed;
            z = Input.GetAxisRaw("Vertical"); // * speed;

            // // transform.position += new Vector3(x,0,z);
            // transform.position += cam.transform.forward * z + cam.transform.right * x;

            float _speed = PlayerParamsSO.Entity.Speed;
            if (Input.GetKey(KeyCode.LeftShift)) { _speed *= PlayerParamsSO.Entity.SpeedCoef; }
            if (isLookingBack) { _speed *= -PlayerParamsSO.Entity.OnLookingBackSpeedCoef; }
            rb.velocity = (transform.forward * z + transform.right * x).normalized * Time.deltaTime * _speed;
        }

        public void UpdateCursorLock()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) { Cursor.lockState = CursorLockMode.None; }
            else if (Input.GetMouseButton(0)) { Cursor.lockState = CursorLockMode.Locked; }
        }

        //�p�x�����֐��̍쐬
        public Quaternion ClampRotation(Quaternion q)
        {
            //q = x,y,z,w (x,y,z�̓x�N�g���i�ʂƌ����j�Fw�̓X�J���[�i���W�Ƃ͖��֌W�̗ʁj)

            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1f;

            float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;

            angleX = Mathf.Clamp(angleX, PlayerParamsSO.Entity.CameraRotXLimit[0], PlayerParamsSO.Entity.CameraRotXLimit[1]);

            q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

            return q;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Goal")
            {
                GameManager.Instance.IsClear = true;
            }
        }
    }
}
