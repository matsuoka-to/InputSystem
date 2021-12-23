using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Framewark.Input
{
    public class KeyboardMouseISX : InputBase
    {
        InputAction keyboardAction;
        InputAction mouseButtonAction;
        InputAction mouseOtherAction;

        const int MOUSE_MOVE_DEADZONE = 5;

        Vector2 mousePos;
        Vector3 mouseDelta;

        bool initFlg = false;

        /// <summary>
        /// 初期化
        /// </summary>
        public override void Init()
        {
            if (initFlg == false)
            {
                initFlg = true;

                base.Init();

                keyboardAction = new InputAction(name: "KeyboardPressAction", InputActionType.PassThrough, binding: "<keyboard>/<key>");
                keyboardAction.performed += callbackContext => KeyboardKeyPress(callbackContext.control as KeyControl);
                keyboardAction.Enable();

                mouseButtonAction = new InputAction(name: "MouseButtonAction", InputActionType.PassThrough, binding: "<mouse>/<button>");
                mouseButtonAction.performed += callbackContext => MouseKeyPress(callbackContext.control as ButtonControl);
                mouseButtonAction.Enable();

                mouseOtherAction = new InputAction(name: "MouseVector2Action", InputActionType.PassThrough, binding: "<mouse>/<vector2>");
                mouseOtherAction.performed += callbackContext => MouseVector2(callbackContext.control as Vector2Control);
                mouseOtherAction.Enable();
            }
        }

        /// <summary>
        /// デバイスチェック
        /// </summary>
        public override bool DeviceCheck()
        {
            Mouse mouse = InputSystem.GetDevice<Mouse>();
            Keyboard keyboard = InputSystem.GetDevice<Keyboard>();
            if ((keyboard != null) && (mouse != null))
            {
                return true;
            }
            else
            {
                initFlg = false;
            }

            return false;
        }

        /// <summary>
        /// 再会
        /// </summary>
        public override void Resume()
        {
            keyboardAction?.Enable();
            mouseButtonAction?.Enable();
            mouseOtherAction?.Enable();
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        public override void Suspend()
        {
            keyboardAction.Disable();
            mouseButtonAction.Disable();
            mouseOtherAction.Disable();
        }

        /// <summary>
        /// マウス位置取得
        /// </summary>
        public Vector2 GetMousePos()
        {
            return mousePos;
        }

        /// <summary>
        /// マウス スクロール情報取得
        /// </summary>
        /// <returns></returns>
        public Vector3 GetMouseDelta()
        {
            return mouseDelta;
        }

        /// <summary>
        /// キーボード キー取得
        /// </summary>
        /// <param name="control"></param>
        void KeyboardKeyPress(KeyControl control)
        {
            var button = GetButton(control.name);
            if (button != PadType.None)
            {
                var value = (long)(1 << (int)button);
                if (control.isPressed)
                {
                    keyDown |= value;
                    keyPress |= value;
                    anyKey = button;
                }
                else
                {
                    keyUp |= value;
                    keyPress &= ~value;
                }
            }
        }

        /// <summary>
        /// マウスボタン取得
        /// </summary>
        void MouseKeyPress(ButtonControl control)
        {
            var button = GetButton(control.name);
            if (button != PadType.None)
            {
                var value = (long)(1 << (int)button);
                if (control.isPressed)
                {
                    keyDown |= value;
                    keyPress |= value;
                    anyKey = button;
                }
                else
                {
                    if ((keyPress & value) == value)
                    {
                        keyUp |= value;
                    }
                    keyPress &= ~value;
                }
            }
        }

        /// <summary>
        /// マウス状態取得
        /// </summary>
        void MouseVector2(Vector2Control control)
        {
            if (control.name == "position")
            {
                OnMousePos(control.ReadValue());
            }
            else if (control.name == "delta")
            {
                OnMouseDelta(control.ReadValue());
            }
            else if (control.name == "scroll")
            {
                OnMouseScroll(control.ReadValue());
            }
        }

        /// <summary>
        /// マウス座標
        /// </summary>
        void OnMousePos(Vector2 pos)
        {
            mousePos = pos;
        }

        /// <summary>
        /// マウス 移動値取得
        /// </summary>
        void OnMouseDelta(Vector2 move)
        {
            // Mouse move horizontally
            if (Mathf.Abs(move.x) > MOUSE_MOVE_DEADZONE)
            {
                mouseDelta.x = move.x;
            }
            else
            {
                mouseDelta.x = 0;
            }

            // Mouse move vertically
            if (Mathf.Abs(move.y) > MOUSE_MOVE_DEADZONE)
            {
                mouseDelta.y = move.y;
            }
            else
            {
                mouseDelta.y = 0;
            }
        }

        /// <summary>
        /// マウス ホイール値取得
        /// </summary>
        void OnMouseScroll(Vector2 scroll)
        {
            if (Mathf.Abs(scroll.y) > MOUSE_MOVE_DEADZONE)
            {
                mouseDelta.z = scroll.y;
            }
            else
            {
                mouseDelta.z = 0;
            }
        }

        /// <summary>
        /// キー取得
        /// </summary>
        PadType GetButton(string buttonName)
        {
            switch (buttonName)
            {
                case "upArrow":
                    return PadType.Up;

                case "downArrow":
                    return PadType.Down;

                case "leftArrow":
                    return PadType.Left;

                case "rightArrow":
                    return PadType.Right;

                //// 一旦、PC(XBOXPad)に合わせる 9.15 murakami 
                case "z":
                    return PadType.A;

                case "s":
                    return PadType.B;

                case "a":
                    return PadType.X;

                case "w":
                    return PadType.Y;

                case "space":
                    return PadType.Space;

                case "leftButton":
                    return PadType.LeftButton;

                case "rightButton":
                    return PadType.RightButton;

                case "middleButton":
                    return PadType.MiddleButton;
            }

            return PadType.None;
        }
    }
}
