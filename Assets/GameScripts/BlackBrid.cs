using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class BlackBrid : Brid
{
    public List<Pig> blocks = new List<Pig>();

    public override void showSkill()
    {
        base.showSkill();

        // 遍历list，如果有值的话，且大于0的话
        if(blocks.Count >0 && blocks != null)
        {
            for(int i = 0;i< blocks.Count; i++)
            {
                blocks[i].Dead();
            }
        }

        Clear();
    }

    /// <summary>
    /// 当物体进入爆炸范围内的话，将其添加到list中
    /// </summary>
    /// <param name="collision"></param>s
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pig")
        {
            blocks.Add(collision.gameObject.GetComponent<Pig>());
        }
    }

    /// <summary>
    /// 当物体退出范围时，将其移出list
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pig")
        {
            blocks.Remove(collision.gameObject.GetComponent<Pig>());
  
        }

        
    }

    void Clear()
    {
        rb.velocity = Vector3.zero; // 速度清零
        Instantiate(boom, transform.position, Quaternion.identity);     // 播放爆炸效果
        HurtRender.enabled = false;                                     // 将SprteRenderer组件取消
        GetComponent<CircleCollider2D>().enabled = false;               // 将碰撞组件取消
    }

    /// <summary>
    /// 重写Next方法，因为在Clear中已经有了boom特效，所以在next中无需将boom显示出来
    /// </summary>
    public override void Next()
    {
        // 先将list中清空其在的位置
        GameManager._instance.brids.Remove(this);
        // 销毁
        Destroy(gameObject);

        GameManager._instance.NextBrid();
    }
}
