using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

      [Serializable]
    public class Player
    {
        public int    playerCoin, maxCoin;
        public int    playerSpecialCoin, maxSpecialCoin;
        public int    knifeLv;
        public int    knifeBounce, knifeMaxBounce;
        public int    knifeAttackCount;
        public float  knifeDamage;
        public int    critPercentLv;
        public int    critDamageLv;
        public int    coinPercentLv;
    } [Header("Player Stats")] public Player stats;

    void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        instance = GetComponent<PlayerStats>();    
    }
}
