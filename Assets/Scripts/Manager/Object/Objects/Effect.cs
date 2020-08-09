using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;

public class Effect : PoolingObject
{
    public override string objectName => "Effect";

    public override void Init()
    {
        Invoke("Release", 0.5f);
        base.Init();
    }

    public override void Release()
    {
        base.Release();
    }
}
