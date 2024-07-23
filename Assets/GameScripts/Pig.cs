using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class Pig : MonoBehaviour
{
    // 本脚本的逻辑是通过判断小鸟和猪碰撞后的相对速度来决定猪的状态

    public float maxSpeed = 10;
    public float minSpeed = 5;
    private SpriteRenderer SR;
    public Sprite Hurt;
    public GameObject boom;
    public GameObject Score;
    public AudioClip HurtClip;
    public AudioClip DeadClip;
    public AudioClip bridCollision;
    /// <summary>
    /// 这里用bool的原因是木头和猪的逻辑是差不多的，仅仅是速度的大小问题，所以要判断物体是哪个
    /// </summary>
    public bool isPig = false;              

    private void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 这里的形参指的是碰撞体，不是被碰撞的物体
    /// </summary>
    /// <param name="collision"></param>
    // 小鸟与猪之间的碰撞
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果碰到了猪或者木头的话，就播放碰撞的音效
        if(collision.transform.tag == "player")
        {
            AudioPlay(bridCollision);
            collision.gameObject.GetComponent<Brid>().HurtPicture();
        }

        // 相对速度大于最高的，猪死亡
        if(collision.relativeVelocity.magnitude > maxSpeed)
        {
            Dead();

        }else if(collision.relativeVelocity.magnitude > minSpeed && collision.relativeVelocity.magnitude < maxSpeed)
        {
            SR.sprite = Hurt;
            AudioPlay(HurtClip);
        }
    }

    public void Dead()
    {
        if (isPig)
        {
            // 移除当前猪所在list中的位置
            GameManager._instance.pigs.Remove(this);
        }
        // 销毁猪的对象
        Destroy(gameObject);
        // 将爆炸的动画放置在猪销毁的地方上
        Instantiate(boom,transform.position, Quaternion.identity);
        // 将分数显示出来 ---- 获取了那个子组件，将其在猪的上方显示
        GameObject go = Instantiate(Score,transform.position + new Vector3(0,0.5f,0), Quaternion.identity);
        // 在1.5秒之后显示出来
        Destroy(go, 1.5f);

        AudioPlay(DeadClip);
    }

    private void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}