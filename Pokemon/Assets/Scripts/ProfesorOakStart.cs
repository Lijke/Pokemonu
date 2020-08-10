using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfesorOakStart : MonoBehaviour
{
    public List<GameObject> pokeballsTotake;

    public void Start()
    {
        pokeballsTotake[0].SetActive(false);
        pokeballsTotake[1].SetActive(false);
        pokeballsTotake[2].SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
       
        if(pokeballsTotake[0] == null)
        {
            return;
        }
        else
        {
            pokeballsTotake[0].SetActive(true);
            pokeballsTotake[1].SetActive(true);
            pokeballsTotake[2].SetActive(true);
        }
    }
}
