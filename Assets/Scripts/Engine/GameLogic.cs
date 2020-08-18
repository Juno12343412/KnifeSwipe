using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Pooling;

public class GameLogic : MonoBehaviour
{
    List<Dictionary<string, object>> stageData;
    void Start()
    {
        /* 불러오기 */
        DataController.Instance.LoadGameData();
        PlayerStats.instance.stats.playerCoin = DataController.Instance.gameData.playerCoin;
        PlayerStats.instance.stats.playerSpecialCoin = DataController.Instance.gameData.playerSpecialCoin;
        PlayerStats.instance.stats.knifeLv = DataController.Instance.gameData.knifeLv;
        PlayerStats.instance.stats.knifeBounce = DataController.Instance.gameData.knifeBounce;
        PlayerStats.instance.stats.knifeAttackCount = DataController.Instance.gameData.knifeAttackCount;
        PlayerStats.instance.stats.knifeDamage = DataController.Instance.gameData.knifeDamage;
        PlayerStats.instance.stats.critPercentLv = DataController.Instance.gameData.critPercentLv;
        PlayerStats.instance.stats.critDamageLv = DataController.Instance.gameData.critDamageLv;
        PlayerStats.instance.stats.coinPercentLv = DataController.Instance.gameData.coinPercentLv;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            Save();
    }

    void Save()
    {
        /* 저장 */
        DataController.Instance.gameData.playerCoin = PlayerStats.instance.stats.playerCoin;
        DataController.Instance.gameData.playerSpecialCoin = PlayerStats.instance.stats.playerSpecialCoin;
        DataController.Instance.gameData.knifeLv = PlayerStats.instance.stats.knifeLv;
        DataController.Instance.gameData.knifeBounce = PlayerStats.instance.stats.knifeBounce;
        DataController.Instance.gameData.knifeAttackCount = PlayerStats.instance.stats.knifeAttackCount;
        DataController.Instance.gameData.knifeDamage = PlayerStats.instance.stats.knifeDamage;
        DataController.Instance.gameData.critPercentLv = PlayerStats.instance.stats.critPercentLv;
        DataController.Instance.gameData.critDamageLv = PlayerStats.instance.stats.critDamageLv;
        DataController.Instance.gameData.coinPercentLv = PlayerStats.instance.stats.coinPercentLv;
        DataController.Instance.SaveGameData();
    }
}
