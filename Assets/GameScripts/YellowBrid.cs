using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class YellowBrid : Brid
{
    public override void showSkill()
    {
        base.showSkill();

        rb.velocity *= 2;           // �ٶȱ�Ϊ2��
    }
}
