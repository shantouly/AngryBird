using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 
/// </summary>
public class MapSelect : MonoBehaviour
{
    public int StarsNum;
    private bool isSelect = false;
    public GameObject locks;
    public GameObject stars;
    public GameObject map;
    public GameObject level;
    public Text starsText;
    public int startNum = 1;            // 起始的关卡数量
    public int endNum = 3;              // 最后的关卡数量
    private void Awake()
    {
        // 游戏发布，先将数据清空
        //PlayerPrefs.DeleteAll();

        // 获取储存的数据，将星星总数计算出来，如果为0的话，返回值赋值为0.如果大于关卡所需的数量，解锁
        if(PlayerPrefs.GetInt("totalNum",0) >= StarsNum)
        {
            isSelect = true;    
        }

        if (isSelect)
        {
            locks.SetActive(false);
            stars.SetActive(true);
        }

        int count = 0;
        for(int i = startNum; i <= endNum; i++)
        {
            count += PlayerPrefs.GetInt("level" + i.ToString());
        }
        starsText.text = count.ToString() + "/9";
    }

    /// <summary>
    /// 鼠标点击事件，将关卡与level进行交互处理
    /// </summary>
    public void Select()
    {
        if (isSelect)
        {
            level.SetActive(true);
            map.SetActive(false);
        }
    }
}
