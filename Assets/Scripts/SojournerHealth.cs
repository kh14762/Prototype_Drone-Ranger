using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SojournerHealth : MonoBehaviour
{

    private Rigidbody sojournerRigidBody;

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    private SojournerController sojournerController;

    public int dmgCooldown = 3; // damage cooldown
    public int dmg = 20; // damage to apply to sojourner
    public bool canDmg = true;
    public bool tickDown = true;

    public float knockBackStrength = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        sojournerRigidBody = GetComponent<Rigidbody>();
        sojournerController = GetComponent<SojournerController>();

        // set max health of sojourner
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // damage function
    void TakeDmg(int dmg, Rigidbody enemyRB)
    {
        if (canDmg)
        {
            currentHealth -= dmg;
            healthBar.SetHealth(currentHealth);
            


            // knockback when taking damage
            Vector3 awayFromEnemy = transform.position - enemyRB.transform.position;
            Vector3 knockback = new Vector3(awayFromEnemy.x * knockBackStrength, sojournerRigidBody.velocity.y, awayFromEnemy.z * knockBackStrength);
            //sojournerRigidBody.AddForce(knockback, ForceMode.Impulse);

            sojournerController.MoveDirection += knockback;
            sojournerRigidBody.AddForce(sojournerController.MoveDirection, ForceMode.Impulse);
            StartCoroutine(TempSpeed());

            StartCoroutine(DamageCooldownRoutine());
        }
         
    }

    // health tick down "animation"
    IEnumerator HealthAnim()
    {
        tickDown = false;
        yield return new WaitForSeconds(1);
        tickDown = true;
    }

    // damage cooldown
    IEnumerator DamageCooldownRoutine()
    {
        canDmg = false;
        yield return new WaitForSeconds(dmgCooldown);
        canDmg = true;
    }

    IEnumerator TempSpeed()
    {

        // disable input temporarily
        sojournerController.enableInput = false;
        Debug.Log(sojournerController.enableInput);
        yield return new WaitForSeconds(0.09f);
        sojournerController.enableInput = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("hit");
            Rigidbody enemyRB = other.GetComponent<Rigidbody>();
            TakeDmg(dmg, enemyRB); // sojourner takes 20 damage



        }
    }
}
