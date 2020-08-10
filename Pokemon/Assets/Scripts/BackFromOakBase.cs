 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackFromOakBase : MonoBehaviour
{
    public Transform teleportTo;
    public GameObject player;
    public void Start()
    {
        player = GameObject.Find("Player");
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            player.transform.position = teleportTo.transform.position;
    }
}
