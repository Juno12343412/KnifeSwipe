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
        Mainu.instance.mainButtons["Boss"].isCheck = true;
        Mainu.instance.mainButtons["Boss"].myButton.GetComponentInChildren<Image>().sprite = Mainu.instance.checkImages[1];

        base.ShowScreen();
    }

    public override sealed void HideScreen()
    {
        Mainu.instance.mainButtons["Boss"].isCheck = false;
        Mainu.instance.mainButtons["Boss"].myButton.GetComponentInChildren<Image>().sprite = Mainu.instance.checkImages[0];

        base.HideScreen();
    }

    public void OnBoss()
    {
        isBoss = true;

        Instantiate(bossObjs[0], GameObject.Find("BossSpawner").transform);
        Ingame.instance.ShowScreen();
        Stats.instance.HideScreen();
        Upgrade.instance.HideScreen();
        HideScreen();
        StartCoroutine(checkBoss());
    }

    IEnumerator checkBoss()
    {
        backGrounds[0].SetActive(false);
        backGrounds[1].SetActive(true);
        backGrounds[2].SetActive(true);
        Mainu.instance.HideScreen();
        while (isBoss)
        {
            bossHpBar.fillAmount = (BossMonster.instance.enemyHP - 0) / (BossMonster.instance.maxHP - 0);
            bossHp.text = ((int)BossMonster.instance.enemyHP).ToString();
            yield return null;
        }
        backGrounds[0].SetActive(true);
        backGrounds[1].SetActive(false);
        backGrounds[2].SetActive(false);
        Mainu.instance.ShowScreen();
    }
}
