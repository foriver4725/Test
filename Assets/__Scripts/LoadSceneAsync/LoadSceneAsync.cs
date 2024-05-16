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
            //Loading����Update()�������󂯕t���Ȃ�
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
                yield return null; // 1�t���[���ҋ@�i�u�R���[�`���́A���̃t���[���� Update �֐������ׂČĂяo���ꂽ��ɑ��s���܂��B�v�j
            }
        }

        //���[�f�B���O���Ȃ�true��Ԃ��֐�
        bool CheckLoading()
        {
            return _loading;
        }

        //UI�̃{�^���܂���Enter�L�[���珈�������
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
