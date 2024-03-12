using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBuyControl : MonoBehaviour
{
        //create the instance of the singleton that we are using
        public static GemBuyControl instance;
        private int gemnum;

        public int Gemnum
    {
            get { return gemnum; }
            set { gemnum = value; }
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
        //add gem when run the program
        public void AddGem(int amount)
        {
            gemnum += amount;
            Debug.Log("Money added: " + amount + ". Total money: " + gemnum);
        }
        //reduce gem when run the program
        public void ReduceGem()
        {
            gemnum = 0;
        }

        public static GemBuyControl Instance
        {
            //find the instance, if can't find it, creat a new one
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GemBuyControl>();
                    if (instance == null)
                    {
                        GameObject singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<GemBuyControl>();
                        singletonObject.name = typeof(GemBuyControl).ToString() + " (Singleton)";
                    }
                }
                return instance;
            }
        }
    }
