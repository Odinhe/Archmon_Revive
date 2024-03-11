using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGemManager : MonoBehaviour
{
    public static BlueGemManager instance;
    private int blueGem = 2;
    public int BlueGem
    {
        get { return blueGem; }
        set { blueGem = value; }
    }
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
    public void AddGem(int amount)
    {
        blueGem += amount;
        Debug.Log("Blue Gem added: " + amount + ". Total Blue Gem: " + blueGem);
    }

    public void LoseGem(int amount)
    {
        blueGem -= amount;
        Debug.Log("Blue Gem lose: " + amount + ". Total Blue Gem: " + blueGem);
    }
    public static BlueGemManager Instance
    {
        //find the instance, if can't find it, creat a new one
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BlueGemManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<BlueGemManager>();
                    singletonObject.name = typeof(BlueGemManager).ToString() + " (Singleton)";
                }
            }
            return instance;
        }
    }
}
