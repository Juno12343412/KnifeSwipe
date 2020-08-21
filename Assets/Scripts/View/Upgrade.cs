using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using KnifeSwipe;
using Good;
using Manager.Sound;
using UnityEditor;

public class Upgrade : BaseScreen<Upgrade>
{
<<<<<<< HEAD
    [HideInInspector] public Image knifeImg;
    [SerializeField]  private Text upgradeCoinPrice, upgradeSpecialPrice;
    [SerializeField]  private Text potentialPrice;
    [SerializeField]  private Text knifeLv;
    [SerializeField]  private Text knifeDmg;
    [SerializeField]  private Text critPercent;
    [SerializeField]  private Text critDmg;
    [SerializeField]  private Text attackCount;
    [SerializeField]  private Text bounce;
    [SerializeField]  private Text totalDmg;
=======
    [SerializeField] private Image  knifeImg;
    [SerializeField] private Text   upgradeCoinPrice, upgradeSpecialPrice;
    [SerializeField] private Text   potentialPrice;
    [SerializeField] private GameObject[] knifeUpgrade;
    [SerializeField] private Text   knifeLv;
    [SerializeField] private Text[] potentialText;
    [SerializeField] private GameObject potentialLock;
>>>>>>> KJY

    private void Start()
    {
        ResetUpgradeView();
    }

    public override sealed void ShowScreen()
    {
        ResetUpgradeView();
        base.ShowScreen();
    }

    public override sealed void HideScreen()
    {
        base.HideScreen();
    }

    public void OnUpgrade()
    {
        if (PlayerStats.instance.stats.knifeLv > 99)
            return;

        if (ETC.GetCoin(PlayerStats.instance.stats.knifeLv) <= PlayerStats.instance.stats.playerCoin &&
            ETC.GetCoin(PlayerStats.instance.stats.knifeLv / 2f) <= PlayerStats.instance.stats.playerSpecialCoin)
        {
            SoundManager.instance.PlaySound("UI 터치 효과음");

            PlayerStats.instance.stats.playerCoin -= (int)(ETC.GetCoin(PlayerStats.instance.stats.knifeLv));
            PlayerStats.instance.stats.playerSpecialCoin -= (int)(ETC.GetCoin(PlayerStats.instance.stats.knifeLv / 2f));
            PlayerStats.instance.stats.knifeLv++;
            PlayerStats.instance.stats.knifeDamage += Mathf.Abs(ETC.GetAttack(PlayerStats.instance.stats.knifeLv));
            //PlayerStats.instance.stats.knifeMaxBounce++;
            ResetUpgradeView();
        }
    }

    public void OnPotential()
    {
        if (1000 <= PlayerStats.instance.stats.playerSpecialCoin)
        {
            PlayerStats.instance.stats.playerSpecialCoin -= 1000;
            GetPotentials(0);
            GetPotentials(1);
            GetPotentials(2);
            ResetUpgradeView();   
        }
    }

    void GetPotentials(int n)
    {
        float potential = Random.Range(1f, 100f);
        float percent = n == 2 ? Random.Range(0, 3) : Random.Range(0, 2);

        switch (percent)
        {
            case 0:
                PlayerStats.instance.stats.potentials[n].potentialKind = "Damage";
                break;
            case 1:
                PlayerStats.instance.stats.potentials[n].potentialKind = "Coin";
                break;
            case 2:
                PlayerStats.instance.stats.potentials[n].potentialKind = "CritDamage";
                break;
            case 3:
                PlayerStats.instance.stats.potentials[n].potentialKind = "CritHit";
                break;
        }

        if (potential >= 0f && potential <= 50f)
        {
            switch (percent)
            {
                case 0:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 10f;
                    break;
                case 1:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 20f;
                    break;
                case 2:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 20f;
                    break;
                case 3:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 10f;
                    break;
            }
        }
        else if (potential > 50f && potential <= 80f)
        {
            switch (percent)
            {
                case 0:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 20f;
                    break;
                case 1:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 40f;
                    break;
                case 2:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 50f;
                    break;
                case 3:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 20f;
                    break;
            }
        }
        else if (potential > 80f && potential <= 90f)
        {
            switch (percent)
            {
                case 0:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 30f;
                    break;
                case 1:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 60f;
                    break;
                case 2:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 80f;
                    break;
                case 3:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 30f;
                    break;
            }
        }
        else if (potential > 90f && potential <= 95f)
        {
            switch (percent)
            {
                case 0:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 50f;
                    break;
                case 1:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 100f;
                    break;
                case 2:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 120f;
                    break;
                case 3:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 40f;
                    break;
            }
        }
        else if (potential > 95f && potential <= 99.77f)
        {
            switch (percent)
            {
                case 0:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 80f;
                    break;
                case 1:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 150f;
                    break;
                case 2:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 180f;
                    break;
                case 3:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 50f;
                    break;
            }
        }
        else if (potential > 99.77f && potential <= 100f)
        {
            switch (percent)
            {
                case 0:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 100f;
                    break;
                case 1:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 200f;
                    break;
                case 2:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 250f;
                    break;
                case 3:
                    PlayerStats.instance.stats.potentials[n].potentialPercent = 60f;
                    break;
            }
        } 
    }

