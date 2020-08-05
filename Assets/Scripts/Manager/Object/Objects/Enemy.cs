using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;

public class Enemy : PoolingObject
{
    public override string objectName => "Enemy";
    public float enemyHP, maxHP = 100;

    public Sprite[] sprites;

    SpriteRenderer spriteRdr;

    public override void Init()
    {
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
            PlayerStats.instance.stats.playerCoin += 500 * (int)(PlayerStats.instance.stats.coinPercentLv * 1.2f);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Knife")
        {
            float damage = 0f;
            if (Random.Range(1, 100) >= PlayerStats.instance.stats.critPercentLv * 5f || PlayerStats.instance.stats.critPercentLv >= 100f)
                damage = PlayerStats.instance.stats.knifeDamage * (PlayerStats.instance.stats.critDamageLv * 20f);
            else
                damage = PlayerStats.instance.stats.knifeDamage * PlayerStats.instance.stats.critDamageLv / Random.Range(1, 5f);
            enemyHP -= damage;

            DamageEffect obj = Ingame.instance.poolDamageEffect.Spawn(transform.position);
            obj.GetComponent<DamageEffect>().getDamage = (int)damage;
        }
    }
}
