using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            PlayerStats.instance.stats.playerSpecialCoin += 10;
            Destroy(gameObject);
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

            Ingame.instance.poolHitEffect.Spawn(transform.position);
        }
    }
}
