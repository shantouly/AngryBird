using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class pausePanel : MonoBehaviour
{
    private Animator anim;
    public GameObject Pause_Button;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void ReTry()
    {
        // ���¿�ʼ�İ�ť
    }

    public void Home()
    {
        // �������˵��İ�ť
    }

    public void Pause()
    {
        // ��ͣ�İ�ť
        anim.SetBool("isPause", true);
        Pause_Button.SetActive(false);
    }

    public void Resume()
    {
        // ����Ϊ1ʱ���ܽ��ж����Ĳ���
        Time.timeScale = 1;
        // �����İ�ť
        anim.SetBool("isPause", false);
        Pause_Button.SetActive(true);
    }

    /// <summary>
    /// ��ͣ��������ʱ��ҳ����о�ֹ����
    /// </summary>
    public void PauseAnimEnd()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// ������������ʱ��ҳ����м�������
    /// </summary>
    public void ResumeAnimEnd()
    {
       
    }
}
