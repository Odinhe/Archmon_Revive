using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingControlR : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GemManager.Instance.AddGem(1);
            Destroy(gameObject); // Destroy the object
        }
    }
}

