using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public Sprite[] knifeImgs;

    [Serializable]
    public class Potential
    {
        public string potentialKind = "";
        public float potentialPercent = 0f; 
    }

    [Serializable]
    public class Player
    {
        public int          playerCoin = 0, maxCoin;
        public int          playerSpecialCoin = 0, maxSpecialCoin;
        public int          knifeLv = 1;
        public int          knifeBounce = 4, knifeMaxBounce = 4;
        public int          knifeAttackCount = 1;
        public double       knifeDamage = 100;
        public double       moreDamage = 0f;
        public int          critPercentLv = 1;
        public float        critPercent = 0f;
        public int          critDamageLv = 1;
        public float        critDamage = 0f;
        public int          coinPercentLv = 1;
        public float        coinPercent = 0f;
        public Potential[]  potentials = new Potential[3];

        public int curStage = 0;
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
