using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCoin : MonoBehaviour
{
    [SerializeField] private Text MyCoin;
    [SerializeField] private Text MySpecialCoin;
    void Update()
    {
        if (PlayerStats.instance.stats.playerCoin < 1000)
            MyCoin.text = PlayerStats.instance.stats.playerCoin.ToString();
        else if (PlayerStats.instance.stats.playerCoin > 1000)
            MyCoin.text = CalculationA(PlayerStats.instance.stats.playerCoin).ToString() + ".A";

        if (PlayerStats.instance.stats.playerSpecialCoin < 1000)
            MySpecialCoin.text = PlayerStats.instance.stats.playerSpecialCoin.ToString();
        else if (PlayerStats.instance.stats.playerSpecialCoin > 1000)
            MySpecialCoin.text = CalculationA(PlayerStats.instance.stats.playerSpecialCoin).ToString() + ".A";
    }

    int CalculationA(int num)
    {
        return num / 1000;
    }
}
