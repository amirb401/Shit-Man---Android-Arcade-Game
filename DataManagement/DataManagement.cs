using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManagement : MonoBehaviour
{
    public static DataManagement dataManagement;
    public float highScore;
    public int coinsCollected; // total coins a user have.
    public int poopCounter;
    public int coinCounter;
    public int buffCounter;

    void Awake()
    {
        if (dataManagement == null)
        {
            DontDestroyOnLoad(gameObject);
            dataManagement = this;
        }
        else if (dataManagement != this)
            Destroy(gameObject);
    }


    public void SaveData()
    {
        BinaryFormatter binForm = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameInfo.dat");
        gameData data = new gameData();
        data.poopCounter = poopCounter;
        data.coinCounter = coinCounter;
        data.buffCounter = buffCounter;
        data.highScore = highScore;
        data.coinsCollected = coinsCollected;
        binForm.Serialize(file, data);
        file.Close();
    }

    public void LoadData()
    {
        if (File.Exists (Application.persistentDataPath + "/gameInfo.dat"))
        {
            BinaryFormatter binForm = new BinaryFormatter();
            FileStream file = File.Open (Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);
            gameData data =  (gameData)binForm.Deserialize (file);
            file.Close();
            highScore = data.highScore;
            coinsCollected = data.coinsCollected;
            poopCounter = data.poopCounter;
            coinCounter = data.coinCounter;
            buffCounter = data.buffCounter;
            Debug.Log("HighScore: " + highScore);
            Debug.Log("poops collected: " + poopCounter);
            Debug.Log("coins collected: " + coinCounter);
            Debug.Log("buffs collected: " + buffCounter);
            Debug.Log("coinsCollected collected: " + coinsCollected);

        }
        
    }

}


[Serializable]
class gameData
{
    public float highScore;
    public int coinsCollected;
    public int poopCounter;
    public int coinCounter;
    //public int timeCounter; 
    public int buffCounter;
}