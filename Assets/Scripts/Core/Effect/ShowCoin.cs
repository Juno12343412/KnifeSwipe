using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Good;

public class ShowCoin : MonoBehaviour
{
    [SerializeField] private Text MyCoin;
    [SerializeField] private Text MySpecialCoin;
    void Update()
    {
        if (PlayerStats.instance.stats.playerCoin >= 0)
        {
            MyCoin = ETC.Calculation(MyCoin, PlayerStats.instance.stats.playerCoin);
        }
        else if (PlayerStats.instance.stats.playerCoin < 0)
            MyCoin.text = "0";

        if (PlayerStats.instance.stats.playerSpecialCoin >= 0)
        {
            MySpecialCoin = ETC.Calculation(MySpecialCoin, PlayerStats.instance.stats.playerSpecialCoin);
        }
        else if (PlayerStats.instance.stats.playerSpecialCoin < 0)
            MySpecialCoin.text = "0";
    }
}
