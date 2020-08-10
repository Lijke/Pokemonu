using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class MenuUiPokemons : MonoBehaviour
{
    [SerializeField]
    BattleManager bm;
    public List<Image> pokemonImage;
    public List<TextMeshProUGUI> pokemonName;
    public List<TextMeshProUGUI> pokemonLevel;
    public List<TextMeshProUGUI> pokemonCurrentHealth;
    public List<TextMeshProUGUI> pokemonMaxHealth;
    public List<Slider> pokemonSlider;
    public List<GameObject> activePokemon;
    public int activePokemonCount;
    public int maxPokemonCount, minPokemonCount;
    public bool menuPokemonIsOpen;
    public bool changePokemonOrder;
    public GameObject menu;
    public GameObject pokemonList;
    //zmiana pokemona
    [SerializeField] private int firstSelectedPokemon;
    [SerializeField] private int secondSelectedPokemon;
    [SerializeField] private bool isFirstSelected, isSecondSelected;
    public GameObject pokemonListButton;
    public TextMeshProUGUI pokemonListText;
    [Header("UI")]
    public GameObject PopOutPokemonMenu;
    public TextMeshProUGUI PopOutTextPokemonMenu;
    public GameObject closeButton;
    // Wybieranie pokemona do leczenie
    public bool chosingPokemonToHeal;
    public bool chosenPokemonToHeal;
    public int activePotionSent;
    public void Awake()
    {
        bm = GameObject.Find("Player").GetComponentInChildren<BattleManager>();
    }
    public void Update()
    {
        if(menuPokemonIsOpen == true && chosingPokemonToHeal == false)
        {
            minPokemonCount = 0;
            maxPokemonCount = bm.playerGameObject.Count;
            if (Input.GetKeyUp(KeyCode.DownArrow) && menuPokemonIsOpen ==true && activePokemonCount < maxPokemonCount-1)
            {
                activePokemonCount += 1;
                activePokemon[activePokemonCount].SetActive(true);
                activePokemon[activePokemonCount - 1].SetActive(false);
            }
            if (Input.GetKeyUp(KeyCode.UpArrow) && menuPokemonIsOpen == true && activePokemonCount > 0)
            {
                activePokemonCount -= 1;
                activePokemon[activePokemonCount].SetActive(true);
                activePokemon[activePokemonCount + 1].SetActive(false);
            }
            if(Input.GetKeyUp(KeyCode.KeypadEnter) && menuPokemonIsOpen == true)
            {
                changingPokemon();

            }
        }
        if(changePokemonOrder == true)
        {
            minPokemonCount = 0;
            maxPokemonCount = bm.playerGameObject.Count;
            if (Input.GetKeyUp(KeyCode.DownArrow) && changePokemonOrder == true && activePokemonCount < maxPokemonCount - 1)
            {
                activePokemonCount += 1;
                activePokemon[activePokemonCount].SetActive(true);
                activePokemon[activePokemonCount - 1].SetActive(false);
            }
            if (Input.GetKeyUp(KeyCode.UpArrow) && changePokemonOrder == true && activePokemonCount > 0)
            {
                activePokemonCount -= 1;
                activePokemon[activePokemonCount].SetActive(true);
                activePokemon[activePokemonCount + 1].SetActive(false);
            }
            if (Input.GetKeyUp(KeyCode.KeypadEnter) && isFirstSelected == true)
            {
                secondSelectedPokemon = activePokemonCount;
                chaningPokemon();
            }
            if (Input.GetKeyUp(KeyCode.KeypadEnter) && isFirstSelected == false && chosingPokemonToHeal == false)
            {
                //Wybieranie Pokemona
                firstSelectedPokemon = activePokemonCount;
                pokemonListText.text = "Chose second pokemon to swap";
                isFirstSelected = true;
            }
        }
        if(chosingPokemonToHeal==true)
        {
            if(Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                chosingPokemonToHeal = false;
                changePokemonOrder = false;
                pokemonList.SetActive(false);
                bm.HealingByPotion(activePokemonCount,activePotionSent);
                
            }
        }
    }
    public void showPokemon()
    {
        test.fight = true;
        changePokemonOrder = true;
        isFirstSelected = false;
        if (bm.playerGameObject.Count == 0)
        {
            PopOutPokemonMenu.SetActive(true);
            StartCoroutine(TypeSentences());
            
        }
        else
        {
            pokemonList.SetActive(true);
            menu.SetActive(false);
            int pokemonInBackpack = bm.playerGameObject.Count;
            for (int i = 0; i < bm.playerGameObject.Count; i++)
            {
                pokemonImage[i].sprite = bm.playerGameObject[i].GetComponent<Image>().sprite;
                pokemonName[i].text = bm.playerGameObject[i].GetComponent<Unit>().unitName;
                pokemonLevel[i].text = bm.playerGameObject[i].GetComponent<Unit>().unitLevel.ToString();
                pokemonCurrentHealth[i].text = bm.playerGameObject[i].GetComponent<Unit>().currentHp.ToString();
                pokemonMaxHealth[i].text = bm.playerGameObject[i].GetComponent<Unit>().maxHp.ToString();
                pokemonSlider[i].value =(float) bm.playerGameObject[i].GetComponent<Unit>().currentHp / (float)bm.playerGameObject[i].GetComponent<Unit>().maxHp;
                if(i==0)
                {
                    activePokemon[0].SetActive(true);
                }
            }
            for (int i = pokemonInBackpack; i < 6; i++)
            {
                pokemonName[i].text = "";
                pokemonLevel[i].text = "";
                pokemonCurrentHealth[i].text = "";
                pokemonMaxHealth[i].text = "";
                pokemonImage[i].enabled = false;
            }

        }
    }
    public void showPokemonInFight()
    {
        test.fight = true;
        changePokemonOrder = false  ;
        pokemonListButton.SetActive(false);
        if (bm.playerGameObject.Count == 0)
        {
            PopOutPokemonMenu.SetActive(true);
            StartCoroutine(TypeSentences());

        }
        else
        {
            pokemonList.SetActive(true);
            menu.SetActive(false);
            int pokemonInBackpack = bm.playerGameObject.Count;
            for (int i = 0; i < bm.playerGameObject.Count; i++)
            {
                pokemonImage[i].sprite = bm.playerGameObject[i].GetComponent<Image>().sprite;
                pokemonName[i].text = bm.playerGameObject[i].GetComponent<Unit>().unitName;
                pokemonLevel[i].text = bm.playerGameObject[i].GetComponent<Unit>().unitLevel.ToString();
                pokemonCurrentHealth[i].text = bm.playerGameObject[i].GetComponent<Unit>().currentHp.ToString();
                pokemonMaxHealth[i].text = bm.playerGameObject[i].GetComponent<Unit>().maxHp.ToString();
                pokemonSlider[i].value = (float)bm.playerGameObject[i].GetComponent<Unit>().currentHp / (float)bm.playerGameObject[i].GetComponent<Unit>().maxHp;
                if (i == 0)
                {
                    activePokemon[0].SetActive(true);
                }
            }
            for (int i = pokemonInBackpack; i < 6; i++)
            {
                pokemonName[i].text = "";
                pokemonLevel[i].text = "";
                pokemonCurrentHealth[i].text = "";
                pokemonMaxHealth[i].text = "";
                pokemonImage[i].enabled = false;
            }

        }
    }
    public void ItemsUse(int activePotion)
    {
        activePotionSent = activePotion;
        changePokemonOrder = true;
        
        int pokemonInBackpack = bm.playerGameObject.Count;
        for (int i = 0; i < bm.playerGameObject.Count; i++)
        {
            pokemonImage[i].sprite = bm.playerGameObject[i].GetComponent<Image>().sprite;
            pokemonName[i].text = bm.playerGameObject[i].GetComponent<Unit>().unitName;
            pokemonLevel[i].text = bm.playerGameObject[i].GetComponent<Unit>().unitLevel.ToString();
            pokemonCurrentHealth[i].text = bm.playerGameObject[i].GetComponent<Unit>().currentHp.ToString();
            pokemonMaxHealth[i].text = bm.playerGameObject[i].GetComponent<Unit>().maxHp.ToString();
            pokemonSlider[i].value = (float)bm.playerGameObject[i].GetComponent<Unit>().currentHp / (float)bm.playerGameObject[i].GetComponent<Unit>().maxHp;
            if (i == 0)
            {
                activePokemon[0].SetActive(true);
            }
        }
        for (int i = pokemonInBackpack; i < 6; i++)
        {
            pokemonName[i].text = "";
            pokemonLevel[i].text = "";
            pokemonCurrentHealth[i].text = "";
            pokemonMaxHealth[i].text = "";
            pokemonImage[i].enabled = false;
        }
        chosingPokemonToHeal = true;
    }
    public void changingPokemon()
    {
        menuPokemonIsOpen = false;
        if(bm.playerGameObject[activePokemonCount].GetComponent<Unit>().currentHp <= 0)
        {
            //dorobić coś zeby gracz wiedział, że nie moze wybrac tego pokemona :D
        }
        else
        {
            activePokemon[activePokemonCount].SetActive(false);
            activePokemon[0].SetActive(true);
            pokemonList.SetActive(false);
            bm.changePokemon(activePokemonCount);
            activePokemonCount = 0;
        }
        activePokemon[0].SetActive(true);
        activePokemon[activePokemonCount].SetActive(false);
        activePokemonCount = 0;
    }
    public void hidePokemonList()
    {

        for (int i = 0; i < maxPokemonCount ; i++)
        {
            activePokemon[i].SetActive(false);
        }
        activePokemon[0].SetActive(true);
        activePokemonCount = 0;
        test.fight = false;
        menuPokemonIsOpen = false;
        menu.SetActive(false);
        pokemonList.SetActive(false);
        changePokemonOrder = false;
    }
    IEnumerator TypeSentences()
    {
        string sentece;
        sentece = "You don't have any pokemons yet. Visit Oak to view the pokemon list.";
        foreach (char letter in sentece.ToCharArray())
        {
            PopOutTextPokemonMenu.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
        closeButton.SetActive(true);
        test.fight = false;
    }
    public void hideSenteces()
    {
        menu.SetActive(false);
        PopOutPokemonMenu.SetActive(false);
      
    }
    public void chaningPokemon()
    {
        menuPokemonIsOpen = false;
        changePokemonOrder = false;
        pokemonList.SetActive(false);
        test.fight = false;
        changePokemonOrder = false;
        bm.changeOrderPokemon(firstSelectedPokemon, secondSelectedPokemon);
        pokemonListText.text = " ";

    }
}
