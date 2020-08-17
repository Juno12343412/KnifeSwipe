using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using KnifeSwipe;
using Good;

public class Upgrade : BaseScreen<Upgrade>
{
    [SerializeField] private Image knifeImg;
    [SerializeField] private Text upgradeCoinPrice, upgradeSpecialPrice;
    [SerializeField] private Text potentialPrice;
    [SerializeField] private Text knifeLv;
    [SerializeField] private Text knifeDmg;
    [SerializeField] private Text critPercent;
    [SerializeField] private Text critDmg;
    [SerializeField] private Text attackCount;
    [SerializeField] private Text bounce;
    [SerializeField] private Text totalDmg;

    private void Start()
    {
        ResetUpgradeView();
    }

    public override sealed void ShowScreen()
    {
        ResetUpgradeView();
        base.ShowScreen();
    }

    public override sealed void HideScreen()
    {
        base.HideScreen();
    }

    public void OnUpgrade()
    {
        if (ETC.GetCoin(PlayerStats.instance.stats.knifeLv) <= PlayerStats.instance.stats.playerCoin &&
            ETC.GetCoin(PlayerStats.instance.stats.knifeLv / 2f) <= PlayerStats.instance.stats.playerSpecialCoin)
        {
            PlayerStats.instance.stats.playerCoin -= (int)(ETC.GetCoin(PlayerStats.instance.stats.knifeLv));
            PlayerStats.instance.stats.playerSpecialCoin -= (int)(ETC.GetCoin(PlayerStats.instance.stats.knifeLv / 2f));
            PlayerStats.instance.stats.knifeLv++;
            PlayerStats.instance.stats.knifeDamage = ETC.GetAttack(PlayerStats.instance.stats.knifeLv);
            PlayerStats.instance.stats.knifeMaxBounce++;
            ResetUpgradeView();
        }
        Controller.instance.ChanageKnife();
        knifeImg.sprite = PlayerStats.instance.knifeImgs[PlayerStats.instance.stats.knifeLv - 1];
    }

    public void OnPotential()
    {
        if (ETC.GetCoin(PlayerStats.instance.stats.knifeLv * 1.2f) <= PlayerStats.instance.stats.playerSpecialCoin)
        {
            PlayerStats.instance.stats.playerSpecialCoin -= (int)ETC.GetCoin(PlayerStats.instance.stats.knifeLv * 1.2f);
            // To Do...
            ResetUpgradeView();
        }
    }

    void ResetUpgradeView()
    {
        int price = 0;

        price = (int)ETC.GetCoin(PlayerStats.instance.stats.knifeLv); 
        upgradeCoinPrice = ETC.Calculation(upgradeCoinPrice, price);

        price = (int)ETC.GetCoin(PlayerStats.instance.stats.knifeLv / 2f); 
        upgradeSpecialPrice = ETC.Calculation(upgradeSpecialPrice, price);

        price = (int)ETC.GetCoin(PlayerStats.instance.stats.knifeLv * 1.2f); 
        potentialPrice = ETC.Calculation(potentialPrice, price);


        knifeLv.text = PlayerStats.instance.stats.knifeLv.ToString() + "+";
        knifeDmg = ETC.Calculation(knifeDmg, (int)PlayerStats.instance.stats.knifeDamage);
        critPercent.text = ETC.GetCritHit(PlayerStats.instance.stats.critPercentLv).ToString() + "%";
        critDmg.text = (ETC.GetCritDmg(PlayerStats.instance.stats.critDamageLv)).ToString() + "%";
        attackCount.text = PlayerStats.instance.stats.knifeAttackCount.ToString() + "번";
        bounce.text = PlayerStats.instance.stats.knifeMaxBounce.ToString() + "번";

        price = (int)((100f + PlayerStats.instance.stats.knifeDamage) * (ETC.GetCritDmg(PlayerStats.instance.stats.critDamageLv / 100f) * 100f / 100f));
        totalDmg = ETC.Calculation(totalDmg, price);

    }
}
