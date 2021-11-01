using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private EnemyStats enemyStats;
    public Weapon weapon;

    private float attackCD = 0f;
    private float attackDelay = 0.5f; // change depending on animation


    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
    }

    void Update()
    {
        attackCD -= Time.deltaTime;

    }

    public void Attack(PlayerStats playerStats)
    {
        if (attackCD <= 0f)
        {
            weapon.Attack();
            if (playerStats != null)
            {
                StartCoroutine(DoDmg(playerStats, attackDelay));
                
            }
            attackCD = 3; // change attack cooldown
        }

    }

    IEnumerator DoDmg(PlayerStats playerStats, float delay)
    {
        yield return new WaitForSeconds(delay);
        playerStats.TakeDmg(enemyStats.damage.GetValue());
    }


   
}
