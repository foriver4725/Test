using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alo
{
    public class GMSample : MonoBehaviour
    {
        #region 静的化・シングルトン化・プロパティ
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
