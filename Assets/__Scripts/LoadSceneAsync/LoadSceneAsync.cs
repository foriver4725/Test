using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace LoadSceneAsync
{
    public class LoadingSceneAsync : MonoBehaviour
    {
        [SerializeField] GameObject _loadingUI;
        [SerializeField] Slider _slider;

        [SerializeField] Button startBtn;
        [SerializeField] TextMeshProUGUI _text;

        bool _loading = false;

        void Start()
        {
            startBtn.onClick.AddListener(LoadNextScene);
        }

        void Update()
        {
            //Loading中はUpdate()処理を受け付けない
            if (CheckLoading())
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                LoadNextScene();
            }
        }

        IEnumerator LoadScene()
        {
            AsyncOperation async = SceneManager.LoadSceneAsync("LoadSceneAsyncAfter");
            async.allowSceneActivation = false;
            while (!async.isDone)
            {
                _slider.value = async.progress;
                if (async.progress >= 0.9f)
                {
                    _text.text = "Complete!";
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        async.allowSceneActivation = true;
                    }
                }
                yield return null; // 1フレーム待機（「コルーチンは、次のフレームで Update 関数がすべて呼び出された後に続行します。」）
            }
        }

        //ローディング中ならtrueを返す関数
        bool CheckLoading()
        {
            return _loading;
        }

        //UIのボタンまたはEnterキーから処理される
        public void LoadNextScene()
        {
            if (CheckLoading())
            {
                return;
            }
            _loading = true;
            startBtn.gameObject.SetActive(false);
            _loadingUI.SetActive(true);
            StartCoroutine(LoadScene());
        }
    }
}
