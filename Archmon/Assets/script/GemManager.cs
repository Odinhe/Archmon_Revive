using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GemManager instance;
    private int redGem = 2;
    public int RedGem
    {
        get { return redGem; }
        set { redGem = value; }
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
        redGem += amount;
        Debug.Log("Red Gem added: " + amount + ". Total Red Gem: " + redGem);
    }
    public void LoseGem(int amount)
    {
        redGem -= amount;
        Debug.Log("Red Gem Lose: " + amount + ". Total Red Gem: " + redGem);
    }
    public static GemManager Instance
    {
        //find the instance, if can't find it, creat a new one
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GemManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<GemManager>();
                    singletonObject.name = typeof(GemManager).ToString() + " (Singleton)";
                }
            }
            return instance;
        }
    }
}
