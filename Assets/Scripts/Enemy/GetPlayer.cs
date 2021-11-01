using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayer : MonoBehaviour
{
    public PlayerStats playerStats;

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerStats = other.GetComponent<PlayerStats>();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerStats = null;
        }
    }
}
