using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManufactoringSystem : MonoBehaviour, IInteractable
{
    private SojournerController sojournerController;
    bool isPlayerColliding;
    bool isUiVisible;
    // Start is called before the first frame update
    void Start()
    {
        sojournerController = GameObject.Find("sojourner").GetComponent<SojournerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIsPlayerColliding(bool isPlayerColliding)
    {
        this.isPlayerColliding = isPlayerColliding;
    }

    public bool GetIsPlayerColliding()
    {
        return isPlayerColliding;
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }
    public void SetIsUIVisible(bool isUiVisible)
    {
        this.isUiVisible = isUiVisible;
    }
    public bool GetIsUIVisible()
    {
        return isUiVisible;
    }
}
