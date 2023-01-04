using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameInit : MonoBehaviour
{
    public static GameInit gameInit;

    [SerializeField]
    public Text CoinsUI; // UI Text object from Hiearchy
    public GameObject coinPrefab; // UI Text object from Hiearchy
    

    void Start()
    {
        DataManagement.dataManagement.LoadData();
        DontDestroyOnLoad(CoinsUI);
        Vector2 coinPrefabLoc = new Vector2( -6.7f, -3f);
        
        // always in offset with the whole scene. should be scaled to world coordinates or whatever that will make this scale properly with the rest of the scene
        //Instantiate(coinPrefab, coinPrefabLoc, Quaternion.identity);
    }


    void Update()
    {
        CoinsUI.text = DataManagement.dataManagement.coinsCollected.ToString();
    }



}
