using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// �ؿ��Ľű�
/// </summary>
public class LevelSelect : MonoBehaviour
{
    private bool isSelect = false;
    public Sprite levelIBG;
    private Image image;

    public GameObject[] stars;

    private void Awake()
    {
        // ��ȡ��ǰͼƬ���
        image = GetComponent<Image>();
    }

    private void Start()
    {
        if(gameObject.name == transform.parent.GetChild(0).name)
        {
            // ����ǵ�һ�صĻ�����ô�ͽ���
            isSelect = true;
        }
        else
        {
            int beforeNum = int.Parse(gameObject.name) - 1;         // ��ȡ��һ�ص�����
            if(PlayerPrefs.GetInt("level"+beforeNum.ToString()) > 0)
            {
                // ˵����һ��ͨ���ˣ����Խ��б��صĽ���
                isSelect = true;
            }

        }
        if (isSelect)
        {
            image.sprite = levelIBG;
            transform.Find("num").gameObject.SetActive(true);

            int count = PlayerPrefs.GetInt("level"+gameObject.name);    // ��ȡ��ǰ�ؿ���������
            if(count >0)
            {
                // ���Ǵ���0����ʾ����
                for(int i = 0; i < count; i++)
                {
                    stars[i].SetActive(true);
                }
            }
        }
    }

    public void Selected()
    {
        // ��ѡ��ؿ�ʱ�������ؿ������ִ����ȥ
        if (isSelect)
        {
            PlayerPrefs.SetString("nowLevel","level"+ gameObject.name);
            SceneManager.LoadScene(2);
        }
    }
}
