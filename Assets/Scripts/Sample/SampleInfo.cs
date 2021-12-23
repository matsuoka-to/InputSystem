using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleInfo : MonoBehaviour
{
    [SerializeField]
    Text playerName;

    [SerializeField]
    Text time;

    /// <summary>
    /// 名前雪堤
    /// </summary>
    public void SetName(string name)
    {
        playerName.text = name;
    }

    /// <summary>
    /// タイマー設定
    /// </summary>
    public void SetTime(string time)
    {
        this.time.text = time;
    }

    /// <summary>
    /// 名前取得
    /// </summary>
    public string GetName()
    {
        return playerName.text;
    }
}