    void AddPotential(int n)
    {
        switch (PlayerStats.instance.stats.potentials[n].potentialKind)
        {
            case "Damage":
                PlayerStats.instance.stats.moreDamage += PlayerStats.instance.stats.potentials[n].potentialPercent;
                break;
            case "Coin":
                PlayerStats.instance.stats.coinPercent += PlayerStats.instance.stats.potentials[n].potentialPercent;
                break;
            case "CritDamage":
                PlayerStats.instance.stats.critDamage += PlayerStats.instance.stats.potentials[n].potentialPercent;
                break;
            case "CritHit":
                PlayerStats.instance.stats.critPercent += PlayerStats.instance.stats.potentials[n].potentialPercent;
                break;
        }
    }

    void ResetUpgradeView()
    {
        int price = 0;

        price = (int)ETC.GetCoin(PlayerStats.instance.stats.knifeLv);
        upgradeCoinPrice = ETC.Calculation(upgradeCoinPrice, price);

        price = (int)ETC.GetCoin(PlayerStats.instance.stats.knifeLv / 2f);
        upgradeSpecialPrice = ETC.Calculation(upgradeSpecialPrice, price);

        // 9
        if (PlayerStats.instance.stats.knifeLv > 99)
        {
            knifeUpgrade[0].SetActive(false);
            knifeUpgrade[1].SetActive(false);
            knifeUpgrade[2].SetActive(true);
        }
        else if ((PlayerStats.instance.stats.knifeLv + 1) % 10 == 0)
        {
            Debug.Log("진급 가능");
            knifeUpgrade[0].SetActive(true);
            knifeUpgrade[1].SetActive(false);
            knifeUpgrade[2].SetActive(false);
        }
        else
        {
            knifeUpgrade[0].SetActive(false);
            knifeUpgrade[1].SetActive(true);
            knifeUpgrade[2].SetActive(false);
        }

        knifeLv.text = PlayerStats.instance.stats.knifeLv.ToString() + "+";

        if (PlayerStats.instance.stats.potentials[0].potentialKind != "")
        {
            PlayerStats.instance.stats.moreDamage = 0f;
            PlayerStats.instance.stats.coinPercent = PlayerStats.instance.stats.coinPercentLv;
            PlayerStats.instance.stats.critDamage = ETC.GetCritDmg(PlayerStats.instance.stats.critDamageLv);
            PlayerStats.instance.stats.critPercent = ETC.GetCritHit(PlayerStats.instance.stats.critPercentLv);
            potentialLock.SetActive(false);

            for (int i = 0; i < 3; i++)
            {
                switch (PlayerStats.instance.stats.potentials[i].potentialKind)
                {
                    case "Damage":
                        potentialText[i].text = "추가 대미지 : ";
                        break;
                    case "Coin":
                        potentialText[i].text = "추가 재화 획등량 : ";
                        break;
                    case "CritDamage":
                        potentialText[i].text = "추가 치명타 대미지 : ";
                        break;
                    case "CritHit":
                        potentialText[i].text = "추가 치명타 확률 : ";
                        break;
                    default:
                        break;
                }
                potentialText[i].text = potentialText[i].text + PlayerStats.instance.stats.potentials[i].potentialPercent + "%";
                AddPotential(i);
            }
        }
        Controller.instance.ChanageKnife();

        // 9 >= 
        if (PlayerStats.instance.stats.knifeLv / 10 >= PlayerStats.instance.knifeImgs.Length - 1)
            knifeImg.sprite = PlayerStats.instance.knifeImgs[PlayerStats.instance.knifeImgs.Length - 1];
        else
            knifeImg.sprite = PlayerStats.instance.knifeImgs[(PlayerStats.instance.stats.knifeLv / 10)];

<<<<<<< HEAD
        Controller.instance.ChanageKnife();
        knifeImg.sprite = PlayerStats.instance.knifeImgs[PlayerStats.instance.stats.knifeLv - 1];
=======
        GameLogic.instance.Save();
>>>>>>> KJY
    }
}
