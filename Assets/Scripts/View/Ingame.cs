using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pooling;
using System;
using Manager.Sound;

public class Ingame : BaseScreen<Ingame>
{
    [Header("IN Game")]
    [SerializeField] private GameObject howtoObj;
    [Tooltip("몬스터들 오브젝트")] public GameObject[] objEnemys; 
    [Tooltip("효과 오브젝트")] public GameObject[] objEffect;
    [Tooltip("백그라운드들")] public GameObject[] backGrounds;  

    public ObjectPool<Enemy> poolEnemy = new ObjectPool<Enemy>();
    public ObjectPool<DamageEffect> poolDamageEffect = new ObjectPool<DamageEffect>();
    public ObjectPool<Effect> poolHitEffect = new ObjectPool<Effect>();
    public ObjectPool<SubKnife> poolMekaGearEffect = new ObjectPool<SubKnife>();
    public ObjectPool<SubKnife> poolSwordOraEffect = new ObjectPool<SubKnife>();

    public int spawnMob = 10; 

    [SerializeField] private Text chapterText = null;
    bool isSpawn = false;

    void Start()
    {
        ShowBackGround();
        poolEnemy.Init(objEnemys[0], 10);
        poolDamageEffect.Init(objEffect[0], 10, Vector3.zero, Quaternion.identity, GameObject.Find("EffectCanvas").transform);
        poolHitEffect.Init(objEffect[1], 10, Vector3.zero, Quaternion.identity);
        poolMekaGearEffect.Init(objEffect[2], 10, Vector3.zero, Quaternion.identity);
        poolSwordOraEffect.Init(objEffect[3], 10, Vector3.zero, Quaternion.identity);
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

    public void ShowBackGround()
    {
        SoundManager.instance.StopBGM();

        if (PlayerStats.instance.stats.curStage < backGrounds.Length)
        {
            for (int i = 0; i < backGrounds.Length; i++)
                backGrounds[i].SetActive(false);
            backGrounds[PlayerStats.instance.stats.curStage].SetActive(true);
        }
        else
        {
            for (int i = 0; i < backGrounds.Length; i++)
                backGrounds[i].SetActive(false);
            backGrounds[backGrounds.Length - 1].SetActive(true);
        }

        switch (PlayerStats.instance.stats.curStage)
        {
            case 0:
                SoundManager.instance.PlayLoopSound("스테이지 1 배경음악");
                chapterText.text = "챕터 1 - 산속 무단 쓰레기 투기장";
                break;
            case 1:
                SoundManager.instance.PlayLoopSound("스테이지 2 배경음악");
                chapterText.text = "챕터 2 - 더럽혀진 해변";
                break;
            case 2:
                SoundManager.instance.PlayLoopSound("스테이지 3 배경음악");
                chapterText.text = "챕터 3 - 녹아가는 남극";
                break;
            case 3:
                SoundManager.instance.PlayLoopSound("스테이지 4 배경음악");
                chapterText.text = "챕터 4 - 쓰레기의 근원지 폐공장";
                break;
            default:
                break;
        }
    }

    IEnumerator SpawnEnemys()
    {
        isSpawn = true;
        int length = 0;

        while (length <= spawnMob && SwipeManager.instance.mainButtons[ViewState.Ingame].isCheck)
        {
            length++;
            poolEnemy.Spawn();
            yield return new WaitForSeconds(0.15f);
        }
        isSpawn = false;
    }

    public void OnExit()
    {
        Boss.instance.isBoss = false;
    }

    public void OnHowTo()
    {
        howtoObj.gameObject.SetActive(!howtoObj.gameObject.activeSelf);
    }
}
