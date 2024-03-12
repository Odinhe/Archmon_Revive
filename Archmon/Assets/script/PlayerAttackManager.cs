using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    //create the instance of the singleton that we are using
    public static PlayerAttackManager instance;
    private int attack = 1;

    public int Attack
    {
        get { return attack; }
        set { attack = value; }
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
    //add attack when run the program
    public void AddAttack(int amount)
    {
        attack += amount;
        Debug.Log("Money added: " + amount + ". Total money: " + attack);
    }
    //reduce attack when run the program
    public void ReduceAttack(int amount)
    {
        attack -= amount;
        Debug.Log("Money subtracted: " + amount + ". Total money: " + attack);
    }

    public static PlayerAttackManager Instance
    {
        //find the instance, if can't find it, creat a new one
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerAttackManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<PlayerAttackManager>();
                    singletonObject.name = typeof(PlayerAttackManager).ToString() + " (Singleton)";
                }
            }
            return instance;
        }
    }
}

