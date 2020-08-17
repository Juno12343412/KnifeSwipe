using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;
using Good;

public class Enemy : PoolingObject
{
    public override string objectName => "Enemy";
    public float enemyHP, maxHP = 100;

    public Sprite[] sprites;

    Animator anim;
    SpriteRenderer spriteRdr;

    public override void Init()
    {
        anim = GetComponent<Animator>();
        spriteRdr = GetComponent<SpriteRenderer>();
        spriteRdr.sprite = sprites[Random.Range(0, 3)];
        transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(0, 5), 0f);
        enemyHP = maxHP;
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
            PlayerStats.instance.stats.playerCoin += 10 + (int)(10f * PlayerStats.instance.stats.coinPercentLv / 10f);
        }
        else if (!SwipeManager.instance.mainButtons[ViewState.Ingame].isCheck)
        {
            Release();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Knife")
        {
            anim.SetBool("isHit", true);

            float damage = 0f;
            DamageEffect obj = Ingame.instance.poolDamageEffect.Spawn(transform.position);

            if (Random.Range(1, 100) <= ETC.GetCritHit(PlayerStats.instance.stats.critPercentLv))
            {
                print("Crit");
                obj.GetComponent<DamageEffect>().isCrit = true;
                damage = (100f + PlayerStats.instance.stats.knifeDamage) * (ETC.GetCritDmg(PlayerStats.instance.stats.critDamageLv / 100f) * 10f / 100f);
            }
            else
            {
                obj.GetComponent<DamageEffect>().isCrit = false;
                damage = (100f + PlayerStats.instance.stats.knifeDamage) * 10f / 100f;
            }
            obj.GetComponent<DamageEffect>().getDamage = (int)damage;
            enemyHP -= damage;

            Ingame.instance.poolHitEffect.Spawn(transform.position);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "Knife")
        {
            anim.SetBool("isHit", false);
        }
        }
    }
