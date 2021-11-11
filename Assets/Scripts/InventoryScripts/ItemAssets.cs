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
    public Sprite RefinedMetal;
    public Sprite RefinedPolymer;
    public Sprite RefinedSilicon;


}
