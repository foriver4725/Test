using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace StructureOfGameProgram2
{
    public class GameManager : MonoBehaviour
    {
        #region static���V���O���g���ɂ���
        public static GameManager Instance { get; set; } = null;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion

        public TextMeshProUGUI TextScore; // �e�L�X�g��\������TMPro���Z�b�g
        public GameObject GameOverScreen; // �Q�[���I�[�o�[UI
        public bool IsGameActive; // �Q�[���̏��
        public int Score = 0; // �X�R�A
        public int Point = 1; // �p���[�A�b�v���ɓG�ɐG�ꂽ�ہC�X�R�A�ɉ��Z�����|�C���g

        // �`�F�C�X��ԁi�����l�̓I���Btrue�Ȃ�G���v���C���[��ǂ������Cfalse�Ȃ�G���v���C���[���瓦����j
        public bool IsChasing = true;

        private void Start()
        {
            IsGameActive = true;
            GameOverScreen.SetActive(false);
        }

        private void Update()
        {
            if (!IsGameActive)
            {
                GameOverScreen.SetActive(true);
            }
            else
            {
                TextScore.text = "Score: " + Score;
            }
        }

        public void ReStart()
        {
            // ���݂̃V�[�����ă��[�h
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
