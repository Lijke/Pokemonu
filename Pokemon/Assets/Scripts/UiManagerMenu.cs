using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UiManagerMenu : MonoBehaviour
{
    public GameObject Menu;
    BattleManager bm;
    public GameObject ItemsList;
    public List<Slider> slider;
    [Header("Items")]
    public string ItemName;
    public string ItemLore;
    public List<TextMeshProUGUI> itemNameToSend;
    public List<string> itemLoreToSend;
    public List<TextMeshProUGUI> itemCount;
    [Header("Items ui")]
    public List<TextMeshProUGUI> itemName;
    public List<GameObject> slotsForItems;
    public List<GameObject> Indicator;
    public int currentIndexItem;
    public int maxIndexItem;
    public int minIndexItem;
    public GameObject menuItemList;
    public bool isOpenInFight;
    BackpackSystem backpack;
    public Sprite itemImage;
    public GameObject itemsImageToSend;
    public TextMeshProUGUI loreItem;

    public bool isMenuItemOpen;
    [Header("Uzywanie itemow")]
    public GameObject pokemonList;
    MenuUiPokemons menuPokemons;
    public GameObject button;
    private void Awake()
    {
        bm = GameObject.Find("Player").GetComponentInChildren<BattleManager>();
        backpack = GameObject.Find("Backpack").GetComponent<BackpackSystem>();
        currentIndexItem = 0;
        minIndexItem = 0;
        backpack = GameObject.Find("Backpack").GetComponent<BackpackSystem>();
        menuPokemons = GameObject.Find("MenuUiPokemon").GetComponent<MenuUiPokemons>();
    }

    private void Update()
    {
        maxIndexItem = backpack.item.Count - 1;

        if (Input.GetKeyDown("i"))
        {
            menuItemList.SetActive(true);
            FirstItemOpen();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && currentIndexItem < maxIndexItem && isMenuItemOpen ==true)
        {
            Indicator[currentIndexItem].SetActive(false);
            Indicator[currentIndexItem + 1].SetActive(true);
            currentIndexItem += 1;
            ShowItems();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && currentIndexItem > minIndexItem && isMenuItemOpen == true )
        {
            Indicator[currentIndexItem].SetActive(false);
            Indicator[currentIndexItem - 1].SetActive(true);
            currentIndexItem -= 1;
            ShowItems();
        }
        else if (Input.GetKeyUp(KeyCode.KeypadEnter) && isMenuItemOpen == true && isOpenInFight== true)
        {
            ItemsList.SetActive(false);
            pokemonList.SetActive(true);
            isMenuItemOpen = false;
            menuPokemons.ItemsUse(currentIndexItem);
        }
    }
    public void ShowItems()
    {
        Menu.SetActive(false);
        menuItemList.SetActive(true);
        itemName[currentIndexItem].text = backpack.item[currentIndexItem].GetComponent<Item>().itemName;
        itemImage = backpack.item[currentIndexItem].GetComponent<SpriteRenderer>().sprite;
        itemsImageToSend.GetComponent<Image>().sprite = itemImage;
        loreItem.text = backpack.item[currentIndexItem].GetComponent<Item>().itemLore;
        isMenuItemOpen = true;
    }
    public void HideItems()
    {
        isMenuItemOpen = false;
        Menu.SetActive(false);
        menuItemList.SetActive(false);
    }
    public void FirstItemOpen()
    {
        Menu.SetActive(false);
        menuItemList.SetActive(true);
        isOpenInFight = false;
        for (int i = 0; i < backpack.item.Count; i++)
        {
            itemName[i].text = backpack.item[i].GetComponent<Item>().itemName;
            itemCount[i].text = backpack.item[i].GetComponent<Item>().itemCount.ToString();
        }
        itemImage = backpack.item[currentIndexItem].GetComponent<SpriteRenderer>().sprite;
        itemsImageToSend.GetComponent<Image>().sprite = itemImage;
        loreItem.text = backpack.item[currentIndexItem].GetComponent<Item>().itemLore;
        isMenuItemOpen = true;
    }
    public void ItemOpenWHenFight()
    {
        isOpenInFight = true;
        Menu.SetActive(false);
        menuItemList.SetActive(true);
        button.SetActive(false);
        for (int i = 0; i < backpack.item.Count; i++)
        {
            itemName[i].text = backpack.item[i].GetComponent<Item>().itemName;
            itemCount[i].text = backpack.item[i].GetComponent<Item>().itemCount.ToString();
        }
        itemImage = backpack.item[currentIndexItem].GetComponent<SpriteRenderer>().sprite;
        itemsImageToSend.GetComponent<Image>().sprite = itemImage;
        loreItem.text = backpack.item[currentIndexItem].GetComponent<Item>().itemLore;
        isMenuItemOpen = true;
    }
}
