using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Weapon weapon;
    private bool allowAttack;
    private float attackCooldown = 3;


    // Start is called before the first frame update
    void Start()
    {
        allowAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        while(other.CompareTag("Player") && allowAttack)
        {
            weapon.Attack();
            StartCoroutine(AttackRoutine());


        }
    }

    // attack cooldown
    IEnumerator AttackRoutine()
    {
        allowAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        allowAttack = true;
    }
}
