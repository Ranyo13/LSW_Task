using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum ItemType {Head, Body}

[Serializable]
public class Item
{
    public Sprite itemEquipSprite;
    public ItemType itemType;
    public string name;
    public int cost;
}
