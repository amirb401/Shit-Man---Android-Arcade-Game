using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsKiller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
            Destroy(collision.gameObject); // If you got hit by something, Kill.
        
    }
}
