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
    private bool isClick = false;       // �ж�����Ƿ���
    public Transform rightPos;          // �ұߵĿհ����壬�����޶���������
    public float maxDis;                // ���ľ���
    [HideInInspector]
    public SpringJoint2D sp;           // ��ȡ���ɹؽ����

    protected Rigidbody2D rb;

    public LineRenderer right;
    public LineRenderer left;
    public Transform rPos;
    public Transform leftPos;

    public GameObject boom;

    private bool canMove = true;

    public float smooth = 1;            // ����ƶ�������
    public AudioClip Select;            // ѡ��С��
    public AudioClip Flying;               // С��ɳ�
    private bool isFly = false;         // ���С���񣬵��ɳ�ʱ���ٶȲ��ܼӿ�
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
    // ����갴��
    private void OnMouseDown()
    {
        if (canMove)
        {
            isClick = true;
            rb.isKinematic = true;                  // ����갴�¶���ʱ��������ѧ
        }
        AudioPlay(Select);
    }

    // �����̧��
    private void OnMouseUp()
    {
        if (canMove)
        {
            //right.enabled = false;
            //left.enabled = false;
            isClick = false;
            rb.isKinematic = false;                 // ����갴�¶���ʱ���������ѧ

            Invoke("Fly", 0.1f);                    // ��0.1��֮���ִ��Fly����

            canMove = false;
        }
        
    }

    private void Update()
    {
        if (isClick)
        {
            // unity�У��Ǹ�С����������������꣬��������ԭ�������½ǣ����Ի�Ҫ���������ת��
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // �����������z������-10������С���z���껹Ҫ��ȥ10���ܱ�Ϊ0
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);


            // ���С�񳬹��������룬���趨С��ľ���ΪmaxDis
            if (Vector3.Distance(transform.position,rightPos.position) > maxDis)
            {
                // ��ȡС����̶���֮�����������Ҫȡ����
                Vector3 distance = transform.position - rightPos.position;
                // ���������е�λ����ֻҪ���򣬴�С���Խ����趨
                Vector3 distance2 = distance.normalized;
                // ��distance2����������
                distance2 *= maxDis;
                // ��������С���λ��
                transform.position = distance2 + rightPos.position;
            }
        }
        if(sp.isActiveAndEnabled)
        {
            Line();
        }

        // Vector3.Lerp --- ʵ����������Ч������һ��������From.Position���ڶ���������To.Position��Time.DeltaTime
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

    // ��һֻС��ɳ�ȥ֮���������ٴ���
    public virtual void Next()
    {
        // �Ƚ�list��������ڵ�λ��
        GameManager._instance.brids.Remove(this);
        // ����
        Destroy(gameObject);
        // �����ٵ�Ч����ʾ����
        Instantiate(boom,transform.position, Quaternion.identity);

        GameManager._instance.NextBrid();
    }

    // �����ɵ���
    void Line()
    {
        //right.enabled = true;
        //left.enabled = true;

        // �ұ߻���
        right.SetPosition(0, rPos.position);
        right.SetPosition(1, transform.position);

        // ��߻���
        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }

    private void AudioPlay(AudioClip clip)
    {
        // ��������Ƭ�Σ��ڴ������²���clipƬ��
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
    /// ������βЧ��
    /// </summary>
    //public void ClearTrail()
    //{

    //}
    #endregion

}
