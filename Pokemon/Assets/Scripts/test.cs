using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    [Range(1f, 10f)]
    public float speed = 1f;
    public LayerMask StopMovement;
    public Vector3 nextPosition;
    private bool isWalking;
    [SerializeField]
    private GameObject cameraa;
    [SerializeField]
    public  GameObject battleCamera;
    public Animator anim;
    private int fightrandom;
    BattleManager battleManager;
    public GameObject whateva;
    public static bool fight;
    public Transform backIfNoPokemon;
    public static bool moveInKrzak;
    //Skakanie
    public static bool Jump;
    public static string jumpDir;
    public bool alreadyFight;
     BattleManager bm;
    [Header("UI")]
    public GameObject MenuUi;
    void Start()
    {
        nextPosition = transform.position;
        isWalking = false;
        battleCamera.SetActive(false);
        bm = GameObject.Find("Player").GetComponentInChildren<BattleManager>();
    }

    private void Update()
    {
        if (fight == false)
            BasicMovement();
        if (Jump == true)
            Jumping();
        if(Input.GetKeyDown("m"))
        {
            ShowMenuUi();
        }
    }

    private void BasicMovement()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        if (transform.position == nextPosition)
        {
            isWalking = false;
            alreadyFight = false;
            Jump = false;

        }   

        if (horizontalMovement != 0 && !isWalking && Jump == false)
        {
            nextPosition += Vector3.right * horizontalMovement;
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                anim.SetBool("isWalking", true);
                anim.SetFloat("Y2", 0);
                anim.SetFloat("X", 1);
            }
            else if (Input.GetAxisRaw("Horizontal") == -1)
            {
                anim.SetBool("isWalking", true);
                anim.SetFloat("Y2", 0);
                anim.SetFloat("X", -1);
            }
        }
        else if (verticalMovement != 0 && !isWalking && Jump == false)
        {
            nextPosition += Vector3.up * verticalMovement;
            if (Input.GetAxisRaw("Vertical") == 1)
            {
                anim.SetBool("isWalking", true);
                anim.SetFloat("Y2", 1);
                anim.SetFloat("X", 0);
            }
            else if (Input.GetAxisRaw("Vertical") == -1)
            {
                anim.SetBool("isWalking", true);
                anim.SetFloat("Y2", -1);
                anim.SetFloat("X", 0);
            }
        }

        if (nextPosition != transform.position)
        {
            Vector2 dir = nextPosition - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, dir.sqrMagnitude, StopMovement.value);

            if (hit.collider == null)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPosition, Time.deltaTime * speed);
                if (!isWalking) { isWalking = true; }
            }
            else
            {
                nextPosition = transform.position;
            }
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Krzak")
        {
            if (isWalking == true & alreadyFight == false)
            {
                alreadyFight = true;
                fightrandom = Random.Range(0, 100);
                if (fightrandom > 90)
                {
                    EnterKrzak();
                }
            }
        }
       
    }
    public void EnterKrzak()
    {
            alreadyFight = true;
            bm.isWildPokemonFight = true;
            battleCamera.SetActive(true);
            BattleManager.startbattle = true;
    }
    public void isFight()
    { 
        if (fight == true)
            nextPosition += Vector3.zero;
        else
            return;
    }

    public void Jumping()
    {
        Jump = false;
        float verticalMovement = Input.GetAxisRaw("Vertical");
        nextPosition += new Vector3(2,0,0);
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, Time.deltaTime * speed);
        anim.SetFloat("Y2", 0);
        anim.SetFloat("X", 0);
    }
    public void ShowMenuUi()
    {
        MenuUi.SetActive(true);
    }
}

