using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("The pause menu object")]
    public GameObject PauseMenu;
    [Tooltip("The in-game menu object")]
    public GameObject GameMenu;
    [Tooltip("If the game is paused")]
    public bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        
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
}
