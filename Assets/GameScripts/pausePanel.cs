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
        // 重新开始的按钮
    }

    public void Home()
    {
        // 返回主菜单的按钮
    }

    public void Pause()
    {
        // 暂停的按钮
        anim.SetBool("isPause", true);
        Pause_Button.SetActive(false);
    }

    public void Resume()
    {
        // 设置为1时才能进行动画的播放
        Time.timeScale = 1;
        // 继续的按钮
        anim.SetBool("isPause", false);
        Pause_Button.SetActive(true);
    }

    /// <summary>
    /// 暂停动画结束时将页面进行静止处理
    /// </summary>
    public void PauseAnimEnd()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// 继续动画结束时将页面进行继续处理
    /// </summary>
    public void ResumeAnimEnd()
    {
       
    }
}
