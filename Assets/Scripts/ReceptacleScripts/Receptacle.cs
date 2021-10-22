using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptacle : MonoBehaviour, IInteractable
{
    private Inventory inventory;
    private GameObject sojourner;
    private SojournerController s_controller;
    private bool isPlayerColliding;
    private UI_Inventory ui_inventory;
    [SerializeField] private Receptacle_UI receptacle_ui;
    // Start is called before the first frame update
    void Start()
    {
        inventory = new Inventory(32);
        receptacle_ui.SetReceptacle(this);
        receptacle_ui.SetInventory(inventory);
        sojourner = GameObject.Find("Sojourner");
        s_controller = sojourner.GetComponent<SojournerController>();
        this.HideUI();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        //  Set bool is Player colliding to true if collider is player
        if (other.gameObject.CompareTag("Player"))
        {
            SetIsPlayerColliding(true);
            Debug.Log("player colliding with receptacle");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetIsPlayerColliding(false);
            //  tell the s* controller that receptacle UI is hidden
            //  Hide the player ui and tell controller
            if (s_controller.GetIsReceptUIVis() == true)
            {
                s_controller.HideUI();
                s_controller.SetIsUiVisible(false);
            }
            s_controller.SetIsReceptUIVis(false);
            Debug.Log("player no longer colliding with receptacle");
            //  Hide receptacle ui
            this.HideUI();
        }
    }

    public void Interact()
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
}
