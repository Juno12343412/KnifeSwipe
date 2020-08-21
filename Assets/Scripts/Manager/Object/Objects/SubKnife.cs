using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;
using Good;

public class SubKnife : PoolingObject
{
    GameObject target = null;
    Vector3 direction;
    float speed = 0f;
    public override string objectName => "SubKnife";

    public override sealed void Init()
    {
        if (tag == "MekaGear")
        {
            target = GameObject.FindGameObjectWithTag("Enemy");
            speed = 5f;
        }
        else if (tag == "SwordOra")
        {
            speed = 10f;
            direction = transform.position.normalized;
        }
        Invoke("Release", 2.5f);
        base.Init();
    }

    public override sealed void Release()
    {
        base.Release();
    }

    void FixedUpdate()
    {
        if (tag == "MekaGear")
        {
            if (!target.activeSelf)
                target = GameObject.FindGameObjectWithTag("Enemy");
            direction = target.transform.position - transform.position;
        }
        transform.position += direction * speed * Time.deltaTime;
        transform.rotation = MathK.LookAngle(direction);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && gameObject.CompareTag("MekaGear"))
        {
            CancelInvoke();
            Release();
        }
    }
}
