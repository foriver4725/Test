using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StructureOfGameProgram2
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject player; //�v���C���[���A�^�b�`
        [SerializeField] private GameObject enemyPrefab; // �G�̃v���n�u���A�^�b�`
        [SerializeField] private int spawnNum; // ���������邩
        [SerializeField] private float spawnRange; // �v���C���[�𒆐S�Ƃ����C1��2*spawnRange�̐����`���ɐ��������
        [SerializeField] private float banRange; // �v���C���[�𒆐S�Ƃ����C���̔��a�̉~���ɂ͐�������Ȃ��i���S���W�ɂ��āj

        void Update()
        {
            // �G�̐�����������ɒB����܂Ŗ��t���[���G�𐶐�
            if (GameObject.FindGameObjectsWithTag("Enemy").Length < spawnNum && GameManager.Instance.IsGameActive)
            {
                // �����𖞂����܂œG�̍��W�𐶐��C�]��
                while (true)
                {
                    float x = Random.Range(-spawnRange, spawnRange);
                    float z = Random.Range(-spawnRange, spawnRange);
                    Vector3 newEnemyPos = new Vector3(x, 0.5f, z);
                    // �����v���C���[�ƓG�̋������v���C���[�̒��a�����łȂ��Ȃ�����N���A
                    if (Vector3.Distance(player.transform.position, newEnemyPos) > banRange)
                    {
                        // �G��1�̐���(�܂�ɏd�Ȃ邪�C�������ʓ|�Ȃ̂Ŗ�������)
                        Instantiate(enemyPrefab, newEnemyPos, Quaternion.identity);
                        break;
                    }
                }
            }
        }
    }
}
