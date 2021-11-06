using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public Sprite MetalScrapSprite;
    public Sprite PolymerScrapSprite;
    public Sprite SiliconScrapSprite;
    public Sprite RefinedMetalSprite;
    public Sprite RefinedSiliconSprite;
    public Sprite RefinedPolymerSprite;

}
