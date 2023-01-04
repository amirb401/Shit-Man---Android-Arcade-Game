using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour
{


    public void addCount(string item)
    {
        if (item == "Poop")
            DataManagement.dataManagement.poopCounter++;
        else if (item == "Buff")
            DataManagement.dataManagement.buffCounter++;
        else if (item == "Coin")
            DataManagement.dataManagement.coinCounter++;

        
    }
}
