using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Alo
{
    [RequireComponent(typeof(Image))]
    public class StartBManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        #region スクリプト本体
        private Image image;
        private Sprite defaultSprite;
        [SerializeField] private Sprite pressedSprite;

        [SerializeField] private int id = 0;
        [SerializeField] private float pressOffsetY = 0f;
        [SerializeField] private UnityEvent onClick = null;

        private Transform child;
        private float defaultY;
        private StartBManager[] buttonControllers;

        private bool isPushed = false;


        void Awake()
        {
            image = GetComponent<Image>();
            defaultSprite = image.sprite;
            child = transform.GetChild(0);
            defaultY = child.localPosition.y;

            Transform canvas = GameObject.Find("Canvas").transform;
            buttonControllers = canvas.GetComponentsInChildren<StartBManager>();
        }

        void OnEnable()
        {
            ButtonActive(true);
        }

        public void ButtonActive(bool active)
        {
            isPushed = !active;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (isPushed) return;
            Vector3 pos = child.localPosition;
            pos.y = defaultY - pressOffsetY;
            child.localPosition = pos;
            if (pressedSprite != null) image.sprite = pressedSprite;
            OnButtonDown();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (isPushed) return;
            Vector3 pos = child.localPosition;
            pos.y = defaultY;
            child.localPosition = pos;
            image.sprite = defaultSprite;
            OnButtonUp();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (id != -1)
            {
                foreach (var controller in buttonControllers)
                {
                    controller.ButtonActive(controller.id != this.id);
                }
            }

            OnButtonClick();
            onClick?.Invoke();
        }

        public void AllButtonReset()
        {
            foreach (var controller in buttonControllers)
            {
                controller.ButtonActive(true);
            }
        }
        #endregion

        // ボタンを押したときtrue、離したときfalseになるフラグ
        private bool buttonDownFlag = false;

        void Update()
        {
            // 押されたままの時実行
            if (buttonDownFlag)
            {
                OnButtonHold();
            }
        }

        #region ボタンの基本3アクション
        /// <summary>
        /// Down時の共通処理
        /// </summary>
        private void OnButtonDown()
        {
            buttonDownFlag = true;

            Destroy(this.gameObject);
            SceneManager.LoadScene("StorySelect");
        }

        /// <summary>
        /// Up時の共通処理
        /// </summary>
        private void OnButtonUp()
        {
            buttonDownFlag = false;
        }

        /// <summary>
        /// Hold時の共通処理
        /// </summary>
        private void OnButtonHold()
        {

        }
        #endregion

        /// <summary>
        /// Click時の共通処理（SE鳴らすなど）
        /// </summary>
        private void OnButtonClick()
        {
            
        }
    }
}
