using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.InputSystem;
using Interface;

namespace IA
{
    #region

    public enum InputType
    {
        /// <summary>
        /// 【null】デフォルト値。何も意味しない
        /// </summary>
        Null,

        /// <summary>
        /// 【bool】そのフレームが、押された瞬間のフレームであるか
        /// </summary>
        Click,

        /// <summary>
        /// 【bool】そのフレームが、一定秒数押された瞬間のフレームであるか
        /// </summary>
        Hold,

        /// <summary>
        /// 【bool】そのフレームにおける、押されているかのフラグ
        /// </summary>
        Value0,

        /// <summary>
        /// 【float】そのフレームにおける、1軸の入力の値(単位線 以内)
        /// </summary>
        Value1,

        /// <summary>
        /// 【Vector2】そのフレームにおける、2軸の入力の値(単位円 以内)
        /// </summary>
        Value2,

        /// <summary>
        /// 【Vector3】そのフレームにおける、3軸の入力の値(単位球 以内)
        /// </summary>
        Value3
    }

    public sealed class InputInfo : IDisposable, INullExistable
    {
        private InputAction inputAction;
        private readonly InputType type;
        private ReadOnlyCollection<Action<InputAction.CallbackContext>> action;

        public bool Bool { get; private set; } = false;
        public float Float { get; private set; } = 0;
        public Vector2 Vector2 { get; private set; } = Vector2.zero;
        public Vector3 Vector3 { get; private set; } = Vector3.zero;

        public InputInfo(InputAction inputAction, InputType type)
        {
            this.inputAction = inputAction;
            this.type = type;

            this.action = this.type switch
            {
                InputType.Null => null,

                InputType.Click => new List<Action<InputAction.CallbackContext>>()
                {
                    _ => { Bool = true; }
                }
                .AsReadOnly(),

                InputType.Hold => new List<Action<InputAction.CallbackContext>>()
                {
                    _ => { Bool = true; }
                }
                .AsReadOnly(),

                InputType.Value0 => new List<Action<InputAction.CallbackContext>>()
                {
                    _ => { Bool = true; },
                    _ => { Bool = false; }
                }
                .AsReadOnly(),

                InputType.Value1 => new List<Action<InputAction.CallbackContext>>()
                {
                    e => { Float = e.ReadValue<float>(); }
                }
                .AsReadOnly(),

                InputType.Value2 => new List<Action<InputAction.CallbackContext>>()
                {
                    e => { Vector2 = e.ReadValue<Vector2>(); }
                }
                .AsReadOnly(),

                InputType.Value3 => new List<Action<InputAction.CallbackContext>>()
                {
                    e => { Vector3 = e.ReadValue<Vector3>(); }
                }
                .AsReadOnly(),

                _ => null
            };
        }

        public void Dispose()
        {
            inputAction = null;
            action = null;
        }

        public bool IsNullExist()
        {
            if (inputAction == null) return true;
            if (action == null) return true;
            return false;
        }

        public void Link(bool isLink)
        {
            if (inputAction == null) return;
            if (action == null) return;

            if (isLink)
            {
                switch (type)
                {
                    case InputType.Null:
                        break;

                    case InputType.Click:
                        inputAction.performed += action[0];
                        break;

                    case InputType.Hold:
                        inputAction.performed += action[0];
                        break;

                    case InputType.Value0:
                        inputAction.performed += action[0];
                        inputAction.canceled += action[1];
                        break;

                    case InputType.Value1:
                        inputAction.started += action[0];
                        inputAction.performed += action[0];
                        inputAction.canceled += action[0];
                        break;

                    case InputType.Value2:
                        inputAction.started += action[0];
                        inputAction.performed += action[0];
                        inputAction.canceled += action[0];
                        break;

                    case InputType.Value3:
                        inputAction.started += action[0];
                        inputAction.performed += action[0];
                        inputAction.canceled += action[0];
                        break;

                    default:
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case InputType.Null:
                        break;

                    case InputType.Click:
                        inputAction.performed -= action[0];
                        break;

                    case InputType.Hold:
                        inputAction.performed -= action[0];
                        break;

                    case InputType.Value0:
                        inputAction.performed -= action[0];
                        inputAction.canceled -= action[1];
                        break;

                    case InputType.Value1:
                        inputAction.started -= action[0];
                        inputAction.performed -= action[0];
                        inputAction.canceled -= action[0];
                        break;

                    case InputType.Value2:
                        inputAction.started -= action[0];
                        inputAction.performed -= action[0];
                        inputAction.canceled -= action[0];
                        break;

                    case InputType.Value3:
                        inputAction.started -= action[0];
                        inputAction.performed -= action[0];
                        inputAction.canceled -= action[0];
                        break;

                    default:
                        break;
                }
            }
        }

        public void OnLateUpdate()
        {
            if (type == InputType.Click && Bool) Bool = false;
            else if (type == InputType.Hold && Bool) Bool = false;
        }
    }

    public static class InputEx
    {
        public static InputInfo Add(this InputInfo inputInfo, List<InputInfo> list)
        {
            list.Add(inputInfo);
            return inputInfo;
        }
    }

    #endregion

    public sealed class InputGetter : MonoBehaviour
    {
        #region

        private IA ia;
        private List<InputInfo> inputInfoList;
        public static InputGetter Instance { get; set; } = null;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            ia = new();
            inputInfoList = new();

            Init();

            foreach (InputInfo e in inputInfoList) e.Link(true);
        }
        private void OnDestroy()
        {
            foreach (InputInfo e in inputInfoList) e.Link(false);

            ia.Dispose();
            foreach (InputInfo e in inputInfoList) e.Dispose();

            ia = null;
            inputInfoList = null;
        }

        private void OnEnable() => ia.Enable();
        private void OnDisable() => ia.Disable();

        private void LateUpdate()
        {
            foreach (InputInfo e in inputInfoList) e.OnLateUpdate();
        }

        #endregion

        public InputInfo Main_Click { get; private set; }
        public InputInfo Main_Hold { get; private set; }
        public InputInfo Main_Value2 { get; private set; }

        private void Init()
        {
            Main_Click = new InputInfo(ia.Main.Click, InputType.Click).Add(inputInfoList);
            Main_Hold = new InputInfo(ia.Main.Hold, InputType.Hold).Add(inputInfoList);
            Main_Value2 = new InputInfo(ia.Main.Value, InputType.Value2).Add(inputInfoList);
        }
    }
}