using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallLaunch
{
    public class GameManager : MonoBehaviour
    {
        #region �ÓI�E�V���O���g���ɂ���
        public static GameManager Instance { get; set; } = null;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion
    }
}
