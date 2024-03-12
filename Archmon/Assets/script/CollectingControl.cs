using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingControl : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BlueGemManager.Instance.AddGem(1);
            Destroy(gameObject); 
        }
    }
}
