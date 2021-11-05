using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptacle : MonoBehaviour, IInteractable
{
    private Inventory inventory;
    private GameObject sojourner;
    private SojournerController s_controller;
    private bool isPlayerColliding;
    [SerializeField] private Receptacle_UI receptacle_ui;
    // Start is called before the first frame update
    void Start()
    {
        inventory = new Inventory(32);
        receptacle_ui.SetReceptacle(this);
        receptacle_ui.SetInventory(inventory);
        sojourner = GameObject.Find("Sojourner");
        s_controller = sojourner.GetComponent<SojournerController>();
        Debug.Log(s_controller);
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
    public void SetReceptacleUI(Receptacle_UI receptacle_ui)
    {
        this.receptacle_ui = receptacle_ui;
    }

    public Receptacle_UI GetReceptacleUI()
    {
        return receptacle_ui;
    }

}
