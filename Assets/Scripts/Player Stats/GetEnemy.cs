using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEnemy : MonoBehaviour
{
    public EnemyStats enemyStats;

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CheckForEnemies();
        }*/

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyStats = other.GetComponent<EnemyStats>();
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyStats = other.GetComponent<EnemyStats>();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyStats = null;
        }
    }


    // check for multiple enemies
    /*public EnemyStats[] CheckForEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(sojourner.transform.position + new Vector3(0,0,3), 2f);
        EnemyStats[] enemies = new EnemyStats[colliders.Length];
        int i = 0;
 

        foreach (Collider c in colliders)
        {
            if (c.GetComponent<EnemyStats>())
            {
                enemies.SetValue(c.GetComponent<EnemyStats>(), i);
                i++;
            }
        }

        return enemies;
    }*/
}
