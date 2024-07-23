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
    public int startNum = 1;            // ��ʼ�Ĺؿ�����
    public int endNum = 3;              // ���Ĺؿ�����
    private void Awake()
    {
        // ��Ϸ�������Ƚ��������
        //PlayerPrefs.DeleteAll();

        // ��ȡ��������ݣ�����������������������Ϊ0�Ļ�������ֵ��ֵΪ0.������ڹؿ����������������
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
    /// ������¼������ؿ���level���н�������
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
