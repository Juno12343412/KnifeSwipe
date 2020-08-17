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
        ResetStatsView();
        base.ShowScreen();
    }

    public override sealed void HideScreen()
    {
        base.HideScreen();
    }

    public void OnCritPercent()
    {
        if (ETC.GetCoin(PlayerStats.instance.stats.critPercentLv) <= PlayerStats.instance.stats.playerCoin)
        {
            PlayerStats.instance.stats.playerCoin -= (int)ETC.GetCoin(PlayerStats.instance.stats.critPercentLv);
            PlayerStats.instance.stats.critPercentLv++;
            ResetStatsView();
        }
    }

    public void OnCritDamage()
    {
        if (ETC.GetCoin(PlayerStats.instance.stats.critDamageLv) <= PlayerStats.instance.stats.playerCoin)
        {
            PlayerStats.instance.stats.playerCoin -= (int)ETC.GetCoin(PlayerStats.instance.stats.critDamageLv);
            PlayerStats.instance.stats.critDamageLv++;
            ResetStatsView();
        }
    }

    public void OnCoinPercent()
    {
        if (ETC.GetCoin(PlayerStats.instance.stats.coinPercentLv) <= PlayerStats.instance.stats.playerCoin)
        {
            PlayerStats.instance.stats.playerCoin -= (int)ETC.GetCoin(PlayerStats.instance.stats.coinPercentLv);
            PlayerStats.instance.stats.coinPercentLv++;
            ResetStatsView();
        }
    }

    void ResetStatsView()
    {
        int price = 0;

        crit1Lv.text = PlayerStats.instance.stats.critPercentLv.ToString();
        crit1NextLv.text = (PlayerStats.instance.stats.critPercentLv + 1).ToString();
        crit1Percent.text = ETC.GetCritHit(PlayerStats.instance.stats.critPercentLv).ToString() + "%";
        price = (int)ETC.GetCoin(PlayerStats.instance.stats.critPercentLv); 
        crit1Price = ETC.Calculation(crit1Price, price);

        crit2Lv.text = PlayerStats.instance.stats.critDamageLv.ToString();
        crit2NextLv.text = (PlayerStats.instance.stats.critDamageLv + 1).ToString();
        crit2Damage.text = ETC.GetCritDmg(PlayerStats.instance.stats.critDamageLv).ToString() + "%";
        price = (int)ETC.GetCoin(PlayerStats.instance.stats.critDamageLv);
        crit2Price = ETC.Calculation(crit2Price, price);

        coinLv.text = PlayerStats.instance.stats.coinPercentLv.ToString();
        coinNextLv.text = (PlayerStats.instance.stats.coinPercentLv + 1).ToString();
        coinPercent.text = PlayerStats.instance.stats.coinPercentLv.ToString() + "%";
        price = (int)ETC.GetCoin(PlayerStats.instance.stats.coinPercentLv);
        coinPrice = ETC.Calculation(coinPrice, price);
    }
}
