using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StructureOfGameProgram2
{
    public class PowerUpGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject player; //�v���C���[���A�^�b�`
        [SerializeField] private GameObject powerUpPrefab; // �G�̃v���n�u���A�^�b�`
        [SerializeField] private int spawnNum; // ���������邩
        [SerializeField] private float spawnRange; // �v���C���[�𒆐S�Ƃ����C1��2*spawnRange�̐����`���ɐ��������
        [SerializeField] private float banRange; // �v���C���[�𒆐S�Ƃ����C���̔��a�̉~���ɂ͐�������Ȃ��i���S���W�ɂ��āj
        private bool isGenerated = false; // �p���[�A�b�v�A�C�e�������łɐ������Ă��邩�ǂ���

        void Update()
        {
            // �p���[�A�b�v�A�C�e���̐�����������ɒB����܂Ŗ��t���[���p���[�A�b�v�A�C�e���𐶐��i�������B����������������Ȃ��j
            if (GameObject.FindGameObjectsWithTag("PowerUp").Length < spawnNum && GameManager.Instance.IsGameActive && !isGenerated)
            {
                // �����𖞂����܂Ńp���[�A�b�v�A�C�e���̍��W�𐶐��C�]��
                while (true)
                {
                    float x = Random.Range(-spawnRange, spawnRange);
                    float z = Random.Range(-spawnRange, spawnRange);
                    Vector3 newPowerUpPos = new Vector3(x, 0.5f, z);
                    // �����v���C���[�ƃp���[�A�b�v�A�C�e���̋������v���C���[�̒��a�����łȂ��Ȃ�����N���A
                    if (Vector3.Distance(player.transform.position, newPowerUpPos) > banRange)
                    {
                        // �p���[�A�b�v�A�C�e����1����(�܂�ɏd�Ȃ邪�C�������ʓ|�Ȃ̂Ŗ�������)
                        Instantiate(powerUpPrefab, newPowerUpPos, Quaternion.identity);
                        break;
                    }
                }
            }
            else
            {
                isGenerated = true;
            }
        }
    }
}
