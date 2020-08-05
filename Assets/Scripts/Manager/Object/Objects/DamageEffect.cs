using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;
using UnityEngine.UI;

public class DamageEffect : PoolingObject
{
    [HideInInspector] public int getDamage;
    public override string objectName => "Effect";
    public Text damageText;

    void FixedUpdate()
    {
        transform.position += Vector3.up * Time.deltaTime;
        damageText.color -= new Color(0, 0, 0, Time.deltaTime);
        damageText.text = getDamage.ToString();
    }

    public override void Init()
    {
        Invoke("Release", 2.5f);
        damageText.color = new Color(255, 241, 85, 1f);
        base.Init();
    }

    public override void Release()
    {
        base.Release();
    }
}
