using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievDisplay : MonoBehaviour
{
    public AchievScriptable achiev_L1;
    public AchievScriptable achiev_L2;
    public AchievScriptable achiev_L3;
    public bool achiev1, achiev2, achiev3, achiev4;


    public Slider progressBar;
    public Text progressText;
    public Text achievementName;
    public Text achievementDesc;
    public Image achievementImage;

    int max_L1 = 10;
    int max_L2 = 50;
    int max_L3 = 100;

    public bool page1 = true;
    public bool page2, page3, page4;

    private void Update()
    {
        if (achiev1 == true)
            progressBar.value = DataManagement.dataManagement.poopCounter;
        if (achiev2 == true)
            progressBar.value = DataManagement.dataManagement.buffCounter;
    }

    void Start()
    {
        // 1st Pooper
        if (achiev1 == true)
        {
            achievementName.text = achiev_L1.achievementName;
            achievementDesc.text = achiev_L1.achievementDesc;
            progressBar.value = DataManagement.dataManagement.poopCounter;
            progressText.text = DataManagement.dataManagement.poopCounter + "/" + max_L1;

            if (DataManagement.dataManagement.poopCounter >= max_L1)
            {
                progressBar.value = DataManagement.dataManagement.poopCounter;
                progressBar.maxValue = max_L2;
                progressText.text = DataManagement.dataManagement.poopCounter + "/" + max_L2; 

                achievementName.text = achiev_L2.achievementName;
                achievementDesc.text = achiev_L2.achievementDesc;
                achievementImage.sprite = achiev_L1.achievementImage;
            }
            if (DataManagement.dataManagement.poopCounter >= max_L2)
            {
                progressBar.value = DataManagement.dataManagement.poopCounter;
                progressBar.maxValue = max_L3;
                progressText.text = DataManagement.dataManagement.poopCounter + "/" + max_L3;

                achievementName.text = achiev_L3.achievementName;
                achievementDesc.text = achiev_L3.achievementDesc;
                achievementImage.sprite = achiev_L2.achievementImage;
            }
            if (DataManagement.dataManagement.poopCounter >= max_L3)
            {
                achievementImage.sprite = achiev_L3.achievementImage;
                progressBar.gameObject.SetActive(false);
                progressText.text = "ACHIEVED!";
                progressText.transform.localPosition = new Vector3(90, 90, 0);
            }
        }

        // 2nd Buff Maniac
        if (achiev2 == true)
        {
            achievementName.text = achiev_L1.achievementName;
            achievementDesc.text = achiev_L1.achievementDesc;
            progressBar.value = DataManagement.dataManagement.buffCounter;
            progressText.text = DataManagement.dataManagement.buffCounter + "/" + max_L1;

            if (DataManagement.dataManagement.buffCounter >= max_L1)
            {
                progressBar.value = DataManagement.dataManagement.buffCounter;
                progressBar.maxValue = max_L2;
                progressText.text = DataManagement.dataManagement.buffCounter + "/" + max_L2;

                achievementName.text = achiev_L2.achievementName;
                achievementDesc.text = achiev_L2.achievementDesc;
                achievementImage.sprite = achiev_L1.achievementImage;
            }
            if (DataManagement.dataManagement.buffCounter >= max_L2)
            {
                progressBar.value = DataManagement.dataManagement.buffCounter;
                progressBar.maxValue = max_L3;
                progressText.text = DataManagement.dataManagement.buffCounter + "/" + max_L3;

                achievementName.text = achiev_L3.achievementName;
                achievementDesc.text = achiev_L3.achievementDesc;
                achievementImage.sprite = achiev_L2.achievementImage;
            }
            if (DataManagement.dataManagement.buffCounter >= max_L3)
            {
                achievementImage.sprite = achiev_L3.achievementImage;
                progressBar.gameObject.SetActive(false);
                progressText.text = "ACHIEVED!";
                progressText.transform.localPosition = new Vector3(90, 90,0);
            }
        }

        // 3rd Speed runner (Who can hold the longest?)
        if (achiev3 == true)
        {
            achievementName.text = achiev_L1.achievementName;
            achievementDesc.text = achiev_L1.achievementDesc;
            progressBar.value = DataManagement.dataManagement.highScore;
            progressText.text = DataManagement.dataManagement.highScore + "/" + max_L2*2;

            if (DataManagement.dataManagement.highScore > max_L2 * 2)
            {
                achievementImage.sprite = achiev_L1.achievementImage;
                progressBar.gameObject.SetActive(false);
                progressText.text = "ACHIEVED!";
                progressText.transform.localPosition = new Vector3(90, 90, 0);
            }
        }

        // 4th RICH BITCH -> Jeff bezos
        if (achiev4 == true)
        {
            achievementName.text = achiev_L1.achievementName;
            achievementDesc.text = achiev_L1.achievementDesc;
            progressBar.value = DataManagement.dataManagement.coinCounter;
            progressText.text = DataManagement.dataManagement.coinCounter + "/" + max_L3;

            if (DataManagement.dataManagement.coinCounter > 100)
            {
                achievementImage.sprite = achiev_L1.achievementImage;
                progressBar.gameObject.SetActive(false);
                progressText.text = "ACHIEVED!";
                progressText.transform.localPosition = new Vector3(90, 90, 0);
            }
                
        }
    }
}
