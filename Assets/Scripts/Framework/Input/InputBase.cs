using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Framewark.Input
{
    public class InputBase
    {
        public enum PadType
        {
            None = 0,
            Up,
            Down,
            Left,
            Right,
            A,
            B,
            X,
            Y,
            L1,
            L2,
            R1,
            R2,
            Start,
            Select,
            TouchPad,
            Option,
            LeftButton,
            RightButton,
            MiddleButton,
            Space,
        }

        protected long keyDown;
        protected long keyPress;
        protected long keyUp;
        protected PadType anyKey;

        /// <summary>
        /// 初期化
        /// </summary>
        public virtual void Init()
        {
            keyDown = (long)PadType.None;
            keyPress = (long)PadType.None;
            keyUp = (long)PadType.None;
            anyKey = PadType.None;
        }

        /// <summary>
        /// キークリア
        /// </summary>
        public virtual void KeyClear()
        {
            if (keyDown != (long)PadType.None)
            {
                keyDown = (long)PadType.None;
            }

            if (keyUp != (long)PadType.None)
            {
                keyUp = (long)PadType.None;
                anyKey = PadType.None;
            }
        }

        /// <summary>
        /// デバイスチェック
        /// </summary>
        public virtual bool DeviceCheck()
        {
            return false;
        }

        /// <summary>
        /// 再会
        /// </summary>
        public virtual void Resume()
        {
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        public virtual void Suspend()
        {
        }

        /// <summary>
        /// キーダウン取得
        /// </summary>
        public bool GetButtonDown(PadType button)
        {
            var value = 1 << (int)button;
            if ((keyDown & value) == value)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// キープレス取得
        /// </summary>
        public bool GetButtonPress(PadType button)
        {
            var value = 1 << (int)button;
            if ((keyPress & value) == value)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// キーアップ取得
        /// </summary>
        public bool GetButtonUp(PadType button)
        {
            var value = 1 << (int)button;
            if ((keyUp & value) == value)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// キー取得
        /// </summary>
        public PadType GetAnyKey()
        {
            return anyKey;
        }

        /// <summary>
        /// 何かのキーダウン取得
        /// </summary>
        public bool GetAnyKeyDown()
        {
            if (keyDown != (long)PadType.None)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 何かのキープレス取得
        /// </summary>
        public bool GetAnyKeyPress()
        {
            if (keyPress != (long)PadType.None)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 何かのキーアップ取得
        /// </summary>
        public bool GetAnyKeyUp()
        {
            if (keyUp != (long)PadType.None)
            {
                return true;
            }
            return false;
        }
    }
}
