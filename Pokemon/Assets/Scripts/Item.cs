using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType { potion,stone,pokeball}
public class Item : MonoBehaviour
{
    public ItemType itemType;
    public string itemName;
    public string itemLore;
    public int itemCount;
    public int procentCatch;
}
