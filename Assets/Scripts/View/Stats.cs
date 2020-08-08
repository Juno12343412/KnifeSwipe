using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Stats : BaseScreen<Stats>
{
    // Crit Percent
    [SerializeField] private Text crit1Lv ,crit1NextLv;
    [SerializeField] private Text crit1Percent;
    [SerializeField] private Text crit1Price;

    // Crit Damage
    [SerializeField] private Text crit2Lv, crit2NextLv;
    [SerializeField] private Text crit2Damage;
    [SerializeField] private Text crit2Price;

    // Coin Percent
    [SerializeField] private Text coinLv, coinNextLv;
    [SerializeField] private Text coinPercent;
    [SerializeField] private Text coinPrice;

    public override sealed void ShowScreen()
    {
        ResetStatsView();

        Mainu.instance.mainButtons["Stats"].isCheck = true;
        Mainu.instance.mainButtons["Stats"].myButton.GetComponentInChildren<Image>().sprite = Mainu.instance.checkImages[1];

        base.ShowScreen();
    }

    public override sealed void HideScreen()
    {
        Mainu.instance.mainButtons["Stats"].isCheck = false;
        Mainu.instance.mainButtons["Stats"].myButton.GetComponentInChildren<Image>().sprite = Mainu.instance.checkImages[0];

        base.HideScreen();
    }

    public void OnCritPercent()
    {
        if (PlayerStats.instance.stats.critPercentLv * 100 * 1.2f <= PlayerStats.instance.stats.playerCoin)
        {
            if (PlayerStats.instance.stats.critPercentLv < 20)
            {
                PlayerStats.instance.stats.playerCoin -= (int)(PlayerStats.instance.stats.critPercentLv * 100 * 1.2f);
                PlayerStats.instance.stats.critPercentLv++;
                ResetStatsView();
            }
        }
    }

    public void OnCritDamage()
    {
        if (PlayerStats.instance.stats.critDamageLv * 100 * 1.2f <= PlayerStats.instance.stats.playerCoin)
        {
            PlayerStats.instance.stats.playerCoin -= (int)(PlayerStats.instance.stats.critDamageLv * 100 * 1.2f);
            PlayerStats.instance.stats.critDamageLv++;
            ResetStatsView();
        }
    }

    public void OnCoinPercent()
    {
        if (PlayerStats.instance.stats.coinPercentLv * 100 * 1.2f <= PlayerStats.instance.stats.playerCoin)
        {
            PlayerStats.instance.stats.playerCoin -= (int)(PlayerStats.instance.stats.coinPercentLv * 100 * 1.2f);
            PlayerStats.instance.stats.coinPercentLv++;
            ResetStatsView();
        }
    }

    void ResetStatsView()
    {
        int price = 0;

        crit1Lv.text = PlayerStats.instance.stats.critPercentLv.ToString();
        crit1NextLv.text = (PlayerStats.instance.stats.critPercentLv + 1).ToString();
        crit1Percent.text = (PlayerStats.instance.stats.critPercentLv * 5).ToString() + "%";
        price = (int)(PlayerStats.instance.stats.critPercentLv * 100 * 1.2f); Debug.Log(price);
        if (price < 1000)
            crit1Price.text = price.ToString();
        else if (price >= 1000)
            crit1Price.text = (price / 1000).ToString() + ".A";

        crit2Lv.text = PlayerStats.instance.stats.critDamageLv.ToString();
        crit2NextLv.text = (PlayerStats.instance.stats.critDamageLv + 1).ToString();
        crit2Damage.text = (PlayerStats.instance.stats.critDamageLv * 20f).ToString() + "%";
        price = (int)(PlayerStats.instance.stats.critDamageLv * 100 * 1.2f);
        if (price < 1000)
            crit2Price.text = price.ToString();
        else if (price >= 1000)
            crit2Price.text = (price / 1000).ToString() + ".A";

        coinLv.text = PlayerStats.instance.stats.coinPercentLv.ToString();
        coinNextLv.text = (PlayerStats.instance.stats.coinPercentLv + 1).ToString();
        coinPercent.text = ((int)(PlayerStats.instance.stats.coinPercentLv * 1.2f)).ToString() + "%";
        price = (int)(PlayerStats.instance.stats.coinPercentLv * 100 * 1.2f);
        if (price < 1000)
            coinPrice.text = price.ToString();
        else if (price >= 1000)
            coinPrice.text = (price / 1000).ToString() + ".A";
    }
}
