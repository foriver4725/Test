using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LoadSceneAsync
{
    public class LoadingSceneAsync : MonoBehaviour
    {
        [SerializeField] GameObject button;
        [SerializeField] GameObject loadingUI;
        [SerializeField] Slider slider;

        public void OnClick()
        {
            button.SetActive(false);
            loadingUI.SetActive(true);
            StartCoroutine(LoadScene());
        }

        IEnumerator LoadScene()
        {
            AsyncOperation async = SceneManager.LoadSceneAsync("LoadSceneAsyncAfter");
            while (!async.isDone)
            {
                slider.value = async.progress;
                yield return null;
            }
        }
    }
}
