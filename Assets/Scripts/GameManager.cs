using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GameManager : MonoBehaviour
{

    public PlayerStats playerStats;
    public SojournerController sojournerController;
    public CinemachineFreeLook cinemachineFreeLook;
    public Button respawnButton;
    public bool isGameActive;
    public bool cursorLock;
    public GameObject printer;
    private Vector3 printerLoc;

    //  Manufactoring Systems
    public GameObject pfReceptacle;
    public GameObject pfRefiner;
    //public GameObject pfPrinter;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;

        //  Instantiate receptacle
        Instantiate(pfReceptacle, new Vector3(7.78f, -5.89f, 7.74f), Quaternion.identity);
        Instantiate(pfRefiner, new Vector3(7.78f, -3.05f, -1f), Quaternion.identity);

        cursorLock = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {
        printerLoc = printer.transform.position;
        if (Input.GetKeyDown(KeyCode.Q))
        {

            if (cursorLock)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                cursorLock = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                cursorLock = true;
            }


        }
    }

    public void GameOver()
    {
        isGameActive = false; 
        respawnButton.gameObject.SetActive(true); // show respawn button
        //Cursor.lockState = CursorLockMode.None; // unlock cursor
        cinemachineFreeLook.gameObject.SetActive(false); // disable camera

        
    }

    public void Respawn()
    {
        playerStats.Awake();
        respawnButton.gameObject.SetActive(false); // hide respawn button
        isGameActive = true;
        //Cursor.lockState = CursorLockMode.Locked; // lock cursor
        sojournerController.transform.position = printerLoc; // set sojourner position
        cinemachineFreeLook.gameObject.SetActive(true); // enable camera
    }
}
