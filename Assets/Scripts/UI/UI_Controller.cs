using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    private bool isUiVisible = true;
    private GameObject ui;
    private CanvasGroup canvasGroup;
    public ManufactoringSystem system;

    void Start()
    {
        ui = gameObject;
        canvasGroup = ui.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIsUiVisible(bool isUiVisible)
    {
        this.isUiVisible = isUiVisible;
    }
    public bool GetIsUiVisible()
    {
        return isUiVisible;
    }

    public void ShowUI()
    {
        GetCanvasGroup().alpha = 1f;
        GetCanvasGroup().blocksRaycasts = true;
    }
    public void HideUI()
    {
        GetCanvasGroup().alpha = 0f;
        GetCanvasGroup().blocksRaycasts = false;
    }

    public CanvasGroup GetCanvasGroup()
    {
        return canvasGroup;
    }
}
