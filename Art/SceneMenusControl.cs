using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMenusControl : MonoBehaviour
{

    //private Player mobilePlayer;
    public static bool GameIsPaused = false;
    [SerializeField]
    public GameObject GameOverMenuUI; // game over menu panel
    public GameObject pauseMenuUI; // pause menu panel
    public Character Player;
    public GameObject mainMenuButtonsCanvas;
    public GameObject howToPlayCanvas;
    public GameObject achievementCanvas;

    // GameOver Text
    public Text scoreTotal;
    public Text coinsCollected;
    public Text buffsCollected;


    // Update is called once per frame
    void Update()
    {
        // Character.cs when hit by poop , activate Dead Function.
        // Dead Function will set GameOverMenuUI to true.
        if (Character.IsPlayerAlive == false)
        {
            OpenGameOverMenu();
        }

        //Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    } // end of update


    public void achievementOpen()
    {
        mainMenuButtonsCanvas.SetActive(false);
        achievementCanvas.SetActive(true);
    }
    public void achievementClose()
    {
        achievementCanvas.SetActive(false);
        mainMenuButtonsCanvas.SetActive(true);
    }

    public void howToPlay()
    {
        mainMenuButtonsCanvas.SetActive(false);
        howToPlayCanvas.SetActive(true);
    }
    public void closeHowToPlay()
    {
        howToPlayCanvas.SetActive(false);
        mainMenuButtonsCanvas.SetActive(true);
    }

    // Game Over Menu:
    public void OpenGameOverMenu()
    {
        //Debug.Log("Amir Debug: OpenGameOverMenu Func...");
        GameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
        double totalScoreDouble = Player.coinsCapturedInThisRun + System.Math.Round(Time.timeSinceLevelLoad, 1);
        scoreTotal.text = totalScoreDouble.ToString();
        coinsCollected.text = Player.coinsCapturedInThisRun.ToString();
        buffsCollected.text = Player.buffsCapturedInThisRun.ToString();
        gameData gameData = new gameData();
        if (gameData.highScore < totalScoreDouble)
            gameData.highScore = (float)totalScoreDouble;
        DataManagement.dataManagement.SaveData();
    } // end of Open Game Over Menu Func.
    public void Restart()
    {
        //Debug.Log("Amir Debug: Restart Func...");
        GameOverMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Character.IsPlayerAlive = true;
        DataManagement.dataManagement.SaveData();
        SceneManager.LoadScene(1); // Reload this scene.
    } // end of Restart Func.
    public void LoadMainMenu()
    {
        Debug.Log("Amir Debug: LoadMainMenu Func...");
        Time.timeScale = 1f;
        Character.IsPlayerAlive = true;
        DataManagement.dataManagement.SaveData();
        SceneManager.LoadScene(0); // Loads Main Menu.
    } // end of LoadMainMenu Func.

    public void LoadShop()
    {
        Debug.Log("Amir Debug: LoadShop Func...");
        Time.timeScale = 1f;
        Character.IsPlayerAlive = true;
        DataManagement.dataManagement.SaveData();
        SceneManager.LoadScene(2); // Loads Shop.
    } // end of LoadMainMenu Func.

    // Pause Menu:
    public void Pause()
    {
        //Debug.Log("Amir Debug: Pause Func...");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    } // end of Pause Func.
    public void Resume()
    {
        //Debug.Log("Amir Debug: Resume Func...");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    } // end of Resume Func.
    public void QuitGame()
    {
        //Debug.Log("Amir Debug: QuitGame Func...");
        DataManagement.dataManagement.SaveData();
        Application.Quit();
    } // end of QuitGame Func.

    //Main Menu:
    public void PlaySceneOne()
    {
        //Debug.Log("Amir Debug: PlaySceneOne Func...");
        SceneManager.LoadScene(1);
    } // end of PlaySceneOne.
}
