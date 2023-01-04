using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    private Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        quitButton = GetComponent<Button>();
        quitButton.onClick.AddListener(QuitGame);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

}