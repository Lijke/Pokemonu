using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsPopOutUi : MonoBehaviour
{
    public GameObject buttonItems;

    public void ItemsPopOut()
    {
        buttonItems.SetActive(true);
    }
}
