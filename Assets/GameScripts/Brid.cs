using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
/// <summary>
/// 
/// </summary>
public class Brid : MonoBehaviour
{
    private bool isClick = false;       // 判断鼠标是否按下
    public Transform rightPos;          // 右边的空白物体，由于限定最大距离用
    public float maxDis;                // 最大的距离
    [HideInInspector]
    public SpringJoint2D sp;           // 获取弹簧关节组件

    protected Rigidbody2D rb;

    public LineRenderer right;
    public LineRenderer left;
    public Transform rPos;
    public Transform leftPos;

    public GameObject boom;

    private bool canMove = true;

    public float smooth = 1;            // 相机移动的速率
    public AudioClip Select;            // 选择小鸟
    public AudioClip Flying;               // 小鸟飞出
    private bool isFly = false;         // 针对小黄鸟，当飞出时，速度才能加快
    public Sprite Hurt;
    protected SpriteRenderer HurtRender;
    /// </summary>
    //private MyTrail myTrail;
    private void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        //myTrail = GetComponent<MyTrail>();
        HurtRender = GetComponent<SpriteRenderer>();
    }
    // 当鼠标按下
    private void OnMouseDown()
    {
        if (canMove)
        {
            isClick = true;
            rb.isKinematic = true;                  // 当鼠标按下耳朵时候开启动力学
        }
        AudioPlay(Select);
    }

    // 当鼠标抬起
    private void OnMouseUp()
    {
        if (canMove)
        {
            //right.enabled = false;
            //left.enabled = false;
            isClick = false;
            rb.isKinematic = false;                 // 当鼠标按下耳朵时候结束动力学

            Invoke("Fly", 0.1f);                    // 在0.1秒之后才执行Fly方法

            canMove = false;
        }
        
    }

    private void Update()
    {
        if (isClick)
        {
            // unity中，那个小鸟的坐标是世界坐标，鼠标坐标的原点是左下角，所以还要进行坐标的转换
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 由于摄像机的z坐标是-10，所以小鸟的z坐标还要减去10才能变为0
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);


            // 如果小鸟超过了最大距离，就设定小鸟的距离为maxDis
            if (Vector3.Distance(transform.position,rightPos.position) > maxDis)
            {
                // 获取小鸟与固定点之间的向量（主要取方向）
                Vector3 distance = transform.position - rightPos.position;
                // 将向量进行单位化，只要方向，大小可以进行设定
                Vector3 distance2 = distance.normalized;
                // 将distance2乘以最大距离
                distance2 *= maxDis;
                // 重新设置小鸟的位置
                transform.position = distance2 + rightPos.position;
            }
        }
        if(sp.isActiveAndEnabled)
        {
            Line();
        }

        // Vector3.Lerp --- 实现相机跟随的效果，第一个参数：From.Position、第二个参数：To.Position、Time.DeltaTime
        float posX = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Mathf.Clamp(posX, 0, 15), Camera.main.transform.position.y, Camera.main.transform.position.z), smooth);
        if (isFly)
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                showSkill();
            }
        }
    }

    void Fly()
    {
        isFly = true;
        //myTrail.TrailStart();
        sp.enabled = false;
        Invoke("Next", 5f);
        AudioPlay(Flying);
    }

    // 第一只小鸟飞出去之后便进行销毁处理
    public virtual void Next()
    {
        // 先将list中清空其在的位置
        GameManager._instance.brids.Remove(this);
        // 销毁
        Destroy(gameObject);
        // 将销毁的效果显示出来
        Instantiate(boom,transform.position, Quaternion.identity);

        GameManager._instance.NextBrid();
    }

    // 画弹簧的线
    void Line()
    {
        //right.enabled = true;
        //left.enabled = true;

        // 右边画线
        right.SetPosition(0, rPos.position);
        right.SetPosition(1, transform.position);

        // 左边画线
        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }

    private void AudioPlay(AudioClip clip)
    {
        // 播放音乐片段，在此物体下播放clip片段
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    myTrail.ClearTrails();
    //}

    public virtual void showSkill()
    {
        isFly = false;
    }

    public void HurtPicture()
    {
        HurtRender.sprite = Hurt;
    }
    #region
    /// <summary>
    /// 清理拖尾效果
    /// </summary>
    //public void ClearTrail()
    //{

    //}
    #endregion

}
