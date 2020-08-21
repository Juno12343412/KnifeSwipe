using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Good;

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

    private void Start()
    {
        ResetStatsView();
    }

    public override sealed void ShowScreen()
    {
        base.ShowScreen();
    }

    public override sealed void HideScreen()
    {
        base.HideScreen();
    }

    public void OnCritPercent()
    {
        if (ETC.GetCoin(PlayerStats.instance.stats.critPercentLv) <= PlayerStats.instance.stats.playerCoin && PlayerStats.instance.stats.critPercentLv < 50)
        {
            PlayerStats.instance.stats.playerCoin -= (int)ETC.GetCoin(PlayerStats.instance.stats.critPercentLv);
            PlayerStats.instance.stats.critPercentLv++;
            PlayerStats.instance.stats.critPercent += ETC.GetCritHit(PlayerStats.instance.stats.critPercentLv);
            ResetStatsView();
        }
    }

    public void OnCritDamage()
    {
        if (ETC.GetCoin(PlayerStats.instance.stats.critDamageLv) <= PlayerStats.instance.stats.playerCoin && PlayerStats.instance.stats.critDamageLv < 50)
        {
            PlayerStats.instance.stats.playerCoin -= (int)ETC.GetCoin(PlayerStats.instance.stats.critDamageLv);
            PlayerStats.instance.stats.critDamageLv++;
            PlayerStats.instance.stats.critDamage += ETC.GetCritHit(PlayerStats.instance.stats.critDamageLv);
            ResetStatsView();
        }
    }

    public void OnCoinPercent()
    {
        if (ETC.GetCoin(PlayerStats.instance.stats.coinPercentLv) <= PlayerStats.instance.stats.playerCoin && PlayerStats.instance.stats.coinPercentLv < 100)
        {
            PlayerStats.instance.stats.playerCoin -= (int)ETC.GetCoin(PlayerStats.instance.stats.coinPercentLv);
            PlayerStats.instance.stats.coinPercentLv++;
            PlayerStats.instance.stats.coinPercent += PlayerStats.instance.stats.coinPercentLv;
            ResetStatsView();
        }
    }

    void ResetStatsView()
    {
        int price = 0;

        if (PlayerStats.instance.stats.critPercentLv < 50)
        {
            crit1Lv.text = PlayerStats.instance.stats.critPercentLv.ToString();
            crit1NextLv.text = (PlayerStats.instance.stats.critPercentLv + 1).ToString();
            crit1Percent.text = ETC.GetCritHit(PlayerStats.instance.stats.critPercentLv).ToString() + "%";
            price = (int)ETC.GetCoin(PlayerStats.instance.stats.critPercentLv);
            crit1Price = ETC.Calculation(crit1Price, price);
        }
        else
        {
            crit1Lv.text = "MAX Level";
            crit1NextLv.text = "MAX";
            crit1Percent.text = ETC.GetCritHit(PlayerStats.instance.stats.critPercentLv).ToString() + "%";
            crit1Price.text = "NONE";
        }

        if (PlayerStats.instance.stats.critDamageLv < 50)
        {
            crit2Lv.text = PlayerStats.instance.stats.critDamageLv.ToString();
            crit2NextLv.text = (PlayerStats.instance.stats.critDamageLv + 1).ToString();
            crit2Damage.text = ETC.GetCritDmg(PlayerStats.instance.stats.critDamageLv).ToString() + "%";
            price = (int)ETC.GetCoin(PlayerStats.instance.stats.critDamageLv);
            crit2Price = ETC.Calculation(crit2Price, price);
        }
        else
        {
            crit2Lv.text = "MAX Level";
            crit2NextLv.text = "MAX";
            crit2Damage.text = ETC.GetCritHit(PlayerStats.instance.stats.critPercentLv).ToString() + "%";
            crit2Price.text = "NONE";
        }

        if (PlayerStats.instance.stats.coinPercentLv < 100)
        {
            coinLv.text = PlayerStats.instance.stats.coinPercentLv.ToString();
            coinNextLv.text = (PlayerStats.instance.stats.coinPercentLv + 1).ToString();
            coinPercent.text = PlayerStats.instance.stats.coinPercentLv.ToString() + "%";
            price = (int)ETC.GetCoin(PlayerStats.instance.stats.coinPercentLv);
            coinPrice = ETC.Calculation(coinPrice, price);
        }
        else
        {
            coinLv.text = "MAX Level";
            coinNextLv.text = "MAX";
            coinPercent.text = ETC.GetCritHit(PlayerStats.instance.stats.critPercentLv).ToString() + "%";
            coinPrice.text = "NONE";
        }

        GameLogic.instance.Save();
    }
}
