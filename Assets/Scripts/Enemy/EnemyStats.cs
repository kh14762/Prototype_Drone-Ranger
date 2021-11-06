using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public GameManager gameManager;
    public EnemyHealthBar healthBar;
    private Coroutine healthRegen;

    public int maxHealth = 100;
    public int currentHealth { get; private set; } // get value from anywhere but only set it in this class

    public Stat damage, armor;

    private int healthRegenCD = 10;
 

    void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDmg(int damage)
    {
        damage -= armor.GetValue();
        // negate any damage if armor value is greater than damage value
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        // update player health
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

       /* // Regenerate health, stop when taking dmg
        if (healthRegen != null)
        {
            StopCoroutine(healthRegen);
        }

        // start health regen
        healthRegen = StartCoroutine(RegenerateHealth());*/

        // player dead if health is zero or less
        if (currentHealth <= 0)
        {
            Die();
            //StopCoroutine(healthRegen); // stop health regen
        }
    }


    // regenerate health after an amount of time
    private IEnumerator RegenerateHealth()
    {
        // cool down before starting health regen
        yield return new WaitForSeconds(healthRegenCD);

        // regen as long as current health isnt max stam
        while (currentHealth < maxHealth)
        {
            currentHealth += 1;
            healthBar.SetHealth(currentHealth);
            yield return new WaitForSeconds(0.01f); // for bar regen "animation"
        }

        healthRegen = null;
    }


    public void Die()
    {
        // add animation

        Destroy(gameObject);

        // add loot
    }
}
