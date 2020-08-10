using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemManager : MonoBehaviour
{
    public List<TextMeshProUGUI> itemName;
    public List<GameObject> slotsForItems;
    public List<GameObject> Indicator;
    public int currentIndexItem;
    public int maxIndexItem;
    public int minIndexItem;
    public GameObject menuItemList;

    BackpackSystem backpack;
    public Sprite itemImage;
    public GameObject itemsImageToSend;
    public TextMeshProUGUI loreItem;
    public void Start()
    {
        currentIndexItem = 0;
        minIndexItem = 0;
        backpack = GameObject.Find("Backpack").GetComponent<BackpackSystem>();
    }
    public void Update()
    {
        maxIndexItem = backpack.item.Count-1;

        if (Input.GetKeyDown("i"))
        {
            menuItemList.SetActive(true);
            for (int i=0; i < backpack.item.Count; i++)
            {
                itemName[i].text = backpack.item[i].GetComponent<Item>().itemName;
               
            }
            itemImage = backpack.item[currentIndexItem].GetComponent<SpriteRenderer>().sprite;
            itemsImageToSend.GetComponent<Image>().sprite = itemImage;

            loreItem.text = backpack.item[currentIndexItem].GetComponent<Item>().itemLore;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) && currentIndexItem < maxIndexItem)
        {
            Indicator[currentIndexItem].SetActive(false);
            Indicator[currentIndexItem + 1].SetActive(true);
            currentIndexItem += 1;
            ShowItems();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && currentIndexItem > minIndexItem)
        {
            Indicator[currentIndexItem].SetActive(false);
            Indicator[currentIndexItem - 1].SetActive(true);
            currentIndexItem -= 1;
            ShowItems();
        }    
    }

    public void ShowItems()
    {
        itemImage = backpack.item[currentIndexItem].GetComponent<SpriteRenderer>().sprite;
        itemsImageToSend.GetComponent<Image>().sprite = itemImage;

        loreItem.text = backpack.item[currentIndexItem].GetComponent<Item>().itemLore;

    }
}
