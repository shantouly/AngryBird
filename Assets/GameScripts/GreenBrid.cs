using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
/// <summary>
/// 
/// </summary>
public class GreenBrid : Brid
{
    public override void showSkill()
    {
        base.showSkill();

        Vector3 speed = rb.velocity;
        speed.x *= -1;
        rb.velocity = speed;
    }
}
