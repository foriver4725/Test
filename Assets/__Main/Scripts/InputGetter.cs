using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Interface;

namespace IA
{
    #region

    public enum InputType
    {
        Null,
        Click,
        Hold,
        Value0,
        Value1,
        Value2,
        Value3
    }

    public sealed class InputInfo : IDisposable, INullExistable
    {
        private InputAction inputAction;
        private readonly InputType type;
        private List<Action<InputAction.CallbackContext>> action;

        private bool val0 = false;
        private float val1 = 0;
        private Vector2 val2 = Vector2.zero;
        private Vector3 val3 = Vector3.zero;

        public InputInfo(InputAction inputAction, InputType type)
        {
            this.inputAction = inputAction;
            this.type = type;

            this.action = this.type switch
            {
                InputType.Null
                => null,
                InputType.Click
                => new() { (InputAction.CallbackContext context) => { val0 = true; } },
                InputType.Hold
                => new() { (InputAction.CallbackContext context) => { val0 = true; } },
                InputType.Value0
                => new() { (InputAction.CallbackContext context) => { val0 = true; },
                    (InputAction.CallbackContext context) => { val0 = false; } },
                InputType.Value1
                => new() { (InputAction.CallbackContext context) => { val1 = context.ReadValue<float>(); } },
                InputType.Value2
                => new() { (InputAction.CallbackContext context) => { val2 = context.ReadValue<Vector2>(); } },
                InputType.Value3
                => new() { (InputAction.CallbackContext context) => { val3 = context.ReadValue<Vector3>(); } },
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

        public T Get<T>()
        {
            object ret = type switch
            {
                InputType.Null => null,
                InputType.Click => val0,
                InputType.Hold => val0,
                InputType.Value0 => val0,
                InputType.Value1 => val1,
                InputType.Value2 => val2,
                InputType.Value3 => val3,
                _ => null
            };

            return (T)ret;
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
            if (type == InputType.Click && val0) val0 = false;
            if (type == InputType.Hold && val0) val0 = false;
        }
    }

    #endregion

    #region

    public static class Ex
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