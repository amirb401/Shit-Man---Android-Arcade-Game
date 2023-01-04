//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sndMan; // with this i can control the soundManager from other scripts.

    private AudioSource audioSrc;
    private AudioClip[] mainMenuSound;
    private AudioClip[] shopSound;
    private AudioClip[] gameSounds;                         // Credits to: https://opengameart.org/content/section-31s-game-music
    private int randomGameSound;
    private AudioClip[] fartSounds;
    private int randomFartSound;
    private AudioClip[] coinSounds;
    private int randomCoinSound;

    // Start is called before the first frame update
    void Start()
    {
        sndMan = this;
        audioSrc = GetComponent<AudioSource>();
        fartSounds = Resources.LoadAll<AudioClip>("FartSounds");
        coinSounds = Resources.LoadAll<AudioClip>("CoinSounds");
        mainMenuSound = Resources.LoadAll<AudioClip>("mainMenuSound");
        gameSounds = Resources.LoadAll<AudioClip>("gameSounds");
        shopSound = Resources.LoadAll<AudioClip>("shopSound");

        Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex == 0)
        {
            PlayMainMenuSound();
        }
        else if ( scene.buildIndex == 1)
        {
            PlayGameSound();
        }
        else if ( scene.buildIndex == 2)
        {
            PlayShopSound();
        }
    }

    public void PlayShopSound()
    {
        audioSrc.PlayOneShot(shopSound[0]); // Play OneTime a random value given.
        Debug.Log("Song playing is: " + shopSound[0].name);
    }

    public void PlayMainMenuSound()
    {
        audioSrc.PlayOneShot(mainMenuSound[0]); // Play OneTime a random value given.
        Debug.Log("Song playing is: " + mainMenuSound[0].name);
        //audioSrc.Play(); // Play OneTime a random value given.
    }
    
    public void PlayGameSound()
    {
        randomGameSound = Random.Range(0, 4); // 0 to 4 (5 excluded)
        Debug.Log("Song playing is: " + gameSounds[randomGameSound].name);
        audioSrc.PlayOneShot(gameSounds[randomGameSound]); // Play OneTime a random value given.
    }

    public void PlayFartSound() // invoked from character OnTriggerEnter2D when hit by poop
    {
        randomFartSound = Random.Range(0, 5); // 0 to 4 (5 excluded)
        audioSrc.PlayOneShot(fartSounds[randomFartSound]); // Play OneTime a random value given.
    }

    public void PlayCoinSound() // invoked from character OnTriggerEnter2D when hit by coin
    {
        randomCoinSound = Random.Range(0, 2); // 0 to 1 (2 excluded)
        audioSrc.PlayOneShot(coinSounds[randomCoinSound]); // Play OneTime a random value given.
    }
}
