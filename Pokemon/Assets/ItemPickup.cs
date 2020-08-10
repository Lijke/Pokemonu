using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    BackpackSystem backpack;
    public GameObject item;

    public void Start()
    {
        backpack = GameObject.Find("Backpack").GetComponent<BackpackSystem>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            backpack.item.Add(item);
            Destroy(gameObject);
        }
        
    }
}
