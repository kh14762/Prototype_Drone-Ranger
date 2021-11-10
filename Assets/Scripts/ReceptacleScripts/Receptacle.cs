using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptacle : MonoBehaviour
{
    private Inventory inventory;
    private GameObject sojourner;
    private SojournerController s_controller;

    [SerializeField] private Receptacle_UI receptacle_ui;
    // Start is called before the first frame update
    private UI_Controller ui_controller;

    void Start()
    {
        

        inventory = new Inventory(32);
        /*receptacle = GameObject.FindGameObjectWithTag("ReceptacleUI");
        Debug.Log(receptacle);*/

        receptacle_ui.SetReceptacle(this);
        receptacle_ui.SetInventory(inventory);
        Debug.Log("ui " + receptacle_ui);

        //ui_controller = receptacle_ui.GetComponent<UI_Controller>();
        ui_controller = GameObject.Find("ReceptacleUI").GetComponent<UI_Controller>();
        Debug.Log(ui_controller);

        ui_controller.HideUI();

        sojourner = GameObject.Find("Sojourner");
        s_controller = sojourner.GetComponent<SojournerController>();
        Debug.Log(s_controller);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    private void OnTriggerStay(Collider other)
    {
        //  Set bool is Player colliding to true if collider is player
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("player colliding with receptacle");
            if (!ui_controller.GetIsUiVisible())
            {
                ui_controller.ShowUI();
                s_controller.ShowUI();
                s_controller.SetIsUiVisible(true);
            } else
            {
                ui_controller.HideUI();
                s_controller.HideUI();
                s_controller.SetIsUiVisible(false);
            }
            


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //  Hide receptacle ui
            ui_controller.HideUI();
            
            s_controller.HideUI();
            s_controller.SetIsUiVisible(false);
            Debug.Log("player no longer colliding with receptacle");
            
            
        }
    }

    /*public void Interact()
    {
        ShowUI();
    }

    public void ShowUI()
    {
        receptacle_ui.GetCanvasGroup().alpha = 1f;
        receptacle_ui.GetCanvasGroup().blocksRaycasts = true;
    }
    public void HideUI()
    {
        receptacle_ui.GetCanvasGroup().alpha = 0f;
        receptacle_ui.GetCanvasGroup().blocksRaycasts = false;
    }


    public void SetIsPlayerColliding(bool isPlayerColliding)
    {
        this.isPlayerColliding = isPlayerColliding;
    }

    public bool GetIsPlayerColliding()
    {
        return isPlayerColliding;
    }
    public void SetReceptacleUI(Receptacle_UI receptacle_ui)
    {
        this.receptacle_ui = receptacle_ui;
    }

    public Receptacle_UI GetReceptacleUI()
    {
        return receptacle_ui;
    }*/

}
