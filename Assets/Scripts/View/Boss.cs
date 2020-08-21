using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Manager.Sound;
using Good;

public class Boss : BaseScreen<Boss>
{
    [SerializeField] private Text bossEventPrice;
    [SerializeField] private Text bossClearCoin;
    [SerializeField] private GameObject[] bossObjs;
    [SerializeField] private GameObject[] backGrounds;

    [SerializeField] private Image bossHpBar;
    [SerializeField] private Text  bossName;
    [SerializeField] private Text  bossHp;
    [SerializeField] private Text  bossTimer;
                     private Text  bossStageHP;

    [SerializeField] private Text bossStatsText;
    [SerializeField] private Text bossInText;
    [SerializeField] private Text bossResultCoin;
    [SerializeField] private Text bossResultSCoin;
    [SerializeField] private Image bossImage;
    [SerializeField] private Sprite[] bossImages;
    [SerializeField] private Image imageSprite;
    [SerializeField] private Sprite[] bossSprites;

    [HideInInspector] public bool isBoss = false;
    [HideInInspector] public float curBossTime = 0f;
    [HideInInspector] public int  BossDifficult = 0;

    [HideInInspector] public int bossInPrice = 0;
    [HideInInspector] public int bossResult = 0;
    [HideInInspector] public int bossResultS = 0;
    [HideInInspector] public string bossNameTag = "";
    [HideInInspector] public float bossHP = 0f;

    void Start()
    {
        bossStageHP = gameObject.AddComponent<Text>();
    }

    public override sealed void ShowScreen()
    {
        base.ShowScreen();
    }

    public override sealed void HideScreen()
    {
        base.HideScreen();
    }

    public void OnBoss()
    {
        if (PlayerStats.instance.stats.playerCoin >= bossInPrice)
        {
            PlayerStats.instance.stats.playerCoin -= bossInPrice;
            isBoss = true;

            Instantiate(bossObjs[0], GameObject.Find("BossSpawner").transform);
            StartCoroutine(checkBoss());

            SwipeManager.instance.ChanageView(ViewState.Ingame, 0f);
            ResetBossView();
        }
    }

    public void ResetBossView()
    {
        switch (PlayerStats.instance.stats.curStage)
        {
            case 0:
                curBossTime = 20f;
                bossInPrice = 1000;
                bossResult = 10000;
                bossResultS = 5000;
                bossHP = 10000f;
                bossNameTag = "버려진 라이터";
                Ingame.instance.spawnMob = 10;
                break;
            case 1:
                curBossTime = 30f;
                bossInPrice = 5000;
                bossResult = 50000;
                bossResultS = 25000;
                bossHP = 200000f;
                bossNameTag = "미세 플라스틱";
                Ingame.instance.spawnMob = 12;
                break;
            case 2:
                curBossTime = 40f;
                bossInPrice = 10000;
                bossResult = 100000;
                bossResultS = 50000;
                bossHP = 1000000f;
                bossNameTag = "이산화 탄소";
                Ingame.instance.spawnMob = 14;
                break;
            case 3:
                curBossTime = 50f;
                bossInPrice = 30000;
                bossResult = 300000;
                bossResultS = 150000;
                bossHP = 500000000f;
                bossNameTag = "폐기물 혼합체";
                Ingame.instance.spawnMob = 17;
                break;
            default:
                break;
        }
        bossImage.sprite = bossImages[PlayerStats.instance.stats.curStage];
        imageSprite.sprite = bossSprites[PlayerStats.instance.stats.curStage];
        bossStageHP = ETC.Calculation(bossStageHP, bossHP);
        bossStatsText.text = "[" + bossNameTag.ToString() + "]" + "\n" + "HP : ";
        bossStatsText.text += bossStageHP.text;
        bossInText = ETC.Calculation(bossInText, bossInPrice);
        bossResultCoin = ETC.Calculation(bossResultCoin, bossResult);
        bossResultSCoin = ETC.Calculation(bossResultSCoin, bossResultS);
    }

    IEnumerator checkBoss()
    {
        SoundManager.instance.StopBGM();
        SoundManager.instance.PlayLoopSound("보스전 배경음악");

        if (PlayerStats.instance.stats.curStage < backGrounds.Length)
            Ingame.instance.backGrounds[PlayerStats.instance.stats.curStage].SetActive(false);
        else
            Ingame.instance.backGrounds[backGrounds.Length - 1].SetActive(false);

        backGrounds[0].SetActive(true);
        backGrounds[1].SetActive(true);
        backGrounds[2].SetActive(false);
        backGrounds[3].SetActive(false);
        backGrounds[4].SetActive(false);
        bossName.text = bossNameTag;

        while (isBoss && 0f <= curBossTime)
        {
            curBossTime -= Time.deltaTime;
            bossHpBar.fillAmount = BossMonster.instance.enemyHP / bossHP;
            bossTimer.text = ((int)curBossTime).ToString();
            bossHp.text = ((int)BossMonster.instance.enemyHP).ToString();
            yield return null;
        }
        backGrounds[0].SetActive(false);
        backGrounds[1].SetActive(false);
        backGrounds[2].SetActive(true);
        backGrounds[3].SetActive(true);
        backGrounds[4].SetActive(true);
        isBoss = false;
        SoundManager.instance.StopBGM();
        Ingame.instance.ShowBackGround();
        ResetBossView();
    }
}