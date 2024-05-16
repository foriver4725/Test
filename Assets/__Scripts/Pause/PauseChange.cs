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
            #region ポーズ切り替え
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

            // この中に書かれた処理は、ポーズの影響を受ける。
            if (!isPause)
            {
                Debug.Log(0);
            }
        }
    }
}
