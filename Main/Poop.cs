using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 offsetLocation = new Vector3(collision.transform.position.x + 2f, 6f, collision.transform.position.z);
        if (collision.gameObject.CompareTag("Coin") || collision.gameObject.CompareTag("BigCoin") || collision.gameObject.CompareTag("tissuePaper") || collision.gameObject.CompareTag("poopStick"))
        {
            collision.transform.position = (offsetLocation);
            Debug.Log("offsetted a buff touching a poop");
        }
    }


}
