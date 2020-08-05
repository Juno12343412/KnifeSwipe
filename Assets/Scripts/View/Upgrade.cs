using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Upgrade : BaseScreen<Upgrade>
{
    [SerializeField] private Text upgradeCoinPrice, upgradeSpecialPrice;
    [SerializeField] private Text potentialPrice;
    [SerializeField] private Text knifeLv;
    [SerializeField] private Text knifeDmg;
    [SerializeField] private Text critPercent;
    [SerializeField] private Text critDmg;
    [SerializeField] private Text attackCount;
    [SerializeField] private Text bounce;
    [SerializeField] private Text totalDmg;

    public override void ShowScreen()
    {
        ResetUpgradeView();
        Mainu.instance.mainButtons["Upgrade"].isCheck = true;
        Mainu.instance.mainButtons["Upgrade"].myButton.GetComponentInChildren<Image>().sprite = Mainu.instance.checkImages[1];

        base.ShowScreen();
    }

    public override void HideScreen()
    {
        Mainu.instance.mainButtons["Upgrade"].isCheck = false;
        Mainu.instance.mainButtons["Upgrade"].myButton.GetComponentInChildren<Image>().sprite = Mainu.instance.checkImages[0];

        base.HideScreen();
    }

    public void OnUpgrade()
    {
        if (
           PlayerStats.instance.stats.knifeLv * 1000 * 1.2f <= PlayerStats.instance.stats.playerCoin &&
           PlayerStats.instance.stats.knifeLv * 10 <= PlayerStats.instance.stats.playerSpecialCoin
           )
        {
            PlayerStats.instance.stats.playerCoin -= (int)(PlayerStats.instance.stats.knifeLv * 1000 * 1.2f);
            PlayerStats.instance.stats.playerSpecialCoin -= (int)(PlayerStats.instance.stats.knifeLv * 10);
            PlayerStats.instance.stats.knifeLv++;
            PlayerStats.instance.stats.knifeDamage *= 2f;
            PlayerStats.instance.stats.knifeMaxBounce++;
            ResetUpgradeView();
        }
    }

    public void OnPotential()
    {
        if (PlayerStats.instance.stats.knifeLv * 500 * 1.2f <= PlayerStats.instance.stats.playerSpecialCoin)
        {
            PlayerStats.instance.stats.playerSpecialCoin -= (int)(PlayerStats.instance.stats.knifeLv * 500 * 1.2f);
            // To Do...
            ResetUpgradeView();
        }
    }

    void ResetUpgradeView()
    {
        int price = 0;

        price = (int)(PlayerStats.instance.stats.knifeLv * 1000 * 1.2f); Debug.Log(price);
        if (price < 1000)
            upgradeCoinPrice.text = price.ToString();
        else if (price >= 1000)
            upgradeCoinPrice.text = (price / 1000).ToString() + ".A";

        price = PlayerStats.instance.stats.knifeLv * 10; Debug.Log(price);
        if (price < 1000)
            upgradeSpecialPrice.text = price.ToString();
        else if (price >= 1000)
            upgradeSpecialPrice.text = (price / 1000).ToString() + ".A";

        price = (int)(PlayerStats.instance.stats.knifeLv * 500 * 1.2f); Debug.Log(price);
        if (price < 1000)
            potentialPrice.text = price.ToString();
        else if (price >= 1000)
            potentialPrice.text = (price / 1000).ToString() + ".A";

        knifeLv.text = PlayerStats.instance.stats.knifeLv.ToString() + "+";
        knifeDmg.text = PlayerStats.instance.stats.knifeDamage.ToString();
        critPercent.text = (PlayerStats.instance.stats.critPercentLv * 5).ToString() + "%";
        critDmg.text = (PlayerStats.instance.stats.critDamageLv * 20f).ToString() + "%";
        attackCount.text = PlayerStats.instance.stats.knifeAttackCount.ToString() + "번";
        bounce.text = PlayerStats.instance.stats.knifeMaxBounce.ToString() + "번";

        price = (int)(PlayerStats.instance.stats.knifeAttackCount * (PlayerStats.instance.stats.knifeDamage * (PlayerStats.instance.stats.critDamageLv * 20f)));
        if (price < 1000)
            totalDmg.text = price.ToString();
        else if (price >= 1000)
            totalDmg.text = (price / 1000).ToString() + ".A";
    }
}
