using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseStat;

    public int GetValue()
    {
        return baseStat;
    }

    public void SetValue(int stat)
    {
        baseStat += stat;
    }
}
