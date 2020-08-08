﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;
using System;

public class Ingame : BaseScreen<Ingame>
{
    [Header("IN Game")]
    [Tooltip("몬스터들 오브젝트")] public GameObject[] objEnemys; 
    [Tooltip("효과 오브젝트")] public GameObject objEffect;

    ObjectPool<Enemy> poolEnemy = new ObjectPool<Enemy>();
    public ObjectPool<DamageEffect> poolDamageEffect = new ObjectPool<DamageEffect>();

    bool isSpawn = false;

    void Start()
    {
        //HideScreen();
        poolEnemy.Init(objEnemys[0], 5);
        poolDamageEffect.Init(objEffect, 5, Vector3.zero, Quaternion.identity, GameObject.Find("EffectCanvas").transform);
    }

    void Update()
    {
        if (!Boss.instance.isBoss)
            EnemySpawn();
    }

    public override sealed void ShowScreen()
    {
        base.ShowScreen();
    }

    public override sealed void HideScreen()
    {
        base.HideScreen();
    }

    void EnemySpawn()
    {
        foreach (var obj in poolEnemy)
        {
            if (obj.isActive == true)
                return;
        }
        if (!isSpawn)
            StartCoroutine(SpawnEnemys());
    }

    IEnumerator SpawnEnemys()
    {
        isSpawn = true;
        int length = 0;

        while (length <= 5)
        {
            length++;
            poolEnemy.Spawn();
            yield return new WaitForSeconds(0.15f);
        }
        isSpawn = false;
    }
}
