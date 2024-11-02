using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            Time.timeScale = isPaused ? 0 : 1;

            if (pauseMenuUI != null)
            {
                pauseMenuUI.SetActive(isPaused);
            }
        }
    }

    
}
