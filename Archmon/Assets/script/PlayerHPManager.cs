using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPManager : MonoBehaviour
{
    //create the instance of the singleton that we are using
    public static PlayerHPManager instance;
    private int hp;

    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }
    //check if there is only one instance at one time
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    //add money when run the program
    public void AddHp(int amount)
    {
        hp += amount;
        Debug.Log("HP added: " + amount + ". Total hp: " + hp);
    }
    //reduce money when run the program
    public void ReduceHp(int amount)
    {
        hp -= amount;
        Debug.Log("Money subtracted: " + amount + ". Total money: " + hp);
    }

    public static PlayerHPManager Instance
    {
        //find the instance, if can't find it, creat a new one
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerHPManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<PlayerHPManager>();
                    singletonObject.name = typeof(PlayerHPManager).ToString() + " (Singleton)";
                }
            }
            return instance;
        }
    }
}
