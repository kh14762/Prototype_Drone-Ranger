using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerCombat : MonoBehaviour
{

    private PlayerStats playerStats;
    public GetEnemy enemy;
    public Weapon weapon;

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

        // attack on left click
        if (Input.GetKeyDown(KeyCode.Mouse0) && weapon.isEquipped)
        {
            Attack(enemy.enemyStats);
            
            
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (weapon.isEquipped)
            {
                weapon.Unequip();

               // remove dmg modifier
               playerStats.AddStatBonus(-weapon.dmgModifier);
               
            }
            else
            {
                weapon.Equip();
                // add dmg modifier
                playerStats.AddStatBonus(weapon.dmgModifier);
            }
        }

        // temporary health and stam reset for testing
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerStats.SetHealth(playerStats.maxHealth);
            playerStats.SetStamina(playerStats.maxStamina);
        }

    }

    public void Attack(EnemyStats enemyStats)
    {
        if (attackCD <= 0f && playerStats.currentStamina > stamCost) {
            weapon.Attack(); // play attack animation
            playerStats.UseStamina(stamCost); // use stamina on each attack

            // check if there is an enemy
            if (enemyStats != null)
            {
                StartCoroutine(DoDmg(enemyStats, attackDelay));
            }
            /*foreach (EnemyStats enemy in enemyStats)
            {
                if(enemy != null)
                {
                    StartCoroutine(DoDmg(enemy, attackDelay));
                }
                
            }*/

            attackCD = 1f / attackSpeed; // change attack cooldown
        }

    }

    IEnumerator DoDmg(EnemyStats enemyStats, float delay)
    {
        // delay before enemy takes dmg (for animation purposes)
        yield return new WaitForSeconds(delay);
        enemyStats.TakeDmg(playerStats.damage.GetValue());
    }


}
