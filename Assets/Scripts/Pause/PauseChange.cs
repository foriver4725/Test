using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pause
{
    public class PauseChange : MonoBehaviour
    {
        bool isPause = false;

        void Update()
        {
            #region �|�[�Y�؂�ւ�
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 0;
                isPause = true;
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                Time.timeScale = 1f;
                isPause = false;
            }
            #endregion

            // ���̒��ɏ����ꂽ�����́A�|�[�Y�̉e�����󂯂�B
            if (!isPause)
            {
                Debug.Log(0);
            }
        }
    }
}
