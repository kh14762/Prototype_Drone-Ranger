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

    public Sprite swordSprite;
    public Sprite healthPotionSprite;
    public Sprite manaPotionSprite;
    public Sprite coinSprite;
    public Sprite medkitSprite;
    public Sprite cubeSprite;
    public Sprite MetalScrapSprite;
    public Sprite PolymerScrapSprite;
    public Sprite SiliconScrapSprite;
}
