using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    public int dmgModifier;

    public bool isEquipped = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void Attack()
    {
        animator.SetTrigger("Attack");

    }

    public void Equip()
    {
        // play on equip animation
        gameObject.SetActive(true); 

        isEquipped = true;
    }

    public void Unequip()
    {
        // play unequip animation
        gameObject.SetActive(false);

        isEquipped = false;
    }
}
