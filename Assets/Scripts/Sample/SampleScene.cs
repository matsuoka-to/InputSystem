using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Framewark.Input;

public class SampleScene : MonoBehaviour
{
    [SerializeField]
    GameObject[] rootImage;

    [SerializeField]
    Text text;

    int type = 0;

    /// <summary>
    /// デバイス選択
    /// </summary>
    public void DropDwonChange(int id)
    {
        type = id;

        for (int i = 0; i < rootImage.Length; i++)
        {
            if (i == id)
            {
                rootImage[i].SetActive(true);
            }
            else
            {
                rootImage[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// キーボード・マウス処理
    /// </summary>
    void KeyboadMouseUpdate()
    {
        var input = InputManager.Instance.KeyboadMouse();
        if (input != null)
        {
            if (input.GetAnyKeyDown())
            {
                text.text = string.Format("{0} button down", input.GetAnyKey());
            }
            if (input.GetAnyKeyPress())
            {
                text.text = string.Format("{0} button press", input.GetAnyKey());
            }
            if (input.GetAnyKeyUp())
            {
                text.text = string.Format("{0} button up", input.GetAnyKey());
            }
        }
        else
        {
            text.text = string.Format("キーボード・マウス 認識できません");
        }
    }

    /// <summary>
    /// PS4処理
    /// </summary>
    void DualShockUpdate()
    {
        var input = InputManager.Instance.DualShock();
        if (input != null)
        {
            if (input.GetAnyKeyDown())
            {
                text.text = string.Format("{0} button down", input.GetAnyKey());
            }
            if (input.GetAnyKeyPress())
            {
                text.text = string.Format("{0} button press", input.GetAnyKey());
            }
            if (input.GetAnyKeyUp())
            {
                text.text = string.Format("{0} button up", input.GetAnyKey());
            }

            var lPos = input.GetLeftStickPos();
            if ((lPos.x != 0) && (lPos.y != 0))
            {
                text.text = string.Format("L Stick :: {0}", lPos);
            }
            var rPos = input.GetLeftStickPos();
            if ((rPos.x != 0) && (rPos.y != 0))
            {
                text.text = string.Format("R Stick :: {0}", rPos);
            }
        }
        else
        {
            text.text = string.Format("Dual Shock 認識できません");
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }

        switch (type)
        {
            case 0:
                KeyboadMouseUpdate();
            break;

            case 1:
                DualShockUpdate();
            break;
        }
    }
}
