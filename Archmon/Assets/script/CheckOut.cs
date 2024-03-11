using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOut : MonoBehaviour
{
    public int numHealth;
    public int MoneyA;
    // Start is called before the first frame update
    public void Update()
    {
      numHealth = HealthBuyController.Instance.Healthnum;
      MoneyA = MoneyManager.Instance.Money;
    }
   
    public void checkOut()
    {
        if(numHealth * 20 > MoneyA)
        {
            Debug.Log("Not enough money");
            HealthBuyController.Instance.ReduceHealth();
        }
        if (numHealth * 20 < MoneyA)
        {
            Debug.Log("Good Buy!");
            MoneyManager.Instance.ReduceMoney(numHealth * 20);
            PlayerHPManager.Instance.AddHp(numHealth * 40);
            HealthBuyController.Instance.ReduceHealth();
        }

    }
}
