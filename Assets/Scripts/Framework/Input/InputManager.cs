using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Framewark.Common;

namespace Framewark.Input
{
    public class InputManager : SingletonMonoBehaviour<InputManager>
    {
        KeyboardMouseISX keyBoard = null;
        DualShockISX dualShock = null;

        private void Start()
        {
            keyBoard = new KeyboardMouseISX();
            dualShock = new DualShockISX();

            KeyBoadMouseCheck();
            GamePadCheck();
        }

        /// <summary>
        /// キーボード・マウスチェック
        /// </summary>
        bool KeyBoadMouseCheck()
        {
            if (keyBoard.DeviceCheck())
            {
                keyBoard.Init();
                return true;
            }

            return false;
        }

        /// <summary>
        /// PS4チェック
        /// </summary>
        bool GamePadCheck()
        {
            if (dualShock.DeviceCheck())
            {
                dualShock.Init();
                return true;
            }

            return false;
        }

        /// <summary>
        /// キーボード・マウス
        /// </summary>
        public KeyboardMouseISX KeyboadMouse()
        {
            if (keyBoard.DeviceCheck())
            {
                return keyBoard;
            }

            return null;
        }

        /// <summary>
        /// PS4コントローラ
        /// </summary>
        public DualShockISX DualShock()
        {
            if (dualShock.DeviceCheck())
            {
                return dualShock;
            }

            return null;
        }

        /// <summary>
        /// 再会
        /// </summary>
        private void OnEnable()
        {
            if (keyBoard != null)
            {
                keyBoard.Resume();
            }
            if (dualShock != null)
            {
                dualShock.Resume();
            }
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        private void OnDisable()
        {
            if (keyBoard != null)
            {
                keyBoard.Suspend();
            }
            if (dualShock != null)
            {
                dualShock.Suspend();
            }
        }

        /// <summary>
        /// キークリア
        /// </summary>
        private void LateUpdate()
        {
            if(KeyBoadMouseCheck())
            {
                keyBoard.KeyClear();
            }

            if(GamePadCheck())
            {
                dualShock.KeyClear();
            }
        }
    }
}
