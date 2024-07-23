using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 
/// </summary>
public class GameManager : MonoBehaviour
{
    public List<Brid> brids;
    public List<Pig> pigs;
    public static GameManager _instance;
    private Vector3 originPos;

    public GameObject Lose;
    public GameObject Win;
    public GameObject[] Stars;
    private int StarsNum = 0;
    private int totalNum = 10;              // 总共的关卡数量
    // 单例模式
    private void Awake()
    {
        _instance = this;
        // 如果list中还有小鸟的话
        if (brids.Count > 0)
        {
            // 记录下第一个小鸟的位置，然后赋值给其他小鸟
            originPos = brids[0].transform.position;
        }
    }

    private void Start()
    {
        Initialized();
    }
    // 初始化小鸟
    public void Initialized()
    {
        for (int i = 0; i < brids.Count; i++)
        {
            if (i == 0)
            {
                // 先将当前小鸟的位置进行赋值处理
                brids[i].transform.position = originPos;

                // 第一只小鸟，将其启用
                brids[i].enabled = true;
                brids[i].sp.enabled = true;
            }
            else
            {
                // 其它的小鸟禁用
                brids[i].enabled = false;
                brids[i].sp.enabled = false;
            }
        }
    }

    // 判断是否要进行下一个小鸟的轮换
    public void NextBrid()
    {
        if (pigs.Count > 0)
        {
            // 判断小鸟的个数
            if (brids.Count > 0)
            {
                // 小鸟个数大于0，游戏继续
                Initialized();
            }
            else
            {
                // 输了
                Lose.SetActive(true);
            }
        }
        else
        {
            // 赢了
            Win.SetActive(true);
            ShowStars();
        }
    }

    /// <summary>
    /// 显示星星
    /// </summary>
    public void ShowStars()
    {
        StartCoroutine("Show");
        ///Debug.Log(StarsNum);
    }

     IEnumerator Show()
    {
        for (; StarsNum < brids.Count + 1; StarsNum++)
        {
            // 对于三个以上的小鸟的话，如果i == 3的话，就break，因为stars只有三个，超出会报错
            if(StarsNum >= Stars.Length)
            {
                break;
            }
            yield return new WaitForSeconds(0.3f);
            Stars[StarsNum].SetActive(true);
        }
    }

    // 重玩
    public void ReTry()
    {
        SaveData();
        SceneManager.LoadScene(2);
    }

    // 返回主菜单
    public void Home()
    {
        SaveData();
        SceneManager.LoadScene(1);
    }

    public void SaveData()
    {
        if (StarsNum > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel")))
        {
            // 将本关所获得的星星的数量存进去
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), StarsNum);
        }

        // 将所有关卡的星星存储进去
        int sum = 0;
        for(int i = 1; i <= totalNum; i++)
        {
            sum += PlayerPrefs.GetInt("level" + i.ToString());
        }
        PlayerPrefs.SetInt("totalNum", sum);

        Debug.Log(PlayerPrefs.GetInt("totalNum"));
    }
}
