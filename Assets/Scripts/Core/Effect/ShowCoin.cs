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
        MyCoin = ETC.Calculation(MyCoin, PlayerStats.instance.stats.playerCoin);
        MySpecialCoin = ETC.Calculation(MySpecialCoin, PlayerStats.instance.stats.playerSpecialCoin);
    }
}
