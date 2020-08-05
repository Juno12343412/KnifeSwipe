using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Boss : BaseScreen<Boss>
{
    [SerializeField] private Text bossEventPrice;
    [SerializeField] private Text bossClearCoin;
    [SerializeField] private GameObject[] bossObjs;

    [HideInInspector] public bool isBoss = false;
    [HideInInspector] public int  BossDifficult = 0;

    public override void ShowScreen()
    {
        Mainu.instance.mainButtons["Boss"].isCheck = true;
        Mainu.instance.mainButtons["Boss"].myButton.GetComponentInChildren<Image>().sprite = Mainu.instance.checkImages[1];

        base.ShowScreen();
    }

    public override void HideScreen()
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
    }
}
