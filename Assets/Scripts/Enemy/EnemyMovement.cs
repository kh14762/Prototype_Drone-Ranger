using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private EnemyCombat enemyCombat;
    public GetPlayer getPlayer;
    public GameObject enemyHitbox;

    public float lookRange = 10f;
    public float lookSpeed = 7f;

    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        enemyCombat = GetComponent<EnemyCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        // distance from enemy to player
        float distance = Vector3.Distance(target.position, transform.position);

        // start chasing player if within look range
        if(distance <= lookRange)
        {
            agent.SetDestination(target.position);
            enemyHitbox.SetActive(true);

            if(distance <= agent.stoppingDistance)
            {
                // enemy attack
                if (getPlayer.playerStats != null)
                {
                    enemyCombat.Attack(getPlayer.playerStats);
                }
                

                // look in the direction of the player
                FacePlayer();
            }
        } else
        {
            enemyHitbox.SetActive(false);
        }

        
    }

    void FacePlayer()
    {
        // direction of the player
        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 lookDirection = new Vector3(direction.x, 0, direction.z);
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookSpeed);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRange);
    }

   
}
