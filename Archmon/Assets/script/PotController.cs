using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Collider2D potCollider;

    //setup the tree numbers that will be used in the game
    public int RedNum = 0;
    public int BlueNum = 0;
    public int numBot = 0;
    public string Potion = "none";

    public int RedGemNum;
    public int BlueGemNum;
    public int health;
    public int attack;
    public int money;

    //set the list of the sprite so later The sprite can change
    public List<Sprite> spr;

    public TextMeshProUGUI Red;
    public TextMeshProUGUI Blue;
    public TextMeshProUGUI Total;
    public TextMeshProUGUI Warning;
    public TextMeshProUGUI Health;
    public TextMeshProUGUI Attack;
    public TextMeshProUGUI Money;
    //set up the game object so later can genrate when ger result
    public GameObject ArmorPotion;
    public GameObject FirePotion;
    public GameObject HealthPotion;
    public GameObject BlueGem;
    public GameObject RedGem;


    void Start()
    {
        RedGemNum = GemManager.Instance.RedGem;
        BlueGemNum = BlueGemManager.Instance.BlueGem;
        health = PlayerHPManager.Instance.HP;
        //get the collider and rigidbody before the game start
        rb = GetComponent<Rigidbody2D>();
        potCollider = GetComponent<Collider2D>();
        //check the gemManagers
        if (GemManager.Instance == null)
        {
            Debug.LogError("GemManager is null");
        }

        if (BlueGemManager.Instance == null)
        {
            Debug.LogError("BlueGemManager is null");
        }


    }

    // Update is called once per frame
    void Update()
    {
        //set up the text
        Total.text = "Total Gem in Pot: " + numBot;
        Red.text = "Red Gem: " + RedNum;
        Blue.text = "Blue Gem: " + BlueNum;
        Attack.text = "Your Attack Point: " + attack;
        Health.text = "Your HP: " + health;
        Money.text = "Your Money: " + money;
        Warning.text = "Pot is not full!";
        health = PlayerHPManager.Instance.HP;
        attack = PlayerAttackManager.Instance.Attack;
        money = MoneyManager.Instance.Money;

        //genrate the gem before the game start based on how many gems player hold
        if (BlueGemNum > 0)
        {
            Instantiate(BlueGem, new Vector2(6, 3), Quaternion.identity);
            BlueGemNum--;
        }

        if (RedGemNum > 0)
        {
            Instantiate(RedGem, new Vector2(-6, 3), Quaternion.identity);
            RedGemNum--;
        }

        //this is the main program that allow the game to calculate what kind of gem player is making, based on the different color combination, the output will be different
        if (RedNum < BlueNum)
        {
            Potion = "ArmorPotion";
        }
        if (RedNum > BlueNum)
        {
            Potion = "FirePotion";
        }
        if (RedNum == BlueNum)
        {
            Potion = "HealthPotion";
        }

        //if there are one elements in the pot, player can start making the gem by right click
        if (numBot >= 1 && Input.GetMouseButtonDown(0))
        {
            Debug.Log("good");
            RedNum = 0;
            BlueNum = 0;
            numBot = 0;

            //set the gem span position, which is disgned
            Vector3 spawnPosition = new Vector3(-0.07f, -2.48f, -3f);
            //besed on the different result of the color combination, the gem that will be genrated will be different 
            switch (Potion)
            {
                case "ArmorPotion":
                    MoneyManager.Instance.AddMoney(50);
                    Instantiate(ArmorPotion, spawnPosition, Quaternion.identity);
                    break;
                case "FirePotion":
                    PlayerAttackManager.Instance.AddAttack(1);
                    Instantiate(FirePotion, spawnPosition, Quaternion.identity);
                    break;
                case "HealthPotion":
                    PlayerHPManager.Instance.AddHp(2);
                    Instantiate(HealthPotion, spawnPosition, Quaternion.identity);
                    break;
            }
        }
        //based on how many elements where put into the pod, the sprite of the pot will be different
        switch (numBot)
        {
            case 0:
                GetComponent<SpriteRenderer>().sprite = spr[0];
                break;
            case 5:
                GetComponent<SpriteRenderer>().sprite = spr[1];
                break;
            case 10:
                GetComponent<SpriteRenderer>().sprite = spr[2];
                break;
        }
        //if there is 10 elements in the pod, players can't put more elements into the pod
        if (numBot == 10)
        {
            potCollider.isTrigger = false;
        }
        //the word will display if the pot is full or not

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //when player put the gem into the pod, react based on the color that player put into the pod, then destory the gem, calculate how many gems where inside the pod that shares the same color, and calculate the total amount of the gems
        if (collision.gameObject.CompareTag("Red"))
        {
            RedNum++;
            numBot++;
            GemManager.Instance.LoseGem(1);
            Red.text = "Red: " + RedNum;
            Total.text = "Total: " + numBot;
            Destroy(collision.gameObject);
            Debug.Log(RedNum);
            
        }

        if (collision.gameObject.CompareTag("Blue"))
        {
            
            BlueNum++;
            numBot++;
            BlueGemManager.Instance.LoseGem(1);
            Blue.text = "Blue: " + BlueNum;
            Total.text = "Total: " + numBot;
            Destroy(collision.gameObject);
            Debug.Log(BlueNum);
        }
        string potMessage = (numBot <= 9) ? "Pot is not full!" : "Pot is full!";
        Debug.Log(potMessage);
        Warning.text = potMessage;
    }
}