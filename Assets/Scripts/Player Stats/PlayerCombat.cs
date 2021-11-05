using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerCombat : MonoBehaviour
{

    private PlayerStats playerStats;
    public GetEnemy enemy;
    public Weapon weapon;

    public float hitboxActive = 0f;
    public float attackCD = 0f;
    private float attackSpeed;
    public int stamCost = 50;

    public float attackDelay = 0.7f; // change depending on animation


    public float knockBackStrength = 1f;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        attackSpeed = playerStats.attackSpeed.GetValue();
        
    }

    void Update()
    {
        attackCD -= Time.deltaTime;
        hitboxActive -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack(enemy.enemyStats);
        }

    }

    public void Attack(EnemyStats enemyStats)
    {
        if (attackCD <= 0f && playerStats.currentStamina > stamCost) {
            weapon.Attack();
            playerStats.UseStamina(stamCost);
            if (enemyStats != null)
            {
                StartCoroutine(DoDmg(enemyStats, attackDelay));
            }

           
            attackCD = 1f / attackSpeed; // change attack cooldown
        }

    }

    IEnumerator DoDmg(EnemyStats enemyStats, float delay)
    {
        yield return new WaitForSeconds(delay);
        enemyStats.TakeDmg(playerStats.damage.GetValue());
    }


    

}
