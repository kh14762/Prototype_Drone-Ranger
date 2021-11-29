using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{

    public GameManager gameManager;
    public HealthBar healthBar;
    public StaminaBar staminaBar;
    public SojournerController sojournerController;
    private Coroutine stamRegen;
    private Coroutine healthRegen;
    [SerializeField] private Animator animator;
    [SerializeField] TextMeshProUGUI stats;
    public bool isDead;

    public int maxHealth = 100;
    public int currentHealth { get; private set; } // get value from anywhere but only set it in this class

    public int maxStamina = 1000;
    public int currentStamina { get; private set; }

    public float stamUseCD = 0f;

    public Stat damage, armor, attackSpeed;

    // cool down before starting health and stam regen
    private int healthRegenCD = 20;
    private int stamRegenCD = 5;


    public void Awake()
    {
        animator.SetBool("isAlive", true);
        // set max health of sojourner
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        // set max stamina of sojourner
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);

        isDead = false;
        
        


    }

    private void Update()
    {
        // if the player is running use stamina
        if(sojournerController.sojournerSpeed == sojournerController.sojournerSprintSpeed)
        {
            StartCoroutine(StamUse());
        }

        stamUseCD -= Time.deltaTime;

        stats.SetText("Damage: " + damage.GetValue() + "\nArmor: " + armor.GetValue());
        
    }

    public void TakeDmg(int damage)
    {
        damage -= armor.GetValue();
        // negate any damage if armor value is greater than damage value
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        // update player health
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        // Regenerate health, stop when taking dmg
        if (healthRegen != null)
        {
            StopCoroutine(healthRegen);
        }

        // start health regen
        healthRegen = StartCoroutine(RegenerateHealth());

        // player dead if health is zero or less
        if (currentHealth <= 0)
        {
            Die();
            isDead = true;
        }
    }

    public void UseStamina(int stamina)
    {
        // check if the player has enough stamina to use
        if(currentStamina - stamina >= 0)
        {

            //currentStamina -= stamina;
            //staminaBar.SetStamina(currentStamina--);

            StartCoroutine(SetStam(stamina));

            // if you are regenerate and use stam, stop regenerating
            if (stamRegen != null)
            {
                StopCoroutine(stamRegen);
            }

            stamRegen = StartCoroutine(RegenerateStam());
        } 
    }

    // use stamina "animation"
    private IEnumerator SetStam(int stamina)
    {
        int tempStamina = currentStamina - stamina;
        while (currentStamina > tempStamina)
        {
            staminaBar.SetStamina(currentStamina--);
            yield return new WaitForSeconds(0.001f);
        }
        
    }

    // regenerate stamina after an amount of time
    private IEnumerator RegenerateStam()
    {
        // cool down before starting stam regen
        yield return new WaitForSeconds(stamRegenCD);

        // regen as long as current stam isnt max stam
        while(currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100; // fills up a set rate regardless of max stam
            staminaBar.SetStamina(currentStamina);
            yield return new WaitForSeconds(0.01f); // for bar regen "animation"
        }

        stamRegen = null;
    }

    // cd on using stam
    private IEnumerator StamUse()
    {
        UseStamina(1);
        yield return new WaitForSeconds(1);
        
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
        gameManager.GameOver();
        /*StopCoroutine(healthRegen); // stop health regen
        StopCoroutine(stamRegen); // stop stamina regen
        StopCoroutine(SetStam(0));*/
        StopAllCoroutines();


        // death animation
        animator.SetTrigger("dead");
        animator.SetBool("isAlive", false);

    }

    public void SetHealth(int health)
    {
        currentHealth = health;
        healthBar.SetHealth(currentHealth);
    }

    public void SetStamina(int stamina)
    {
        currentStamina = stamina;
        staminaBar.SetStamina(currentStamina);
    }

    // add stats based on weapon
    public void AddStatBonus(int dmg)
    {
        damage.SetValue(dmg);
    }
}
