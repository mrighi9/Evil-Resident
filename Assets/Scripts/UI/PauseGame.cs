using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public bool gamePaused = false;
    public AudioSource areaBGM;
    public GameObject pauseScreen;
    public Button quitButton;
    public Button restartButton;

    void Start()
    {
        if (quitButton != null)
            quitButton.onClick.AddListener(GoToMainMenu);

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause") && !GlobalControl.disableInv)
        {
            if (!gamePaused)
            {
                GlobalControl.disableInv = true;
                Time.timeScale = 0;
                gamePaused = true;
                areaBGM.Pause();
                pauseScreen.SetActive(true);
            }
            else
            {
                pauseScreen.SetActive(false);
                areaBGM.UnPause();
                gamePaused = false;
                Time.timeScale = 1;
                GlobalControl.disableInv = false;
            }
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TelaInicial");
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Hall");
    }
}
