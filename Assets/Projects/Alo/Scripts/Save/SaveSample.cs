using UnityEngine;

namespace Alo
{
    public class SaveSample : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            // Save
            if (Input.GetKeyDown(KeyCode.S))
            {
                SaveSystem.Instance.Save();
                Debug.Log("�Z�[�u����");
            }

            // Load
            if (Input.GetKeyDown(KeyCode.L))
            {
                SaveSystem.Instance.Load();
                Debug.Log("���[�h����");
            }

            // Change
            if (Input.GetKeyDown(KeyCode.C))
            {
                SaveSystem.Instance.UserData.Test = 10;
                Debug.Log("�`�F���W����");
            }

            // Reset
            if (Input.GetKeyDown(KeyCode.R))
            {
                SaveSystem.Instance.UserData.Test = 0;
                Debug.Log("���Z�b�g����");
            }

            // Debug.Log
            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log($"Test = {SaveSystem.Instance.UserData.Test}");
            }
        }
    }
}

