using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrzakManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> krzakPokemon;
    [SerializeField]
    BattleManager battlemanager;
    public void Start()
    {
        battlemanager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i=0; i<krzakPokemon.Count; i++)
        {
            battlemanager.enemyPrefab.Add(krzakPokemon[i]);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        battlemanager.enemyPrefab = new List<GameObject>();
    }
}
