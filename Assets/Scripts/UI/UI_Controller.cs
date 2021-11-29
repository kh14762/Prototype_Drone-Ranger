using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    private bool isUiVisible = true;
    //public GameObject ui;
    private CanvasGroup canvasGroup;



    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ShowUI()
    {
       // Cursor.lockState = CursorLockMode.None; // unlock cursor
        GetCanvasGroup().alpha = 1f;
        GetCanvasGroup().blocksRaycasts = true;
        SetIsUiVisible(true);
    }
    public void HideUI()
    {
        //Cursor.lockState = CursorLockMode.Locked; // lock cursor
        GetCanvasGroup().alpha = 0f;
        GetCanvasGroup().blocksRaycasts = false;
        SetIsUiVisible(false);
    }
    public void SetIsUiVisible(bool isUiVisible)
    {
        this.isUiVisible = isUiVisible;
    }
    public bool GetIsUiVisible()
    {
        return isUiVisible;
    }
    public CanvasGroup GetCanvasGroup()
    {
        return canvasGroup;
    }
}
