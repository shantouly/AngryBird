using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// 关卡的脚本
/// </summary>
public class LevelSelect : MonoBehaviour
{
    private bool isSelect = false;
    public Sprite levelIBG;
    private Image image;

    public GameObject[] stars;

    private void Awake()
    {
        // 获取当前图片组件
        image = GetComponent<Image>();
    }

    private void Start()
    {
        if(gameObject.name == transform.parent.GetChild(0).name)
        {
            // 如果是第一关的话，那么就解锁
            isSelect = true;
        }
        else
        {
            int beforeNum = int.Parse(gameObject.name) - 1;         // 获取上一关的名称
            if(PlayerPrefs.GetInt("level"+beforeNum.ToString()) > 0)
            {
                // 说明上一关通关了，可以进行本关的解锁
                isSelect = true;
            }

        }
        if (isSelect)
        {
            image.sprite = levelIBG;
            transform.Find("num").gameObject.SetActive(true);

            int count = PlayerPrefs.GetInt("level"+gameObject.name);    // 获取当前关卡的星星数
            if(count >0)
            {
                // 星星大于0，显示出来
                for(int i = 0; i < count; i++)
                {
                    stars[i].SetActive(true);
                }
            }
        }
    }

    public void Selected()
    {
        // 当选择关卡时，将本关卡的名字存入进去
        if (isSelect)
        {
            PlayerPrefs.SetString("nowLevel","level"+ gameObject.name);
            SceneManager.LoadScene(2);
        }
    }
}
