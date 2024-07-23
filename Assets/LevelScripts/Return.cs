using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class Return : MonoBehaviour
{
    public GameObject map;
    public GameObject level;

    public void returnMapPanel()
    {
        level.SetActive(false);
        map.SetActive(true);
    }
}
