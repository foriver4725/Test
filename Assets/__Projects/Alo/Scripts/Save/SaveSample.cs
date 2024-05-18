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
                Debug.Log("セーブ完了");
            }

            // Load
            if (Input.GetKeyDown(KeyCode.L))
            {
                SaveSystem.Instance.Load();
                Debug.Log("ロード完了");
            }

            // Change
            if (Input.GetKeyDown(KeyCode.C))
            {
                SaveSystem.Instance.UserData.Test = 10;
                Debug.Log("チェンジ完了");
            }

            // Reset
            if (Input.GetKeyDown(KeyCode.R))
            {
                SaveSystem.Instance.UserData.Test = 0;
                Debug.Log("リセット完了");
            }

            // Debug.Log
            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log($"Test = {SaveSystem.Instance.UserData.Test}");
            }
        }
    }
}

