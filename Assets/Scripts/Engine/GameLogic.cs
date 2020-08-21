using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Pooling;
using Good;

public class GameLogic : MonoBehaviour
{
<<<<<<< HEAD
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
=======
    public static GameLogic instance;

    void Awake()
    {
        instance = GetComponent<GameLogic>();
        Load();
>>>>>>> KJY
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            Save();
    }

<<<<<<< HEAD
    void Save()
    {
        /* 저장 */
=======
    public void Save()
    {
        /* 저장 */
        Debug.Log("Save");
>>>>>>> KJY
        DataController.Instance.gameData.playerCoin = PlayerStats.instance.stats.playerCoin;
        DataController.Instance.gameData.playerSpecialCoin = PlayerStats.instance.stats.playerSpecialCoin;
        DataController.Instance.gameData.knifeLv = PlayerStats.instance.stats.knifeLv;
        DataController.Instance.gameData.knifeBounce = PlayerStats.instance.stats.knifeBounce;
        DataController.Instance.gameData.knifeAttackCount = PlayerStats.instance.stats.knifeAttackCount;
        DataController.Instance.gameData.knifeDamage = PlayerStats.instance.stats.knifeDamage;
<<<<<<< HEAD
        DataController.Instance.gameData.critPercentLv = PlayerStats.instance.stats.critPercentLv;
        DataController.Instance.gameData.critDamageLv = PlayerStats.instance.stats.critDamageLv;
        DataController.Instance.gameData.coinPercentLv = PlayerStats.instance.stats.coinPercentLv;
        DataController.Instance.SaveGameData();
    }
=======
        DataController.Instance.gameData.moreDamage = PlayerStats.instance.stats.moreDamage;
        DataController.Instance.gameData.critPercentLv = PlayerStats.instance.stats.critPercentLv;
        DataController.Instance.gameData.critPercent = PlayerStats.instance.stats.critPercent;
        DataController.Instance.gameData.critDamageLv = PlayerStats.instance.stats.critDamageLv;
        DataController.Instance.gameData.critDamage = PlayerStats.instance.stats.critDamage;
        DataController.Instance.gameData.coinPercentLv = PlayerStats.instance.stats.coinPercentLv;
        DataController.Instance.gameData.coinPercent = PlayerStats.instance.stats.coinPercent;
        DataController.Instance.gameData.curStage = PlayerStats.instance.stats.curStage;
        if (PlayerStats.instance.stats.potentials != null)
        {
            DataController.Instance.gameData.potentials = PlayerStats.instance.stats.potentials;
        }
        DataController.Instance.SaveGameData();
    }

    public void Load()
    {
        /* 불러오기 */
        Debug.Log("Load");
        DataController.Instance.LoadGameData();

        PlayerStats.instance.stats.playerCoin = DataController.Instance.gameData.playerCoin;
        PlayerStats.instance.stats.playerSpecialCoin = DataController.Instance.gameData.playerSpecialCoin;
        PlayerStats.instance.stats.knifeLv = DataController.Instance.gameData.knifeLv;
        PlayerStats.instance.stats.knifeBounce = DataController.Instance.gameData.knifeBounce;
        PlayerStats.instance.stats.knifeAttackCount = DataController.Instance.gameData.knifeAttackCount;
        PlayerStats.instance.stats.knifeDamage = DataController.Instance.gameData.knifeDamage;
        PlayerStats.instance.stats.moreDamage = DataController.Instance.gameData.moreDamage;
        PlayerStats.instance.stats.critPercentLv = DataController.Instance.gameData.critPercentLv;
        PlayerStats.instance.stats.critPercent = DataController.Instance.gameData.critPercent;
        PlayerStats.instance.stats.critDamageLv = DataController.Instance.gameData.critDamageLv;
        PlayerStats.instance.stats.critDamage = DataController.Instance.gameData.critDamage;
        PlayerStats.instance.stats.coinPercentLv = DataController.Instance.gameData.coinPercentLv;
        PlayerStats.instance.stats.coinPercent = DataController.Instance.gameData.coinPercent;
        PlayerStats.instance.stats.curStage = DataController.Instance.gameData.curStage;
        if (DataController.Instance.gameData.potentials != null)
        {
            PlayerStats.instance.stats.potentials = DataController.Instance.gameData.potentials;
        }
    }
>>>>>>> KJY
}
