using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;
using System;

public class Knife : PoolingObject
{
    public override string objectName => "BasicKnife";

       [Serializable]
    public struct Stats
    {
        public float knifeDamage;
        public float maxBounce;
        public float bounce;
    } [Header("Knife Stats")] public Stats stats;

    private GameObject target;
    private GameObject player;

    private Vector3 vecMove;
    private float moveSpeed = 10f;

    public override void Init()
    {
        stats.maxBounce = PlayerStats.instance.stats.knifeMaxBounce;
        stats.knifeDamage = PlayerStats.instance.stats.knifeDamage;

        target = null;
        player = GameObject.Find("Line");
        vecMove = transform.position;
        stats.bounce = stats.maxBounce;
        base.Init();

        Invoke("Release", 10f);
    }

    public override void Release()
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
        if (target == null)
            transform.position += vecMove.normalized * moveSpeed * Time.deltaTime;
        else
        {
            if (target.activeSelf)
                transform.position = Vector3.Lerp(transform.position, target.transform.position, 5f * Time.deltaTime);
            else
                target = null;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.gameObject.tag == "Enemy" || other.transform.gameObject.tag == "Boss")
        {
            target = FindNearTarget("Enemy", other.gameObject);

            if (target == null)
                return;
            else
            {
                Vector3 dir = target.transform.position - transform.position;
                float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                stats.bounce--;
            }
        }
        if (other.transform.tag == "Wall")
        {
            Vector2 incomingVec = Vector2.zero;
            if (target == null)
                incomingVec = other.transform.position - new Vector3(0f, -2f, 0f);
            else
                incomingVec = other.transform.position - target.transform.position;
            Debug.Log(incomingVec);

            vecMove = incomingVec.normalized + other.contacts[0].normal * (-2 * Vector2.Dot(incomingVec.normalized, other.contacts[0].normal));

            float angle = Mathf.Atan2(vecMove.x, vecMove.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.transform.tag == "Wall")
        {
            Vector2 incomingVec = Vector2.zero;
            if (target == null)
                incomingVec = other.transform.position - new Vector3(0f, -2f, 0f);
            else
                incomingVec = other.transform.position - target.transform.position;
            Debug.Log(incomingVec);

            vecMove = incomingVec.normalized + other.contacts[0].normal * (-2 * Vector2.Dot(incomingVec.normalized, other.contacts[0].normal));

            float angle = Mathf.Atan2(vecMove.x, vecMove.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
        }
    }

    GameObject FindNearTarget(string tag, GameObject myObj)
    {
        List<GameObject> objects = new List<GameObject>(GameObject.FindGameObjectsWithTag(tag));
        if (objects == null || objects.Count <= 1) return null; objects.Remove(myObj);

        GameObject nearTarget = objects[0];
        float nearDistance = Vector3.Distance(gameObject.transform.position, nearTarget.transform.position);
        foreach (var obj in objects)
        {
            float distance = Vector3.Distance(gameObject.transform.position, obj.transform.position);
            if (distance < nearDistance)
            {
                nearTarget = obj;
                nearDistance = distance;
            }
        }
        return nearTarget;
    }
}
