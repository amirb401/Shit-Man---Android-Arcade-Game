using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    private Rigidbody2D rb;
    private string[] godModeCheat;
    private int charIndex;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Code is "god", user needs to input this in the right order
        godModeCheat = new string[] { "g", "o", "d"};
        charIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.anyKeyDown)
            // Debug.Log("Key Pressed is: " + Input.GetKeyDown()); // Check how to receive which key was pressed.
        fastForward();
        godMode();

    }


    private void godMode()
    {
        // Check if any key is pressed
        if (Input.anyKeyDown)
        {
            // Check if the next key in the code is pressed
            if (Input.GetKeyDown(godModeCheat[charIndex]))
            {
                // Add 1 to index to check the next key in the code
                charIndex++;
            }
            // Wrong key entered, we reset code typing
            else
            {
                charIndex = 0;
            }
        }// end of If

        // If index reaches the length of the cheatCode string, 
        // the entire code was correctly entered
        if (charIndex == godModeCheat.Length)
        {
            // Cheat code successfully inputted!
            // Unlock crazy cheat code stuff
            if (rb.simulated == false)
                rb.simulated = true;
            else
                rb.simulated = false;
            charIndex = 0;
        }
    } // end of GodMode
    private void fastForward()
    {
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            Time.timeScale = 1f;
            Debug.Log("SLOOOOOOOOOOOOW");
        }
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            Time.timeScale = 25f;
            Debug.Log("SPEEEEEEEEEEEED");
        }
    }// end of fastForward




}
