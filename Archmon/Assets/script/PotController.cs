using System.Collections;
using System.Collections.Generic;
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

    //set the list of the sprite so later The sprite can change
    public List<Sprite> spr;

    //set different text to change
    [SerializeField] private Text Blue;
    [SerializeField] private Text Red;
    [SerializeField] private Text Total;
    [SerializeField] public Text Warning;

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
        //get the collider and rigidbody before the game start
        rb = GetComponent<Rigidbody2D>();
        potCollider = GetComponent<Collider2D>();
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

        //if there are five or more elements in the pot, player can start making the gem by right click
        if (numBot >= 1 && Input.GetMouseButtonDown(0))
        {
            Debug.Log("good");
            Total.text = "Total: " + numBot;
            Red.text = "Red: " + RedNum;
            Blue.text = "Blue: " + BlueNum;
            Warning.text = "Pot is not full!";
            RedNum = 0;
            BlueNum = 0;
            numBot = 0;

            //set the gem span position, which is disgned
            Vector3 spawnPosition = new Vector3(-0.07f, -2.48f, -1f);
            //besed on the different result of the color combination, the gem that will be genrated will be different 
            switch (Potion)
            {
                case "ArmorPotion":
                    Instantiate(ArmorPotion, spawnPosition, Quaternion.identity);
                    break;
                case "FirePotion":
                    Instantiate(FirePotion, spawnPosition, Quaternion.identity);
                    break;
                case "HealthPotion":
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