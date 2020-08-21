using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pooling;
using Good;
using Manager.Sound;

public class Enemy : PoolingObject
{
    public override string objectName => "Enemy";
    public float enemyHP, maxHP = 100;

    public GameObject explosionEffect;
    public GameObject blackHoleEffect;
    public Sprite[] sprites;

    Animator anim = null;
    SpriteRenderer spriteRdr = null;

    public bool isFire = false;

    public override void Init()
    {
        spriteRdr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        spriteRdr.color = new Color(1f, 1f, 1f, 1f);
        isFire = false;
        transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(0, 5), 0f);

        switch (PlayerStats.instance.stats.curStage)
        {
            case 0:
                enemyHP = maxHP = 500f;
                spriteRdr.sprite = sprites[Random.Range(0, 3)];
                break;
            case 1:
                enemyHP = maxHP = 5000f;
                spriteRdr.sprite = sprites[Random.Range(3, 6)];
                break;
            case 2:
                enemyHP = maxHP = 500000f;
                spriteRdr.sprite = sprites[Random.Range(6, 9)];
                break;
            case 3:
                enemyHP = maxHP = 5000000f;
                spriteRdr.sprite = sprites[Random.Range(9, 12)];
                break;
            default:
                break;
        }
        base.Init();
    }

    public override void Release()
    {
        base.Release();
    }

    void Update()
    {
        if (enemyHP <= 0)
        {
            Release();

            switch (PlayerStats.instance.stats.curStage)
            {
                case 0:
                    PlayerStats.instance.stats.playerCoin += 50 + (int)(50f * PlayerStats.instance.stats.coinPercent / 50f);
                    PlayerStats.instance.stats.playerSpecialCoin += 25 + (int)(25f * PlayerStats.instance.stats.coinPercent / 25f);
                    break;
                case 1:
                    PlayerStats.instance.stats.playerCoin += 200 + (int)(200f * PlayerStats.instance.stats.coinPercent / 200f);
                    PlayerStats.instance.stats.playerSpecialCoin += 100 + (int)(100f * PlayerStats.instance.stats.coinPercent / 100f);
                    break;
                case 2:
                    PlayerStats.instance.stats.playerCoin += 500 + (int)(500f * PlayerStats.instance.stats.coinPercent / 500f);
                    PlayerStats.instance.stats.playerSpecialCoin += 250 + (int)(250f * PlayerStats.instance.stats.coinPercent / 250f);
                    break;
                case 3:
                    PlayerStats.instance.stats.playerCoin += 1000 + (int)(1000f * PlayerStats.instance.stats.coinPercent / 1000f);
                    PlayerStats.instance.stats.playerSpecialCoin += 500 + (int)(500f * PlayerStats.instance.stats.coinPercent / 500f);
                    break;
                default:
                    break;
            }
        }
        else if (!SwipeManager.instance.mainButtons[ViewState.Ingame].isCheck)
        {
            Release();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        SoundManager.instance.PlaySound("몬스터 타격음");

        if (other.transform.tag == "Knife")
        {
            anim.SetBool("isHit", true);

            double damage;
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
            // 블래스트 능력
            // 은하계 능력
            else if (PlayerStats.instance.stats.knifeLv / 10 == 6 && Random.Range(1, 100) <= 10f)
            {
                obj.GetComponent<DamageEffect>().isCrit = false;
                damage = (PlayerStats.instance.stats.knifeDamage + (PlayerStats.instance.stats.knifeDamage * PlayerStats.instance.stats.moreDamage / PlayerStats.instance.stats.knifeDamage));
                Instantiate(blackHoleEffect, transform.position, transform.rotation);
            }
            // 은하계 능력
            else if (Random.Range(1, 100) <= PlayerStats.instance.stats.critPercent)
            {
                obj.GetComponent<DamageEffect>().isCrit = true;
                damage = (PlayerStats.instance.stats.knifeDamage + PlayerStats.instance.stats.knifeDamage * PlayerStats.instance.stats.moreDamage / PlayerStats.instance.stats.knifeDamage) + (PlayerStats.instance.stats.knifeDamage * PlayerStats.instance.stats.critDamageLv / PlayerStats.instance.stats.knifeDamage);
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

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "Knife")
        {
            anim.SetBool("isHit", false);
        }
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
