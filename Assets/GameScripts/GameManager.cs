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
    private int totalNum = 10;              // �ܹ��Ĺؿ�����
    // ����ģʽ
    private void Awake()
    {
        _instance = this;
        // ���list�л���С��Ļ�
        if (brids.Count > 0)
        {
            // ��¼�µ�һ��С���λ�ã�Ȼ��ֵ������С��
            originPos = brids[0].transform.position;
        }
    }

    private void Start()
    {
        Initialized();
    }
    // ��ʼ��С��
    public void Initialized()
    {
        for (int i = 0; i < brids.Count; i++)
        {
            if (i == 0)
            {
                // �Ƚ���ǰС���λ�ý��и�ֵ����
                brids[i].transform.position = originPos;

                // ��һֻС�񣬽�������
                brids[i].enabled = true;
                brids[i].sp.enabled = true;
            }
            else
            {
                // ������С�����
                brids[i].enabled = false;
                brids[i].sp.enabled = false;
            }
        }
    }

    // �ж��Ƿ�Ҫ������һ��С����ֻ�
    public void NextBrid()
    {
        if (pigs.Count > 0)
        {
            // �ж�С��ĸ���
            if (brids.Count > 0)
            {
                // С���������0����Ϸ����
                Initialized();
            }
            else
            {
                // ����
                Lose.SetActive(true);
            }
        }
        else
        {
            // Ӯ��
            Win.SetActive(true);
            ShowStars();
        }
    }

    /// <summary>
    /// ��ʾ����
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
            // �����������ϵ�С��Ļ������i == 3�Ļ�����break����Ϊstarsֻ�������������ᱨ��
            if(StarsNum >= Stars.Length)
            {
                break;
            }
            yield return new WaitForSeconds(0.3f);
            Stars[StarsNum].SetActive(true);
        }
    }

    // ����
    public void ReTry()
    {
        SaveData();
        SceneManager.LoadScene(2);
    }

    // �������˵�
    public void Home()
    {
        SaveData();
        SceneManager.LoadScene(1);
    }

    public void SaveData()
    {
        if (StarsNum > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel")))
        {
            // ����������õ����ǵ��������ȥ
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), StarsNum);
        }

        // �����йؿ������Ǵ洢��ȥ
        int sum = 0;
        for(int i = 1; i <= totalNum; i++)
        {
            sum += PlayerPrefs.GetInt("level" + i.ToString());
        }
        PlayerPrefs.SetInt("totalNum", sum);

        Debug.Log(PlayerPrefs.GetInt("totalNum"));
    }
}
