using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsControl : MonoBehaviour
{

    public int smallCoinValue = 1;
    public int bigCoinValue = 5;
    Controller control;

    // Start is called before the first frame update
    public int addCoin(string prefabName)
    {
        if (prefabName == "smallCoin")
        {
            return smallCoinValue;
        }
        else if (prefabName == "bigCoin")
        {
            return bigCoinValue;
        }

        return 0;
    }

}
