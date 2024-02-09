using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StructureOfGameProgram2
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float chasingSpeed; // �G���v���C���[��ǂ�������X�s�[�h
        [SerializeField] private float notChasingSpeed; // �G���v���C���[���瓦����X�s�[�h
        private GameObject player;
        private Rigidbody enemyRb;

        void Start()
        {
            // �v���n�u�ɃV�[������GameObject�̓A�^�b�`�ł��Ȃ�
            player = GameObject.Find("Player");
            enemyRb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (!GameManager.Instance.IsGameActive)
            {
                Destroy(gameObject);
            }
            else
            {
                // �G�̉^���x�N�g�����v�Z(y�����ɂ͈ړ����Ȃ��悤�ɂ���)
                Vector3 playerVec = new Vector3(player.transform.position.x, 0, player.transform.position.z);
                Vector3 enemyVec = new Vector3(transform.position.x, 0, transform.position.z);
                Vector3 moveVec = (playerVec - enemyVec).normalized;

                Vector3 posVec = enemyRb.position;
                // IsChasing��true�Ȃ�v���C���[��ǂ�������Cfalse�Ȃ�v���C���[���瓦����悤�ɂ���
                float speedVec = GameManager.Instance.IsChasing ? chasingSpeed : -notChasingSpeed;
                // �ړ�(Rigidbody.position��ω������Ĉړ������邱�ƁI(�������Z�̌�Ɉړ����Ă����))
                enemyRb.MovePosition(posVec + moveVec * (speedVec * Time.deltaTime));
            }
        }
    }
}
