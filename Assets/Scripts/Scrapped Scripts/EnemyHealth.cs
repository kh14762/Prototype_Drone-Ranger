using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public EnemyHealthBar healthBar;

    public int maxHealth = 100;
    public int currentHealth;
    public bool canDmg = true;
    public int dmgCooldown = 3; // damage cooldown
    public int dmg = 20; // damage to apply to enemy


    // Start is called before the first frame update
    void Start()
    {
        // set max health of enemy
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            Debug.Log("hit");
            
            TakeDmg(dmg); // sojourner takes 20 damage



        }
    }

    void TakeDmg(int dmg)
    {
        if (canDmg)
        {
            currentHealth -= dmg;
            healthBar.SetHealth(currentHealth);
        }

    }

    // damage cooldown
    IEnumerator DamageCooldownRoutine()
    {
        canDmg = false;
        yield return new WaitForSeconds(dmgCooldown);
        canDmg = true;
    }
}
