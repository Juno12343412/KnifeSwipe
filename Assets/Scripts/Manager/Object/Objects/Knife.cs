using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;
using Good;

public class Knife : PoolingObject
{
    public override string objectName => "BasicKnife";

       [Serializable]
    public struct Stats
    {
        public float knifeDamage;
        public float maxBounce;
        public float bounce;
        public float lifeTime;
    } [Header("Knife Stats")] public Stats stats;

    private GameObject target;
    private GameObject player;

    private Vector3 vecMove;
    private float moveSpeed = 10f;

    public override sealed void Init()
    {
        stats.maxBounce = PlayerStats.instance.stats.knifeMaxBounce;
        stats.knifeDamage = PlayerStats.instance.stats.knifeDamage;

        target = MathK.FindNearTarget("Enemy", gameObject);
        player = GameObject.Find("Line");
        vecMove = transform.position;

        stats.bounce = stats.maxBounce;
        base.Init();

        Invoke("Release", stats.lifeTime);
    }

    public override sealed void Release()
    {
        base.Release();
        CancelInvoke();
    }

    void Update()
    {
        if (stats.bounce <= 0)
            Release();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (target == null)
        {
            transform.position += vecMove.normalized * moveSpeed * Time.deltaTime;
            transform.rotation = MathK.LookAngle(vecMove);
        }
        else
        {
            if (target.activeSelf)
            {
                transform.position = Vector3.Lerp(transform.position, target.transform.position, 5f * Time.deltaTime);
                transform.rotation = MathK.LookAngle(target.transform.position - transform.position);
            }
            else
                target = null;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.gameObject.tag == "Enemy" || other.transform.gameObject.tag == "Boss")
        {
            target = MathK.FindNearTarget("Enemy", other.gameObject);

            if (target != null)
                stats.bounce--;
            else
                return;
            return;
        }
        else if (other.transform.tag == "Wall")
        {
            Vector2 incomingVec = Vector2.zero;
            if (target == null)
                incomingVec = other.transform.position - new Vector3(0f, -2f, 0f);
            else
                incomingVec = other.transform.position - target.transform.position;

            vecMove = incomingVec.normalized + other.contacts[0].normal * (-2 * Vector2.Dot(incomingVec.normalized, other.contacts[0].normal));
        }
    }
}
