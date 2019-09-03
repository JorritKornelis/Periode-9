﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class ItemClassScriptableObject : ScriptableObject
{
    public List<ItemInformationClass> itemInformationList = new List<ItemInformationClass>();

}

[System.Serializable]
public class ItemInformationClass
{
    public string name;
    public Sprite Sprite;
    public int maxStack;
    public GameObject itemGameObject;

}
