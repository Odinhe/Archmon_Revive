using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckOut : MonoBehaviour
{
    public int numHealth;
    public int MoneyA;
    public int numGem;
    public TextMeshProUGUI Red;
    public TextMeshProUGUI Health;
    public TextMeshProUGUI warn;
    // Start is called before the first frame update
    public void Update()
    {

      numHealth = HealthBuyController.Instance.Healthnum;
      MoneyA = MoneyManager.Instance.Money;
      numGem = GemBuyControl.Instance.Gemnum;
      Red.text = "Number in cart: " + numGem;
      Health.text = "Number in cart: " + numHealth;
    }
   
    public void checkOut()
    {
        //check if player have enough money
        if((numHealth * 20) + (numGem * 30)> MoneyA)
        {
            warn.text =  "You don't have enough money!";
            HealthBuyController.Instance.ReduceHealth();
            GemBuyControl.Instance.ReduceGem();
        }
        if (numHealth * 20 < MoneyA)
        {
            warn.text = "Good Buy!";
            MoneyManager.Instance.ReduceMoney(numHealth * 20);
            MoneyManager.Instance.ReduceMoney(numGem * 30);
            PlayerHPManager.Instance.AddHp(numHealth * 2);
            GemManager.Instance.AddGem(numGem);
            HealthBuyController.Instance.ReduceHealth();
            GemBuyControl.Instance.ReduceGem();
        }

    }
}
