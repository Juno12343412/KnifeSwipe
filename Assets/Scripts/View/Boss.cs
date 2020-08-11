using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Boss : BaseScreen<Boss>
{
    [SerializeField] private Text bossEventPrice;
    [SerializeField] private Text bossClearCoin;
    [SerializeField] private GameObject[] bossObjs;
    [SerializeField] private GameObject[] backGrounds;

    [SerializeField] private Image bossHpBar;
    [SerializeField] private Text  bossName;
    [SerializeField] private Text  bossHp;

    [HideInInspector] public bool isBoss = false;
    [HideInInspector] public int  BossDifficult = 0;

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
        isBoss = true;

        Instantiate(bossObjs[0], GameObject.Find("BossSpawner").transform);
        StartCoroutine(checkBoss());

        SwipeManager.instance.ChanageView(ViewState.Ingame, 0f);
    }

    IEnumerator checkBoss()
    {
        backGrounds[0].SetActive(false);
        backGrounds[1].SetActive(true);
        backGrounds[2].SetActive(true);
        backGrounds[3].SetActive(false);
        backGrounds[4].SetActive(false);
        while (isBoss)
        {
            bossHpBar.fillAmount = (BossMonster.instance.enemyHP - 0) / (BossMonster.instance.maxHP - 0);
            bossHp.text = ((int)BossMonster.instance.enemyHP).ToString();
            yield return null;
        }
        backGrounds[0].SetActive(true);
        backGrounds[1].SetActive(false);
        backGrounds[2].SetActive(false);
        backGrounds[3].SetActive(true);
        backGrounds[4].SetActive(true);
    }
}
