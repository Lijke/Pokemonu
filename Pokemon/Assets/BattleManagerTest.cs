using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Runtime.InteropServices.ComTypes;
using System.Linq;

public enum BattleStatee { START, PLAYERTURN, ENEMYTURN, NEXTPOKEMON, WON, LOST, POKEMONDEAD, CATCH_POKEMON, NEXT_POKEMON_SPINNER }

public class BattleManagerTest : MonoBehaviour
{

    public BattleStatee state;
    test Test;
    [SerializeField]
    public static bool startbattle;
    public static bool FirstFight;

    [Header("Pokemony")]
    public List<GameObject> PlayerPrefab;
    public List<GameObject> PlayerPokemonGameObject;


    [Header("UI")]
    public Transform playerBattleStation;
    public Transform enemyBattleStation;


    private void Start()
    {
        state = BattleStatee.START;
        Test = GameObject.Find("Player").GetComponent<test>();


    }
    public void Update()
    {
        if (startbattle == true)
        {

            StartCoroutine(SetupBattle());
            startbattle = false;
            test.fight = true;
        }
    }
    public IEnumerator SetupBattle()
    {

        if (FirstFight== true)
        {
            PlayerPokemonGameObject[0] = Instantiate(PlayerPrefab[0], playerBattleStation);
        }
        else if(FirstFight == false)
        {

        }
        yield return new WaitForSeconds(1f);
    }
}
