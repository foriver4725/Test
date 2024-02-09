using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alo
{
    public class GMSample : MonoBehaviour
    {
        #region �ÓI���E�V���O���g�����E�v���p�e�B
        public static GMSample Instance { get; set; } = null;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        #endregion
    }
}
