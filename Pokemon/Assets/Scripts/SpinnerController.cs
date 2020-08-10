using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinnerController : MonoBehaviour
{
    public List<GameObject> SpinnerPokemon;
    public int Direction;
    private bool changeDirection = true;
    [Header("Ui Element")]
    public string Name;
    public Animator anim;
    public RaycastHit2D hit;
    public int offsetX;
    public int offsetY;
    test Test;
    BattleManager bm;
    public bool spinnerFight;
    [Header("Dialogue Trigger")]
    public GameObject dialogueUI;
    public Text text;
    public Dialogue dialogue;
    public void Start()
    {
        Test = GameObject.Find("Player").GetComponent<test>();
        bm = GameObject.Find("Player").GetComponentInChildren<BattleManager>();
    }
    public void Update()
    {
        if (changeDirection == true && spinnerFight == false )
        {
            changeDirection = false;
            StartCoroutine(SpinnerRotation());

           
        }
       
    }

    IEnumerator SpinnerRotation()
    {
        Direction = Random.Range(0,4);
        switch(Direction)
        {
            case 0:

                anim.SetFloat("DirX", 1);
                anim.SetFloat("DirY", 0);
                hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x+offsetX, transform.position.y));
                if (hit)
                {
                    StartFight();
                }
                break;
            case 1:

                anim.SetFloat("DirX", -1);
                anim.SetFloat("DirY", 0);
                hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x-offsetX, transform.position.y));
                if (hit)
                {
                    StartFight();
                }
                break;
            case 2:

                anim.SetFloat("DirX", 0);
                anim.SetFloat("DirY", 1);
                hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y + offsetY));
                if (hit)
                {
                    StartFight();
                }
                break;
            case 3:

                anim.SetFloat("DirX", 0);
                anim.SetFloat("DirY", -1);
                hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y - offsetY));
                if (hit)
                {
                    StartFight();
                }
                break;
        }

        yield return new WaitForSeconds(0.3f);
        changeDirection = true;
    }
    public void StartFight()
    {
       
        spinnerFight = true;
        for (int i = 0; i<SpinnerPokemon.Count; i++)
        {
            bm.enemyPrefab.Add(SpinnerPokemon[i]);
        }
        Test.EnterKrzak();
        
    }
}
