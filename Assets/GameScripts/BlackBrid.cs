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

        // ����list�������ֵ�Ļ����Ҵ���0�Ļ�
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
    /// ��������뱬ը��Χ�ڵĻ���������ӵ�list��
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
    /// �������˳���Χʱ�������Ƴ�list
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
        rb.velocity = Vector3.zero; // �ٶ�����
        Instantiate(boom, transform.position, Quaternion.identity);     // ���ű�ըЧ��
        HurtRender.enabled = false;                                     // ��SprteRenderer���ȡ��
        GetComponent<CircleCollider2D>().enabled = false;               // ����ײ���ȡ��
    }

    /// <summary>
    /// ��дNext��������Ϊ��Clear���Ѿ�����boom��Ч��������next�����轫boom��ʾ����
    /// </summary>
    public override void Next()
    {
        // �Ƚ�list��������ڵ�λ��
        GameManager._instance.brids.Remove(this);
        // ����
        Destroy(gameObject);

        GameManager._instance.NextBrid();
    }
}
