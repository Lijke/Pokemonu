using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PokemonStorage : MonoBehaviour
{
    [SerializeField] public List<GameObject> pokemon1;

public void SendPokemonInStorage()
    {
        foreach (GameObject pokemon1 in pokemon1)
            {
            GameObject.Find("Player").GetComponentInChildren<BattleManager>().playerPrefab.Add(pokemon1);
        }
    }

}
