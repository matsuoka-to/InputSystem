using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Framewark.Input
{
    public class DualShockISX : InputBase
    {
        InputAction buttonAction;
        InputAction dPadAction;
        InputAction stickMoveAction;

        Vector2 leftStickPos;
        Vector2 rightStickPos;

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

                buttonAction = new InputAction(name: "DualShockButtonAction", InputActionType.PassThrough, binding: "*DualShock*/<button>");
                buttonAction.performed += callbackContext => OnControllerButtonPress(callbackContext.control as ButtonControl, isPS: true);
                buttonAction.Enable();

                dPadAction = new InputAction(name: "DualShockDpadAction", InputActionType.PassThrough, binding: "*DualShock*/<dpad>");
                dPadAction.performed += callbackContext => OnDpadPress(callbackContext.control as DpadControl);
                dPadAction.Enable();

                stickMoveAction = new InputAction(name: "DualShockStickMoveAction", InputActionType.PassThrough, binding: "*DualShock*/<stick>");
                stickMoveAction.performed += callbackContext => StickMove(callbackContext.control as StickControl);
                stickMoveAction.Enable();
            }
        }

        /// <summary>
        /// デバイスチェック
        /// </summary>
        public override bool DeviceCheck()
        {
            Gamepad gamepad = Gamepad.current;
            if (gamepad != null)
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
            if (buttonAction != null) buttonAction.Enable();
            if (dPadAction != null) dPadAction.Enable();
            if (stickMoveAction != null) stickMoveAction.Enable();
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        public override void Suspend()
        {
            buttonAction?.Disable();
            dPadAction?.Disable();
            stickMoveAction?.Disable();
        }

        /// <summary>
        /// 左スティック値取得
        /// </summary>
        public Vector2 GetLeftStickPos()
        {
            return leftStickPos;
        }

        /// <summary>
        /// 右スティック値取得
        /// </summary>
        public Vector2 GetRightStickPos()
        {
            return rightStickPos;
        }

        /// <summary>
        /// ボタン情報
        /// </summary>
        void OnControllerButtonPress(ButtonControl control, string dpadName = null, bool isXbox = false, bool isPS = false)
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
        /// 十字キー情報
        /// </summary>
        void OnDpadPress(DpadControl control)
        {
            string dpadName = control.name;
            OnControllerButtonPress(control.up, dpadName);
            OnControllerButtonPress(control.down, dpadName);
            OnControllerButtonPress(control.left, dpadName);
            OnControllerButtonPress(control.right, dpadName);
        }

        /// <summary>
        /// スティック情報
        /// </summary>
        void StickMove(StickControl control)
        {
            string stickdName = control.name;
            Vector2 pos = control.ReadValue();
            switch (stickdName)
            {
                case "leftStick":
                    leftStickPos = pos;
                break;

                case "rightStick":
                    rightStickPos = pos;
                break;
            }
        }

        /// <summary>
        /// キー取得
        /// </summary>
        PadType GetButton(string buttonName)
        {
            switch (buttonName)
            {
                case "up":
                    return PadType.Up;

                case "down":
                    return PadType.Down;

                case "left":
                    return PadType.Left;

                case "right":
                    return PadType.Right;

                // 一旦、PC(XBOX)に合わせる 9.15 murakami 
                case "buttonSouth":
                    return PadType.A;

                case "buttonEast":
                    return PadType.B;

                case "buttonWest":
                    return PadType.X;

                case "buttonNorth":
                    return PadType.Y;


                case "leftShoulder":
                    return PadType.L1;

                case "leftTriggerButton":
                    return PadType.L2;

                case "rightShoulder":
                    return PadType.R1;

                case "rightTriggerButton":
                    return PadType.R2;

                case "start":
                    return PadType.Start;

                case "select":
                    return PadType.Select;

                case "touchpadButton":
                    return PadType.TouchPad;

                case "systemButton":
                    return PadType.Option;
            }

            return PadType.None;
        }
    }
}
