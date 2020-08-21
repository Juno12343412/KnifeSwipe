using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;
using UnityEngine.UI;
using Good;

public class DamageEffect : PoolingObject
{
    [HideInInspector] public float getDamage = 0;
    [HideInInspector] public bool isCrit = false;
    public override string objectName => "Effect";
    public Text damageText;

    void FixedUpdate()
    {
        transform.position += Vector3.up * Time.deltaTime;
        damageText.color -= new Color(0, 0, 0, Time.deltaTime);
        damageText = ETC.Calculation(damageText, getDamage);
        if (!isCrit)
            damageText.fontSize = 45;
        else
            damageText.fontSize = 90;
    }

    public override void Init()
    {
        damageText.color = new Color(255, 255, 255, 1);
        Invoke("Release", 2.5f);
        base.Init();
    }

    public override void Release()
    {
        base.Release();
    }
}
