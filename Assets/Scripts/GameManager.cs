using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public HealthBar sojournerHealthBar;
    public SojournerHealth sojournerHealth;
    public SojournerController sojournerController;
    public CinemachineFreeLook cinemachineFreeLook;
    public Button respawnButton;
    public bool isGameActive;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;

    }

    // Update is called once per frame
    void Update()
    {
        if(sojournerHealth.currentHealth == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        isGameActive = false; 
        respawnButton.gameObject.SetActive(true); // show respawn button
        Cursor.lockState = CursorLockMode.None; // unlock cursor
        cinemachineFreeLook.gameObject.SetActive(false); // disable camera

        
    }

    public void Respawn()
    {
        respawnButton.gameObject.SetActive(false); // hide respawn button
        isGameActive = true;
        Cursor.lockState = CursorLockMode.Locked; // lock cursor
        sojournerHealth.currentHealth = sojournerHealth.maxHealth; // set sojourner health to max
        sojournerHealthBar.SetHealth(sojournerHealth.currentHealth); // update health bar
        sojournerController.transform.position = new Vector3(0, 1, 8); // set sojourner position
        cinemachineFreeLook.gameObject.SetActive(true); // enable camera
    }
}
