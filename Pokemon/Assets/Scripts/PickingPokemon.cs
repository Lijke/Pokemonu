using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingPokemon : MonoBehaviour
{
    public GameObject pokemon;
    public GameObject lorePokemon;
    [SerializeField]
    BattleManager battlemanager;
    public GameObject pokemon2, pokemon1;
    public GameObject stopBarrier;
    public void Awake()
    {
        battlemanager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        lorePokemon.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        test.fight = true;
        lorePokemon.SetActive(true);
    }
    public void AccecptPokemon()
    {
        test.fight = false;
        battlemanager.playerPrefab.Add(pokemon);
        Destroy(pokemon2);
        Destroy(pokemon1);
        Destroy(gameObject);
        Destroy(stopBarrier);
        lorePokemon.SetActive(false);
    }
    public void CancelPokemon()
    {
        test.fight = false;
        lorePokemon.SetActive(false);
    }
}
