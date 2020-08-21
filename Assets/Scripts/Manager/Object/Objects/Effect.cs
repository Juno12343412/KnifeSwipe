using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;

public class Effect : PoolingObject
{
    private Animator myAnim;

    public override string objectName => "Effect";

    public override void Init()
    {
        myAnim = GetComponent<Animator>();
        Invoke("Release", 0.25f);
        base.Init();
    }

    private void Update()
    { 
        if (myAnim != null)
            myAnim.SetInteger("Hit", PlayerStats.instance.stats.knifeLv / 10);
    }

    public override void Release()
    {
        base.Release();
    }
}
