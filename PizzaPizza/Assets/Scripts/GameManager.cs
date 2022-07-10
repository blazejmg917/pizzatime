using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Tooltip("The pause menu object")]
    public GameObject PauseMenu;
    [Tooltip("The in-game menu object")]
    public GameObject GameMenu;
    [Tooltip("If the game is paused")]
    public bool paused = false;
    [Tooltip("The instance of this game manager")]
    public static GameManager instance;
    [Tooltip("the id of the main menu scene. should likely be 0")]
    public int mainMenuId = 0;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance && instance != this)
        {
            Destroy(this);
        }
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //pause or unpause the game
    public void Pause()
    {
        paused = !paused;
        GameMenu.SetActive(!paused);
        PauseMenu.SetActive(paused);
    }

    public bool IsPaused()
    {
        return paused;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuId);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
