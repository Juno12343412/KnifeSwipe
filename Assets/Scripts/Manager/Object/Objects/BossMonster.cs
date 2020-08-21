using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.Sound;
using Good;

public class BossMonster : MonoBehaviour
{
    public static BossMonster instance;
    public float enemyHP, maxHP;

    public GameObject explosionEffect = null;
    Animator myAnims = null;
    SpriteRenderer spriteRdr = null;

    bool isFire = false;

    void Awake()
    {
        instance = GetComponent<BossMonster>();
        myAnims = GetComponent<Animator>();
        spriteRdr = GetComponent<SpriteRenderer>();
        myAnims.SetInteger("Boss", PlayerStats.instance.stats.curStage);
    }

    void Start()
    {
        switch (PlayerStats.instance.stats.curStage)
        {
            case 0:
                enemyHP = 10000;
                break;
            case 1:
                enemyHP = 200000;
                break;
            case 2:
                enemyHP = 1000000;
                break;
            case 3:
                enemyHP = 500000000;
                break;
            default:
                break;
        }
    }

    void Update()
    {
        if (enemyHP <= 0)
        {
            Boss.instance.isBoss = false;
            if (PlayerStats.instance.stats.curStage != 3)
                PlayerStats.instance.stats.curStage++;
            PlayerStats.instance.stats.playerCoin += Boss.instance.bossResult;
            PlayerStats.instance.stats.playerSpecialCoin += Boss.instance.bossResultS;
            Destroy(gameObject);
        }
        else if (!Boss.instance.isBoss)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Knife")
        {
            SoundManager.instance.PlaySound("몬스터 타격음");
            
            double damage = 0f;
            DamageEffect obj = Ingame.instance.poolDamageEffect.Spawn(transform.position);
            // 검 능력
            if (PlayerStats.instance.stats.knifeLv / 10 == 1 && Random.Range(1, 100) <= 30f)
            {
                Debug.Log("검 능력 발동");
                obj.GetComponent<DamageEffect>().isCrit = true;
                damage = (PlayerStats.instance.stats.knifeDamage + (PlayerStats.instance.stats.knifeDamage * 2f) * PlayerStats.instance.stats.moreDamage / PlayerStats.instance.stats.knifeDamage) + (PlayerStats.instance.stats.knifeDamage * PlayerStats.instance.stats.critDamageLv / PlayerStats.instance.stats.knifeDamage);
            }
            // 검 능력
            // 암살자의 검 능력
            else if (PlayerStats.instance.stats.knifeLv / 10 == 2 && Random.Range(1, 100) <= 30f)
            {
                Debug.Log("암살자 능력 발동");
                obj.GetComponent<DamageEffect>().isCrit = false;
                damage = (PlayerStats.instance.stats.knifeDamage + (PlayerStats.instance.stats.knifeDamage * PlayerStats.instance.stats.moreDamage / PlayerStats.instance.stats.knifeDamage)) * 2;
                Ingame.instance.poolHitEffect.Spawn(transform.position, new Quaternion(0, 0, 0.25f, 0.25f));
            }
            // 암살자의 검 능력
            // 기사의 검
            else if (PlayerStats.instance.stats.knifeLv / 10 == 3 && Random.Range(1, 100) <= 50f)
            {
                Debug.Log("기사 능력 발동");
                obj.GetComponent<DamageEffect>().isCrit = true;
                damage = (PlayerStats.instance.stats.knifeDamage + (PlayerStats.instance.stats.knifeDamage * 2f) * PlayerStats.instance.stats.moreDamage / PlayerStats.instance.stats.knifeDamage) + (PlayerStats.instance.stats.knifeDamage * PlayerStats.instance.stats.critDamageLv / PlayerStats.instance.stats.knifeDamage);
                Ingame.instance.poolHitEffect.Spawn(transform.position, new Quaternion(0, 0, 0.25f, 0.25f));
                Ingame.instance.poolHitEffect.Spawn(transform.position, new Quaternion(0, 0, 0.25f, 0.25f));
            }
            // 기사의 검
            // 블래스트 능력
            else if (PlayerStats.instance.stats.knifeLv / 10 == 5 && Random.Range(1, 100) <= 10f)
            {
                // 폭발 이펙트
                obj.GetComponent<DamageEffect>().isCrit = true;
                damage = (PlayerStats.instance.stats.knifeDamage + (PlayerStats.instance.stats.knifeDamage * PlayerStats.instance.stats.moreDamage / PlayerStats.instance.stats.knifeDamage)) * 3;
                Instantiate(explosionEffect, transform.position, transform.rotation);
            }
            else if (Random.Range(1, 100) <= PlayerStats.instance.stats.critPercent)
            {
                print("Crit");
                obj.GetComponent<DamageEffect>().isCrit = true;
                damage = (PlayerStats.instance.stats.knifeDamage + (PlayerStats.instance.stats.knifeDamage * PlayerStats.instance.stats.moreDamage / PlayerStats.instance.stats.knifeDamage) * 2) + (PlayerStats.instance.stats.knifeDamage * ETC.GetCritDmg(PlayerStats.instance.stats.critDamageLv) / PlayerStats.instance.stats.knifeDamage);
            }
            else
            {
                obj.GetComponent<DamageEffect>().isCrit = false;
                damage = PlayerStats.instance.stats.knifeDamage + (PlayerStats.instance.stats.knifeDamage * PlayerStats.instance.stats.moreDamage / PlayerStats.instance.stats.knifeDamage);
            }

            // 화염장미 검 능력
            if (PlayerStats.instance.stats.knifeLv / 10 == 4)
            {
                if (!isFire)
                    StartCoroutine(Fire());
            }
            // 화염장미 검 능력
            // 메카기어드 능력
            else if (PlayerStats.instance.stats.knifeLv / 10 == 7 && Random.Range(1, 100) <= 30f)
            {
                int n = Random.Range(1, 3);
                for (int i = 0; i <= n; i++)
                {
                    Ingame.instance.poolMekaGearEffect.Spawn(transform.position);
                }
            }
            // 메카기어드 능력
            // 인도자 능력
            else if (PlayerStats.instance.stats.knifeLv / 10 == 8)
            {
                Ingame.instance.poolSwordOraEffect.Spawn(transform.position);
            }
            // 인도자 능력

            obj.GetComponent<DamageEffect>().getDamage = (float)damage;
            enemyHP -= (float)damage;

            Ingame.instance.poolHitEffect.Spawn(transform.position);
        }

        // 블래스트 능력
        if (other.transform.tag == "Explosion")
        {
            if (!isFire)
                StartCoroutine(Fire());
        }
        // 블래스트 능력
        // 은하계 능력
        if (other.transform.tag == "BlackHole")
        {
            enemyHP = 0f;
            Ingame.instance.poolHitEffect.Spawn(transform.position);
        }
        // 은하계 능력
        // 메카기어드 능력
        if (other.transform.tag == "MekaGear")
        {
            DamageEffect obj = Ingame.instance.poolDamageEffect.Spawn(transform.position);
            obj.GetComponent<DamageEffect>().isCrit = true;
            // 폭발 이펙트
            double damage = (PlayerStats.instance.stats.knifeDamage + (PlayerStats.instance.stats.knifeDamage * PlayerStats.instance.stats.moreDamage / PlayerStats.instance.stats.knifeDamage)) * 3;
            Instantiate(explosionEffect, transform.position, transform.rotation);

            obj.GetComponent<DamageEffect>().getDamage = (float)damage;
            enemyHP -= (float)damage;

            Ingame.instance.poolHitEffect.Spawn(transform.position);
        }
        // 메카기어드 능력
        // 인도자 능력
        if (other.transform.tag == "SwordOra")
        {
            DamageEffect obj = Ingame.instance.poolDamageEffect.Spawn(transform.position);
            obj.GetComponent<DamageEffect>().isCrit = true;
            double damage = (PlayerStats.instance.stats.knifeDamage + PlayerStats.instance.stats.knifeDamage * PlayerStats.instance.stats.moreDamage / PlayerStats.instance.stats.knifeDamage) + (PlayerStats.instance.stats.knifeDamage * PlayerStats.instance.stats.critDamageLv / PlayerStats.instance.stats.knifeDamage);
            obj.GetComponent<DamageEffect>().getDamage = (float)damage;
            enemyHP -= (float)damage;

            Ingame.instance.poolHitEffect.Spawn(transform.position);
        }
        // 인도자 능력
    }

    IEnumerator Fire()
    {
        isFire = true;
        float t = 0f;
        double damage = 0f;
        spriteRdr.color = new Color(255f / 255f, 168 / 255f, 168 / 255f, 1f);

        while (t <= 2.5f)
        {
            t += 0.25f;
            DamageEffect obj = Ingame.instance.poolDamageEffect.Spawn(transform.position);
            damage = (PlayerStats.instance.stats.knifeDamage + (PlayerStats.instance.stats.knifeDamage * PlayerStats.instance.stats.moreDamage / PlayerStats.instance.stats.knifeDamage)) / 5f;
            obj.GetComponent<DamageEffect>().getDamage = (float)damage;
            enemyHP -= (float)damage;
            Debug.Log("화염장미 능력 발동 : " + damage);
            yield return new WaitForSeconds(0.25f);
        }
        spriteRdr.color = new Color(1f, 1f, 1f, 1f);
        isFire = false;
    }
}
