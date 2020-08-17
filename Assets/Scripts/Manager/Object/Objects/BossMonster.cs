using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Good;

public class BossMonster : MonoBehaviour
{
    public static BossMonster instance;
    public float enemyHP, maxHP;

    private void Awake()
    {
        instance = GetComponent<BossMonster>();
    }

    void Update()
    {
        if (enemyHP <= 0 || !Boss.instance.isBoss)
        {
            Boss.instance.isBoss = false;
            PlayerStats.instance.stats.playerSpecialCoin += 1000;
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Knife")
        {
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
}
