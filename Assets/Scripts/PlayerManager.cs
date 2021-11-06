using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    #region Singleton  

    public static PlayerManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    // change reference to player when they spawn
    public GameObject player;


}
