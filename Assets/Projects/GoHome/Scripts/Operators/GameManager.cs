using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GoHome
{
    public class GameManager : MonoBehaviour
    {
        #region static���V���O���g���ɂ���
        public static GameManager Instance { get; set; }

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

        public GameObject CameraUI; // �J������UI
        public Image Mark; // �Ə��}�[�N
        public List<GameObject> AnimalList; // ��������Animal���i�[
        public Vector3 StartPos = new Vector3(18, 3, -40.75f); // �X�^�[�g���W
        public Vector3 GoalPos = new Vector3(9.3f, 2.3f, -8.1f); // �S�[�����W
        public bool IsTaking = false; // �J������`���Ă��邩�ǂ���
        public bool IsClear = false; // �N���A�������ǂ���

        // �V�[�����ă��[�h
        private void Update()
        {
            if (IsClear && Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("GameScene");
            }
        }
    }
}
