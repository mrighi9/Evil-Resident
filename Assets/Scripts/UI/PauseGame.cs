using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public bool gamePaused = false;
    public AudioSource areaBGM;
    public GameObject pauseScreen;

    void Update()
    {
        // Verifica se o inventário está fechado para permitir o pause
        if (Input.GetButtonDown("Pause") && !GlobalControl.disableInv)
        {
            if (!gamePaused)
            {
                GlobalControl.disableInv = true; // Desativa o inventário enquanto o jogo está pausado
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
                GlobalControl.disableInv = false; // Reativa o inventário quando o pause é desativado
            }
        }
    }
}
