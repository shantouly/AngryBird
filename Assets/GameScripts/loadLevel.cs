using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class loadLevel : MonoBehaviour
{
    // ��ѡ���˹ؿ�֮�󣬼���prefab�еĳ���
    private void Awake()
    {
       Instantiate(Resources.Load(PlayerPrefs.GetString("nowLevel")));
    }
}
