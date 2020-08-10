using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Runtime.InteropServices.ComTypes;
using System.Linq;
using TMPro;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, NEXTPOKEMON, WON, LOST, POKEMONDEAD, CATCH_POKEMON,CHOSE_ACCTION}
public enum PlayerPokemonType { normal,fire,water,grass}
public enum EnemyPokemonType { normal, fire, water, grass }

public class BattleManager : MonoBehaviour
{   [Header("Enumy")]
    public BattleState state;
    public UnitType unitType;
    public UnitEnemyType enemyType;
    public MoveType moveType;
    PlayerPokemonType playerPokemonType;
    [Header("Listy Pokemon")]
    public List<GameObject> playerPrefab;
    public List<GameObject> CurrentPlayerPrefab; 
    public List<GameObject> enemyPrefab;
    public List <GameObject> playerGameObject;
    public List<GameObject> enemyGameObject;
    public int activePokemonIndex;
    [Header("UI")]
    public Transform playerBattleStation;
    public Transform enemyBattleStation;
    public Text dialogueText;
    public Button pokeballButton;
    public GameObject dialogueUI;
    //Fighting
    public GameObject battleMenuRight;
    public List<GameObject> action;
    public int currentAction;
    public List<GameObject> battleIndicator;
    public bool isChosingAction;
    public int maxAcction;
    public int minAcction;
    public List<TextMeshProUGUI> nameMoves;
    public List<GameObject> indicatorMoves;
    public TextMeshProUGUI ppValue;
    public TextMeshProUGUI spellType;
    public GameObject acctionMenu;
    public bool chosingAttack;
    // pokemon
    public GameObject pokemonListUI;
    MenuUiPokemons menuPokemons;
    [Header("Catching Pokemon")]
    public GameObject menuPokeballs;
    public List<GameObject> menuPokeballIndicator;
    public List<TextMeshProUGUI> pokeballCount;
    public int menuPokeballIndicatorIndex;
    public bool menuPokeballIsActive;
    public bool menuPokeBallChosingIsActive;
    public bool pokemonCought;
    public int enemyrandom;
    ///Items
    [Header("Items")]
    public GameObject menuItems;
    UiManagerMenu itemsManager;
    [Header("Inne")]
    public BattleHud playerHUD;
    public BattleHud enemyHUD;
    BackpackSystem backpack;
    test Test;
    public int pokemonIndex;
    [SerializeField]
    public static bool startbattle;
    Unit playerUnit;
    Unit EnemyUnit;
    public GameObject battlecamera;
    public int enemyRandom;
    public int gainXP;
    public bool isPokemonDead;
    public bool isWildPokemonFight;
    public List<GameObject> currentEnemyPokemon;
    [Header("Liczniki")]
    public int PlayerPokemonIndex;
    public int CountPokemonDead;
    private void Start()
    {
        state = BattleState.START;
        Test = GameObject.Find("Player").GetComponent<test>();
        backpack = GameObject.Find("Backpack").GetComponent<BackpackSystem>();
        menuPokemons = GameObject.Find("MenuUiPokemon").GetComponent<MenuUiPokemons>();
        itemsManager = GameObject.Find("MenuUiManager").GetComponent<UiManagerMenu>();
        PlayerPokemonIndex = 1;
        CountPokemonDead = 0;
        currentAction = 0;
        maxAcction = 3;
        minAcction = 0;

    }
    public void Update()
    {
        if (startbattle == true)
        {
            
            StartCoroutine(SetupBattle());
            startbattle = false;
            test.fight = true;
        }
        //Moving Indicator menu
        if (isChosingAction == true)
        {
            maxAcction = 3;
            if (Input.GetKeyUp(KeyCode.RightArrow) && currentAction <maxAcction)
            {
                movingMenuRight();

            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow) &&  currentAction > minAcction )
            {
                movingMenuLeft();
            }
            else if (Input.GetKeyUp(KeyCode.KeypadEnter) && isChosingAction == true)
            {

                ChoseMenu();
            }
            if(Input.GetKeyUp(KeyCode.KeypadEnter) && currentAction==2)
            {
                menuPokeballIndicatorIndex = 0;
                battlecamera.SetActive(false);
                pokemonListUI.SetActive(true);
                menuPokemons.menuPokemonIsOpen = true;
                menuPokemons.showPokemonInFight();

            }
        }
        //moving indicator acctionMenu
        if (chosingAttack == true)
        {
            chosingAttack = false;
            chosingAttack = true;
            maxAcction = playerUnit.moves.Count-1;
            if (Input.GetKeyUp(KeyCode.RightArrow) && currentAction < maxAcction)
            {
                currentAction += 1;
                indicatorMoves[currentAction].SetActive(true);
                indicatorMoves[currentAction - 1].SetActive(false);
                nameMoves[currentAction].text = playerUnit.moves[currentAction].movesName;
                ppValue.text = playerUnit.moves[currentAction].pp.ToString();
                spellType.text = playerUnit.moves[currentAction].moveType.ToString();
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow) && currentAction > minAcction)
            {
                currentAction -= 1;
                indicatorMoves[currentAction].SetActive(true);
                indicatorMoves[currentAction + 1].SetActive(false);
                nameMoves[currentAction].text = playerUnit.moves[currentAction].movesName;
                ppValue.text = playerUnit.moves[currentAction].pp.ToString();
                spellType.text = playerUnit.moves[currentAction].moveType.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.KeypadEnter) && chosingAttack == true)
            {
                if(playerUnit.moves[currentAction].pp <= 0)
                {
                    chosingAttack = false;
                    indicatorMoves[0].SetActive(true);
                    indicatorMoves[currentAction].SetActive(false);
                    currentAction = 0;
                    acctionMenu.SetActive(false);
                    StartCoroutine(showMesseageOutOfPP());
                }
                else
                {
                    playerUnit.moves[currentAction].pp -= 0;
                    state = BattleState.PLAYERTURN;
                    chosenSpell();
                }
            }
        }
        //moving indicator in menu fight pokeballs
        if (menuPokeballIsActive == true)
        {
            if (Input.GetKeyUp(KeyCode.RightArrow) && menuPokeballIsActive == true && menuPokeballIndicatorIndex <3)
            {
                menuPokeballIndicator[menuPokeballIndicatorIndex].SetActive(false);
                menuPokeballIndicatorIndex += 1;
                menuPokeballIndicator[menuPokeballIndicatorIndex].SetActive(true);
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow) && menuPokeballIsActive == true && menuPokeballIndicatorIndex >0)
            {
                menuPokeballIndicator[menuPokeballIndicatorIndex].SetActive(false);
                menuPokeballIndicatorIndex -= 1;
                menuPokeballIndicator[menuPokeballIndicatorIndex].SetActive(true);
            }
            else if (Input.GetKeyUp(KeyCode.KeypadEnter) && menuPokeBallChosingIsActive == true)
            {
                menuPokeBallChosingIsActive = false;

                StartCoroutine(ChosingPokeball());
            }
        }
       
    }
    public IEnumerator SetupBattle()
    {
        if (state == BattleState.START & isPokemonDead == false)
        {
            
            enemyrandom = Random.Range(0, enemyPrefab.Count);
            enemyRandom = enemyrandom;
            if(playerGameObject.Count == 0)
            {
               
                //spawn player pokemon
                playerGameObject.Add(playerPrefab[0]);
                playerGameObject[0] = Instantiate(playerPrefab[0], playerBattleStation);
                playerUnit = playerGameObject[0].GetComponent<Unit>();

                //spawn enemy
                GameObject enemyGameObject = Instantiate(enemyPrefab[enemyRandom], enemyBattleStation);
                EnemyUnit = enemyGameObject.GetComponent<Unit>();
                activePokemonIndex = 0;
                //sprawdzamy jak mamy 1 pokemona czy on żyje?
                if (playerGameObject[0].GetComponent<Unit>().currentHp <= 0)
                {
                    state = BattleState.PLAYERTURN;
                    PlayerTurn();
                }
            }
            else if(playerGameObject.Count >=1)
            {
                
                for (int i=0; i< playerGameObject.Count; i++)
                {
                    if(playerGameObject[i].GetComponent<Unit>().currentHp>0)
                    {
                        playerGameObject[i].SetActive(true);
                        playerUnit = playerGameObject[i].GetComponent<Unit>();
                        activePokemonIndex = i;
                        for (int j=0; j<playerGameObject.Count; j++)
                        {
                            if (activePokemonIndex==j)
                            {
                                
                            }
                            else
                            {
                                
                                playerGameObject[j].SetActive(false);
                            }

                        }
                        break;
                    }
                    else if(playerGameObject[i].GetComponent<Unit>().currentHp <=0)
                    {
                        playerGameObject[i].SetActive(false);
                        CountPokemonDead += 1;
                    }
                }
                GameObject enemyGameObject = Instantiate(enemyPrefab[enemyRandom], enemyBattleStation);
                EnemyUnit = enemyGameObject.GetComponent<Unit>();
                currentEnemyPokemon.Add(enemyGameObject);
                state = BattleState.PLAYERTURN;
            }    


            dialogueText.text = "Wild " + EnemyUnit.unitName + " attack you";
            playerHUD.SetHUD(playerUnit);
            enemyHUD.SetHUD(EnemyUnit);
            state = BattleState.PLAYERTURN;
            yield return new WaitForSeconds(2f);
            battleMenuRight.SetActive(true);
            PlayerTurn();
        }
        else if (state == BattleState.NEXTPOKEMON)
        {
            for ( int i=0; i<playerGameObject.Count;i++)
            {

                if(playerGameObject[i].GetComponent<Unit>().currentHp >1)
                {
                    playerGameObject[i].SetActive(true);
                    playerUnit = playerGameObject[i].GetComponent<Unit>();
                    activePokemonIndex = i;
                    break;
                }
            }
            for (int i=0; i<playerGameObject.Count; i++)
            {
                 if (playerGameObject[i].GetComponent<Unit>().currentHp <= 0)
                    {
                    Debug.Log("POKEMON DEAD");
                    playerGameObject[i].SetActive(false);
                    CountPokemonDead += 1;
                    }
            }
            
            playerHUD.SetHUD(playerUnit);
            enemyHUD.SetHUD(EnemyUnit);
            state = BattleState.PLAYERTURN;
            yield return new WaitForSeconds(1f);
            battleMenuRight.SetActive(true);
            PlayerTurn();
        }
    }
    IEnumerator PlayerAttack()
    {
     
            
        //enemy type
        enemyType = EnemyUnit.unitEnemyType;
        int enemyTypeee = (int)EnemyUnit.unitEnemyType;
        // player spell type
        moveType = playerUnit.moves[currentAction].moveType;
        int playerSpellType = (int)playerUnit.moves[currentAction].moveType;
        
                float[][] chart =
                {//                      nor  fir wat  grass
                   /*nor*/ new float[] { 2f, 2f, 2f, 2f },
                   /*fire*/new float[] { 2f, 1f, 2f, 4f },
                   /*water*/new float[] { 2f, 4f, 1, 1f },
                   /*grass*/new float[] { 2f, 1f, 4f, 1f }
                };
                int row = (int)unitType - 1;
                int col = (int)unitType - 1;
                int effectiveness = (int) chart[playerSpellType][enemyTypeee];
                


        bool isDead = EnemyUnit.TakeDamage(playerUnit.damage*effectiveness);
       
        enemyHUD.SetHP(EnemyUnit.currentHp);
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            if(isWildPokemonFight == true)
            {
                state = BattleState.WON;
                EndBattle();
            }
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the  battle!";
            battlecamera.SetActive(false);
            currentEnemyPokemon = new List<GameObject>();
            isWildPokemonFight = false;
            gainXP = 4 * EnemyUnit.unitLevel ^ 3 / 2;
            playerUnit.LevelSystem(gainXP);
            state = BattleState.START;
            Destroy(EnemyUnit.gameObject);
            test.fight = false;
            CountPokemonDead = 0;
            isPokemonDead = false;
            acctionMenu.SetActive(false);
            battleMenuRight.SetActive(true);
            chosingAttack = false;
            isChosingAction = false;
            menuPokeballs.SetActive(false);
            menuPokeBallChosingIsActive = false;
            menuPokeballIsActive = false;

            battleIndicator[0].SetActive(true);
            battleIndicator[currentAction].SetActive(false);
            currentAction = 0;

        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeted!";
            battlecamera.SetActive(false);
            state = BattleState.START;
            Destroy(EnemyUnit.gameObject);
            test.fight = false;
            CountPokemonDead = 0;
            isPokemonDead = false;
            acctionMenu.SetActive(false);
            battleMenuRight.SetActive(true);
            chosingAttack = false;
            isChosingAction = false;
            menuPokeBallChosingIsActive = false;
            menuPokeballIsActive = false;
            currentAction = 0;
        }
        else if (state == BattleState.POKEMONDEAD)
        {
            dialogueText.text = "Ulecz swoje pokemony!";
            state = BattleState.START;
            battlecamera.SetActive(false);
            test.fight = false;
            CountPokemonDead = 0;
            isPokemonDead = false;
            acctionMenu.SetActive(false);
            battleMenuRight.SetActive(true);
            chosingAttack = false;
            isChosingAction = false;
            menuPokeBallChosingIsActive = false;
            menuPokeballIsActive = false;
            currentAction = 0;

            battleIndicator[0].SetActive(true);
            battleIndicator[currentAction].SetActive(false);
            currentAction = 0;

        }
        else if (pokemonCought== true)
        {
            pokemonCought = false;
            state = BattleState.START;
            battlecamera.SetActive(false);
            test.fight = false;
            CountPokemonDead = 0;
            isPokemonDead = false;
            menuPokeBallChosingIsActive = false;
            menuPokeballIsActive = false;
            currentAction = 0;

            battleIndicator[0].SetActive(true);
            battleIndicator[currentAction].SetActive(false);
            currentAction = 0;
        }
    }
    IEnumerator EnemyTurn()
    {

        battlecamera.SetActive(true);
        acctionMenu.SetActive(false);
        isChosingAction = false;
        chosingAttack = false;
        


        menuPokeballIndicator[0].SetActive(false);
        menuPokeballIndicator[1].SetActive(false);
        menuPokeballIndicator[2].SetActive(false);
        menuPokeballIndicator[3].SetActive(false);

        menuPokeballIndicator[0].SetActive(true);
        menuPokeballIndicatorIndex = 0;
        dialogueText.text = EnemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(EnemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHp);
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.NEXTPOKEMON;
            StartCoroutine(SetupBattle());

        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }
    void PlayerTurn()
    {
        dialogueText.text = "Chose an action";
        battleIndicator[0].SetActive(true);
        battleIndicator[1].SetActive(false);
        battleIndicator[2].SetActive(false);
        battleIndicator[3].SetActive(false);
        currentAction = 0;
        indicatorMoves[0].SetActive(true);
        indicatorMoves[1].SetActive(false);
        indicatorMoves[2].SetActive(false);
        indicatorMoves[3].SetActive(false);

        isChosingAction = true;
        if (CountPokemonDead == playerGameObject.Count)
        {
            state = BattleState.LOST;
            isPokemonDead = true;
            EndBattle();
            
        }
        CountPokemonDead =0;

        PlayerAttack();
    }
    public void CatchPokemon()
    {

            menuPokeballs.SetActive(false);
            int catchPokemon;
            catchPokemon = backpack.pokeballs[menuPokeballIndicatorIndex].GetComponent<Item>().procentCatch;
            int pokemonCatch = Random.Range(0, catchPokemon);
            int pokemonCatch1 = pokemonCatch;
            backpack.pokeballs[menuPokeballIndicatorIndex].GetComponent<Item>().itemCount -= 1;
            if (EnemyUnit.procentCatch < pokemonCatch1)
            {
                pokemonCought = true;

                playerPrefab.Add(enemyPrefab[enemyRandom]);
                playerGameObject.Add(enemyPrefab[enemyRandom]);
                playerGameObject[PlayerPokemonIndex] = Instantiate(playerPrefab[PlayerPokemonIndex], playerBattleStation);
                PlayerPokemonIndex += 1;
                state = BattleState.WON;
                StartCoroutine(showMessegaeCatch());
            }
            else
            {
                
                menuPokeballIsActive = false;
                state = BattleState.ENEMYTURN;
                pokemonCought = false;
                StartCoroutine(showMessegaeCatch());
            }
        
        
    }
    public void HealingPokemon()
    {
        for (int i = 0; i <= playerGameObject.Count-1 ; i++)
        {
            //playerGameObject[i] = playerGameObject[i];
            playerUnit = playerGameObject[i].GetComponent<Unit>();
            playerUnit.Healing();
            playerGameObject[0].SetActive(true);
            for (int j = 1; j < playerGameObject.Count; j++)
            {
                playerGameObject[j].SetActive(false);
            }
        }
    }
    public void choseAcction()
    {
        battleMenuRight.SetActive(true);
        battleIndicator[0].SetActive(true);
        for (int i=1; i<battleIndicator.Count; i++)
        {
            battleIndicator[i].SetActive(false);
        }
        
    }
    public void ChoseMenu()
    {
        
        if(currentAction ==0)
        {
            acctionMenu.SetActive(true);
            int maxMoves = playerUnit.GetComponent<Unit>().moves.Count;
            for (int i=0; i<maxMoves; i++)
            {
                nameMoves[i].text = playerUnit.moves[i].movesName;
            }
            for (int i=maxMoves; i<4; i++)
            {
                nameMoves[i].text = " ";
            }
            ppValue.text = playerUnit.moves[0].pp.ToString();
            spellType.text = playerUnit.moves[0].moveType.ToString();
            chosingAttack = true;
            currentAction = 0;
            isChosingAction = false;
        }
        else if(currentAction == 1)
        {
            menuPokeballs.SetActive(true);
            battlecamera.SetActive(false);
            menuPokeballIndicatorIndex = 0;
            menuPokeballIsActive = true;
            isChosingAction = false;
            for (int i = 0; i < backpack.pokeballs.Count; i++)
            {
                pokeballCount[i].text = backpack.pokeballs[i].GetComponent<Item>().itemCount.ToString();
            }
            StartCoroutine(Test123());
        }
        else if (currentAction == 3)
        {
            battlecamera.SetActive(false);
            currentAction = -10;
            isChosingAction = false;
            itemsManager.ItemOpenWHenFight();
            
        }
    }
    IEnumerator Test123()
    {
        yield return new WaitForSeconds(1f);
        menuPokeBallChosingIsActive = true;
        
    }
    IEnumerator ChosingPokeball()
    {
        
        if (backpack.pokeballs[menuPokeballIndicatorIndex].GetComponent<Item>().itemCount <=0)
        {
            Debug.Log("nie mozesz tego wybrac");
        }
        else
        {
            menuPokeballIndicator[menuPokeballIndicatorIndex].SetActive(false);
            menuPokeballIndicator[0].SetActive(true);
            CatchPokemon();
        }
       
        
        
        
        yield return new WaitForSeconds(0.2f);
        menuPokeBallChosingIsActive = false;
        

    }
    //after picking spell
    public void chosenSpell()
    {
        StartCoroutine(PlayerAttack());
    }
    public void movingMenuRight()
    {
        currentAction += 1;
        battleIndicator[currentAction].SetActive(true);
        battleIndicator[currentAction - 1].SetActive(false);
    }
    public void movingMenuLeft()
    {
        currentAction -= 1;
        battleIndicator[currentAction].SetActive(true);
        battleIndicator[currentAction + 1].SetActive(false);
    }
    public void cancelMenuPokeball()
    {
        menuPokeballs.SetActive(false);
        battleIndicator[0].SetActive(true);
        battleIndicator[1].SetActive(false);
        menuPokeballIndicator[menuPokeballIndicatorIndex].SetActive(false);
        menuPokeballIndicatorIndex = 0;
        menuPokeballIndicator[0].SetActive(true);
        menuPokeballIsActive = false;
        menuPokeBallChosingIsActive = false;
        isChosingAction = true;
        currentAction = 0;
        battlecamera.SetActive(true);

    }
    public void changePokemon(int pokemonIndex)
    {
        Debug.Log(pokemonIndex);
        battlecamera.SetActive(true);
        playerGameObject[activePokemonIndex].SetActive(false);
        playerGameObject[pokemonIndex].SetActive(true);
        playerUnit = playerGameObject[pokemonIndex].GetComponent<Unit>();
        playerHUD.SetHUD(playerUnit);
        dialogueText.text = playerUnit.unitName + " Go!";
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void changeOrderPokemon(int firstPokemon, int secondPokemon)
    {
        GameObject buforFirstPokemon;
        GameObject buforSecondPokemon;
        buforFirstPokemon = playerGameObject[firstPokemon];
        buforSecondPokemon = playerGameObject[secondPokemon];
        playerGameObject[firstPokemon] = buforSecondPokemon;
        playerGameObject[secondPokemon] = buforFirstPokemon;
    }
    public void HealingByPotion(int activePokemon, int activePotion)
    {
        int currentHealth = playerGameObject[activePokemon].GetComponent<Unit>().currentHp;
        int maxHealth = playerGameObject[activePokemon].GetComponent<Unit>().maxHp;
        backpack.item[activePokemon].GetComponent<Item>().itemCount -= 1;
        if (currentHealth + 15 > maxHealth)
        {
            playerGameObject[activePokemon].GetComponent<Unit>().currentHp = maxHealth;
        }
        else
        {
            playerGameObject[activePokemon].GetComponent<Unit>().currentHp += 15;
        }
        backpack.item[activePotion].GetComponent<Item>().itemCount -= 1;
        battlecamera.SetActive(true);
        isChosingAction = true;
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    IEnumerator showMessegaeCatch()
    {
        battlecamera.SetActive(true);
        if(pokemonCought == true)
        {
            playerGameObject[playerGameObject.Count-1].SetActive(false);
            dialogueText.text = "Gotcha! " + EnemyUnit.unitName + " was caught";
            yield return new WaitForSeconds(2f);
            
            state = BattleState.WON;
            EndBattle();

        }
        else if(pokemonCought==false)
        {
            dialogueText.text = "Aaargh! You almost have it!";
            yield return new WaitForSeconds(2f);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());

        }
        
        
    }
    IEnumerator showMesseageOutOfPP()
    {
        dialogueText.text = "You can't use this move";
        yield return new WaitForSeconds(1);
        acctionMenu.SetActive(true);
        chosingAttack = true;
        
    }
}