using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace StructureOfGameProgram2
{
    public class StartButton : MonoBehaviour
    {
        void Start()
        {
            Button button = GetComponent<Button>();

            // �{�^���������ꂽ�Ƃ��̏������L�q
            button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Game");
            });
        }
    }
}
