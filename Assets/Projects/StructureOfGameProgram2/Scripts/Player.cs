using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StructureOfGameProgram2
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float speed; // �v���C���[�̃X�s�[�h�i���ɂ��邱�Ɓj
        [SerializeField] private float jumpSpeed; // �v���C���[�̃W�����v�́i���ɂ��邱�Ɓj
        [SerializeField] private float powerUpTime;// �v���C���[���p���[�A�b�v���Ă����鎞�Ԑ���
        [SerializeField, Range(0f, 1f)] private float blinkInterval; // �_�ł���Ԋu�i�b�j
        private Renderer playerRd; // �v���C���[�̃����_���[�R���|�[�l���g
        private Rigidbody playerRb;
        private bool isBlinking = false; // �v���C���[���_�Œ��i���p���[�A�b�v���j���ǂ����̃t���O

        [SerializeField] AudioClip jumpSound; // �W�����v������
        [SerializeField] AudioClip hitByEnemySound; // �p���[�A�b�v���łȂ����ɁC�G�ɂԂ���ꂽ��
        [SerializeField] AudioClip hitEnemySound; // �p���[�A�b�v���ɁC�G�ɂԂ�������
        [SerializeField] AudioClip itemGetSound; // �A�C�e�����Q�b�g������
        private AudioSource playerAudio;

        void Start()
        {
            playerRd = GetComponent<Renderer>(); // �v���C���[�̃����_���[���i�[
            playerRb = GetComponent<Rigidbody>(); // �v���C���[��Ridigbody���i�[
            playerAudio = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (GameManager.Instance.IsGameActive)
            {
                // ���͌��m
                float inputX = Input.GetAxis("Horizontal"); // ���Ɓ��̌��m�i-1f~1f�j
                float inputZ = Input.GetAxis("Vertical"); // ���Ɓ��̌��m�i-1f~1f�j

                // �ړ�
                Vector3 movement = new Vector3(inputX, 0, inputZ); // �v���C���[�̉^���x�N�g�����Z�b�g
                if (movement.magnitude > 1.0f)
                {
                    movement.Normalize(); // �^���x�N�g���𐳋K��
                }
                Vector3 position = playerRb.position; // �v���C���[�̈ʒu�x�N�g�����擾
                                                      // x,z�ړ��i�ʒu�x�N�g���ɉ^���x�N�g���𑫂��j�iy����]�݂̂ɌŒ肵�Ă���̂ŁCAddForce����xz�����ɂ͓����Ȃ��j
                playerRb.MovePosition(position + movement * (speed * Time.deltaTime));
                // �W�����v�{�^���������ꂽ���n�ʂɐڐG���Ă���Ȃ�Cy�ړ��iImpulse���[�h�ɂ��邱�Ɓj
                if (Input.GetButtonDown("Jump") && Mathf.Abs(playerRb.position.y - 0.5f) <= 0.01f)
                {
                    playerRb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);

                    playerAudio.PlayOneShot(jumpSound, 1.0f); // �W�����v���������C����1.0f�ōĐ�
                }

                // �`�F�C�X���[�h���I�t�ɂȂ����Ƃ��ɁC��񂾂�Blink()�R���[�`�����I���ɂ���B
                if (!GameManager.Instance.IsChasing && !isBlinking)
                {
                    StartCoroutine(Blink());
                    isBlinking = true;
                }
            }
        }

        // �v���C���[�ɑ΂��ēG
        // �i�wEnemy�x�Ƃ����^�O���t�����Q�[���I�u�W�F�N�g�j
        // ���ڐG�����Ƃ��C�G������
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                // �p���[�A�b�v��ԂȂ�X�R�A�Z
                if (!GameManager.Instance.IsChasing)
                {
                    GameManager.Instance.Score += GameManager.Instance.Point;

                    Destroy(collision.gameObject);

                    playerAudio.PlayOneShot(hitEnemySound, 1.0f); // �p���[�A�b�v���ɁC�G�ɂԂ����������C����1.0f�ōĐ�
                }
                // �����łȂ��Ȃ�Q�[���I�[�o�[
                else
                {
                    GameManager.Instance.IsGameActive = false;

                    playerAudio.PlayOneShot(hitByEnemySound, 1.0f); // �p���[�A�b�v���łȂ����ɁC�G�ɂԂ���ꂽ�����C����1.0f�ōĐ�
                }
            }
        }

        // �v���C���[���p���[�A�b�v�A�C�e��
        // �i�wPowerUp�x�Ƃ����^�O���t�����Q�[���I�u�W�F�N�g�C�g���K�[�Ƃ��Đݒ肵�Ă��邽�ߓ����蔻��͂Ȃ��j
        // �͈̔͂ɓ������Ƃ��C�p���[�A�b�v�A�C�e���������C�`�F�C�X��Ԃ��I�t�ɂ���
        // �����Ƀp���[�A�b�v���؂��܂ł̃J�E���g�_�E�����J�n
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "PowerUp" && GameManager.Instance.IsGameActive)
            {
                Destroy(other.gameObject);
                GameManager.Instance.IsChasing = false;
                StartCoroutine(PowerUpCountDown(powerUpTime));

                playerAudio.PlayOneShot(itemGetSound, 1.0f); // �A�C�e�����Q�b�g���������C����1.0f�ōĐ�
            }
        }

        // �p���[�A�b�v�̎��Ԑ������߂�����C�`�F�C�X��Ԃ��I���ɂ���
        IEnumerator PowerUpCountDown(float powerUpTime)
        {
            yield return new WaitForSeconds(powerUpTime);
            GameManager.Instance.IsChasing = true;
            yield break;
        }

        IEnumerator Blink()
        {
            bool isVisible = true;

            // blikInterval�i�b�j���ƂɈȉ����J��Ԃ�
            while (true)
            {
                // �`�F�C�X���[�h���I���ɖ߂�����C�v���C���[��\����Ԃɖ߂��ăR���[�`�����~
                if (GameManager.Instance.IsChasing)
                {
                    playerRd.enabled = true;
                    yield break;
                }

                if (isVisible) // �����\���Ȃ�
                {
                    yield return new WaitForSeconds(blinkInterval);
                    playerRd.enabled = false; // ��\��
                    isVisible = false; // ���͕\��
                }
                else // ������\���Ȃ�
                {
                    yield return new WaitForSeconds(blinkInterval);
                    playerRd.enabled = true; // �\��
                    isVisible = true; // ���͔�\��
                }
            }
        }
    }
}
