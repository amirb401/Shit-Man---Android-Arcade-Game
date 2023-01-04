using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    //public SpriteRenderer sprRen;
    public Sprite zombieSkin, girlZombieSkin, golemSkin, defaultSkin;
    //gameData data = new gameData();


    // Start is called before the first frame update
    void Start()
    {
        //sprRen = GetComponent<SpriteRenderer>();
        zombieSkin = Resources.Load<Sprite>("zombieSkin");
        golemSkin = Resources.Load<Sprite>("golemSkin");
        girlZombieSkin = Resources.Load<Sprite>("girlZombieSkin");
        defaultSkin  = Resources.Load<Sprite>("defaultSkin");
        //sprRen.sprite = girlZombieSkin;
    }

    
    // Method to set sprite
    //public void setSprite(Sprite skinName)
    //{
    //    if (data.currentSkin != skinName)
    //    {
    //        data.currentSkin = skinName;
    //        skinName = sprRen.sprite;
    //    }
    //    DataManagement.dataManagement.SaveData();
    //}
}
