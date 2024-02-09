using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace GoHome
{
    // �v���C���[�̑傫���Ɋւ�炸�C�J�����̑��Έʒu���Œ�i���ɁC�����������Ȃ�j
    // �S�[��������C�J�������ʐ^�̊p�x�Ɍ�������
    // 6,7���C�ړ����������̕���������
    // ����Ȃ����]����ƁC�����_�����O���Ԃ��
    // �W�����v�̒��_�ŁC�u���x��0�v����ɂȂ��Ă��܂�Ȃ����H
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] float speed = 10;
        [SerializeField] float jumpPower = 5;
        [SerializeField] bool isFryable = false;
        [SerializeField] bool isPlayer4 = false;
        [SerializeField] bool isPlayer5 = false;
        [SerializeField] float player5SpeedMul = 3;
        [SerializeField] float rotX = 0; // ������ς���ۂ́C�I�C���[�px�̌Œ�l
        [SerializeField] bool isRotYAdjust = false; // ������ς���ۂ́C�I�C���[�py��+90���邩�ǂ���
                                                    // �X�^�[�g�ʒu�ɋ����I�ɖ߂��C���E�̍��W(x,y,z : �ŏ��C�ő�)
        Vector2[] limit = { new Vector2(-60, 80), new Vector2(-10, 100), new Vector2(-65, 65) };
        Rigidbody rb;
        int jumpCount = 0;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            // limit�̔���
            Vector3 p = transform.position;
            if (p.x < limit[0].x || limit[0].y < p.x)
            {
                transform.position = GameManager.Instance.StartPos;
            }
            else if (p.y < limit[1].x || limit[1].y < p.y)
            {
                transform.position = GameManager.Instance.StartPos;
            }
            else if (p.z < limit[2].x || limit[2].y < p.z)
            {
                transform.position = GameManager.Instance.StartPos;
            }

            // ���[�v�{�^���̔���
            if (Input.GetKey(KeyCode.RightShift))
            {
                if (Input.GetKeyDown(KeyCode.Return) && !GameManager.Instance.IsTaking)
                {
                    transform.position = GameManager.Instance.GoalPos;
                }
                else if (Input.GetKeyDown(KeyCode.Backspace) && !GameManager.Instance.IsTaking)
                {
                    transform.position = GameManager.Instance.StartPos;
                }
            }

            // 2�i�W�����v�̃J�E���g�����Z�b�g
            if (jumpCount >= 2 && Mathf.Abs(rb.velocity.y) <= 0.00001f)
            {
                jumpCount = 0;
            }

            // ��ׂ�ꍇ�Ɣ�ׂȂ��ꍇ�Ŕ���𕪂���
            if (!isFryable && !GameManager.Instance.IsTaking)
            {
                float inputX = Input.GetAxis("Horizontal");
                float inputZ = Input.GetAxis("Vertical");
                Vector3 mov = new Vector3(inputX, 0, inputZ);

                if (mov != Vector3.zero)
                {
                    // ������ς���
                    foreach (GameObject playerBody in GameObject.FindGameObjectsWithTag("PlayerBody"))
                    {
                        Vector3 cameraAngles = Camera.main.transform.localEulerAngles;
                        cameraAngles.x = rotX;
                        if (isPlayer5 && Input.GetKey(KeyCode.LeftShift)) { cameraAngles.x += 90; } // ���΂����[�h�ɂȂ�
                        if (isRotYAdjust) { cameraAngles.y += 90; }
                        playerBody.transform.localEulerAngles = cameraAngles;
                    }
                }
                else if (isPlayer5)
                {
                    // ������ς���
                    foreach (GameObject playerBody in GameObject.FindGameObjectsWithTag("PlayerBody"))
                    {
                        Vector3 playerAngles = playerBody.transform.localEulerAngles;
                        playerAngles.x = rotX;
                        if (Input.GetKey(KeyCode.LeftShift)) { playerAngles.x += 90; } // ���΂����[�h�ɂȂ�
                        playerBody.transform.localEulerAngles = playerAngles;
                    }
                }

                Vector3 pos = transform.position;
                Vector3 MOV = Camera.main.transform.localRotation * mov;
                MOV.y = 0;
                MOV = MOV.normalized * speed;
                if (isPlayer5 && Input.GetKey(KeyCode.LeftShift)) { MOV *= player5SpeedMul; }
                rb.MovePosition(pos + MOV * Time.deltaTime);

                if (isPlayer4)
                {
                    // �{��2�i�W�����v�ł���
                    if (Input.GetKeyDown(KeyCode.Space) && jumpCount <= 1)
                    {
                        jumpCount += 1;
                        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) <= 0.00001f)
                    {
                        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                    }
                }
            }
            else if (isFryable && !GameManager.Instance.IsTaking)
            {
                float inputX = 0;
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) { inputX += 1; }
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) { inputX -= 1; }

                float inputZ = 0;
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) { inputZ += 1; }
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) { inputZ -= 1; }

                Vector3 mov = new Vector3(inputX, 0, inputZ);

                if (mov != Vector3.zero)
                {
                    // ������ς���
                    foreach (GameObject playerBody in GameObject.FindGameObjectsWithTag("PlayerBody"))
                    {
                        Vector3 cameraAngles = Camera.main.transform.localEulerAngles;
                        cameraAngles.x = rotX;
                        if (isRotYAdjust) { cameraAngles.y += 90; }
                        playerBody.transform.localEulerAngles = cameraAngles;
                    }
                }

                Vector3 pos = transform.position;
                Vector3 MOV = Camera.main.transform.localRotation * mov;
                MOV.y = 0;
                MOV = MOV.normalized * speed;

                float inputY = 0;
                if (Input.GetKey(KeyCode.Space)) { inputY += 1; }
                if (Input.GetKey(KeyCode.LeftShift)) { inputY -= 1; }

                rb.MovePosition(pos + MOV * Time.deltaTime + Vector3.up * inputY * (jumpPower * Time.deltaTime));
            }
        }
    }
}
