using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;

public class Ingame : BaseScreen<Ingame>
{
    [Header("IN Game")]
    [Tooltip("몬스터들 오브젝트")] public GameObject[] objEnemys; 
    [Tooltip("효과 오브젝트")] public GameObject objEffect;

    ObjectPool<Enemy> poolEnemy = new ObjectPool<Enemy>();
    public ObjectPool<DamageEffect> poolDamageEffect = new ObjectPool<DamageEffect>();

    void Start()
    {
        //HideScreen();
        poolEnemy.Init(objEnemys[0], 5);
        poolDamageEffect.Init(objEffect, 5, Vector3.zero, Quaternion.identity, GameObject.Find("EffectCanvas").transform);
    }

    void Update()
    {
        EnemySpawn();
    }

    public override void ShowScreen()
    {
        base.ShowScreen();
    }

    public override void HideScreen()
    {
        base.HideScreen();
    }

    public void EnemySpawn()
    {
        foreach (var obj in poolEnemy)
        {
            if (obj.isActive == true)
                return;
        }
        StartCoroutine(SpawnEnemys());
    }

    IEnumerator SpawnEnemys()
    {
        int length = 0;
        while (length <= 5)
        {
            length++;
            poolEnemy.Spawn();
            yield return new WaitForSeconds(0.25f);
        }
    }
}
