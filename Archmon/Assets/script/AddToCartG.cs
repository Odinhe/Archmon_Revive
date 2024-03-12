using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToCartG : MonoBehaviour
{
    public void AddTo0Cart()
    {
        GemBuyControl.Instance.AddGem(1);
    }
}
