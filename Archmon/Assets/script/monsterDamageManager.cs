using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterDamageManager : MonoBehaviour
{
    //create the instance of the singleton that we are using
    public static monsterDamageManager instance;
    private int damageT;

    public int DamageT
    {
        get { return damageT; }
        set { damageT = value; }
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
    //add damage when run the program
    public void AddDamage(int amount)
    {
        damageT += amount;
        Debug.Log("Damage added: " + amount + ". Total damage: " + damageT);
    }
    //reduce damage when run the program
    public void resetDamage()
    {
        damageT = 0;
    }

    public static monsterDamageManager Instance
    {
        //find the instance, if can't find it, creat a new one
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<monsterDamageManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<monsterDamageManager>();
                    singletonObject.name = typeof(monsterDamageManager).ToString() + " (Singleton)";
                }
            }
            return instance;
        }
    }
}


