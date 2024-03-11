using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    //create the instance of the singleton that we are using
    public static MoneyManager instance;
    private int money = 100;

    public int Money
    {
        get { return money; }
        set { money = value; }
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
    public void AddMoney(int amount)
    {
        money += amount;
        Debug.Log("Money added: " + amount + ". Total money: " + money);
    }
    //reduce money when run the program
    public void ReduceMoney(int amount)
    {
        money -= amount;
        Debug.Log("Money subtracted: " + amount + ". Total money: " + money);
    }

    public static MoneyManager Instance
    {
        //find the instance, if can't find it, creat a new one
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MoneyManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<MoneyManager>();
                    singletonObject.name = typeof(MoneyManager).ToString() + " (Singleton)";
                }
            }
            return instance;
        }
    }
}
