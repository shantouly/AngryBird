using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class Pig : MonoBehaviour
{
    // ���ű����߼���ͨ���ж�С�������ײ�������ٶ����������״̬

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
    /// ������bool��ԭ����ľͷ������߼��ǲ��ģ��������ٶȵĴ�С���⣬����Ҫ�ж��������ĸ�
    /// </summary>
    public bool isPig = false;              

    private void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// ������β�ָ������ײ�壬���Ǳ���ײ������
    /// </summary>
    /// <param name="collision"></param>
    // С������֮�����ײ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ��������������ľͷ�Ļ����Ͳ�����ײ����Ч
        if(collision.transform.tag == "player")
        {
            AudioPlay(bridCollision);
            collision.gameObject.GetComponent<Brid>().HurtPicture();
        }

        // ����ٶȴ�����ߵģ�������
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
            // �Ƴ���ǰ������list�е�λ��
            GameManager._instance.pigs.Remove(this);
        }
        // ������Ķ���
        Destroy(gameObject);
        // ����ը�Ķ��������������ٵĵط���
        Instantiate(boom,transform.position, Quaternion.identity);
        // ��������ʾ���� ---- ��ȡ���Ǹ������������������Ϸ���ʾ
        GameObject go = Instantiate(Score,transform.position + new Vector3(0,0.5f,0), Quaternion.identity);
        // ��1.5��֮����ʾ����
        Destroy(go, 1.5f);

        AudioPlay(DeadClip);
    }

    private void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}