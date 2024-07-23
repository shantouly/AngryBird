using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class loadLevel : MonoBehaviour
{
    // 当选择了关卡之后，加载prefab中的场景
    private void Awake()
    {
       Instantiate(Resources.Load(PlayerPrefs.GetString("nowLevel")));
    }
}
