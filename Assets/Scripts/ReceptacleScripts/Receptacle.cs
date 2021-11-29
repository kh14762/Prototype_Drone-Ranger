using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptacle : MonoBehaviour
{
    private Inventory inventory;
    private GameObject sojourner;
    private SojournerController s_controller;

    private Receptacle_UI receptacle_ui;
    // Start is called before the first frame update
    private UI_Controller ui_controller;

    void Start()
    {
        

        inventory = new Inventory(32);
        receptacle_ui = GameObject.Find("ReceptacleUI").GetComponent<Receptacle_UI>();
        receptacle_ui.SetReceptacle(this);
        receptacle_ui.SetInventory(inventory);
        Debug.Log("ui " + receptacle_ui);

        //ui_controller = receptacle_ui.GetComponent<UI_Controller>();
        ui_controller = GameObject.Find("ReceptacleUI").GetComponent<UI_Controller>();
        ui_controller.HideUI();

        sojourner = GameObject.Find("Sojourner");
        s_controller = sojourner.GetComponent<SojournerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ToggleInventory();
        
        
    }

    public void ToggleInventory()
    {
        if (Vector3.Distance(s_controller.transform.position, gameObject.transform.position) <= 5)
        {
            Debug.Log("player colliding with receptacle");

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!ui_controller.GetIsUiVisible())
                {
                    ui_controller.ShowUI();
                    s_controller.ShowUI();
                    s_controller.SetIsUiVisible(true);
                }
                else
                {
                    ui_controller.HideUI();
                    s_controller.HideUI();
                    s_controller.SetIsUiVisible(false);
                }
            }
        }
        else
        {
            //  Hide receptacle ui
            ui_controller.HideUI();
            
        }
    }

    public Inventory GetInventory()
    {
        return inventory;
    }


}
