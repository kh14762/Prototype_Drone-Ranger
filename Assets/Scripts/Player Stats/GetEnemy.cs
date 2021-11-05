using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEnemy : MonoBehaviour
{ 

    public EnemyStats enemyStats;

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

}
