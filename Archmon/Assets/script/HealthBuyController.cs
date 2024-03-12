using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuyController : MonoBehaviour
{
    //create the instance of the singleton that we are using
    public static HealthBuyController instance;
    private int healthnum;

    public int Healthnum
    {
        get { return healthnum; }
        set { healthnum = value; }
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
    //add health when run the program
    public void AddHealth(int amount)
    {
        healthnum += amount;
        Debug.Log("Money added: " + amount + ". Total money: " + healthnum);
    }
    //reduce health when run the program
    public void ReduceHealth()
    {
        healthnum = 0;
    }

    public static HealthBuyController Instance
    {
        //find the instance, if can't find it, creat a new one
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HealthBuyController>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<HealthBuyController>();
                    singletonObject.name = typeof(HealthBuyController).ToString() + " (Singleton)";
                }
            }
            return instance;
        }
    }
}
