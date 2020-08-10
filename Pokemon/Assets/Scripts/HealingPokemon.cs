using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealingPokemon : MonoBehaviour
{
    [SerializeField]
    BattleManager battlemanager;
    public GameObject dialogueUI;
    public Text text;
    public Dialogue dialogue;

    public void Start()
    {
        battlemanager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(battlemanager.playerPrefab.Count ==0)
            {
                text.text = "Nie masz żadnych pokemenów! Wróc później!";
                dialogueUI.SetActive(false);
            }
            else
            {
                dialogueUI.SetActive(true);
                battlemanager.HealingPokemon();
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            
        }
    }
}
