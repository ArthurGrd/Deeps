using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    
    private static bool _isGamePaused;
    
    public bool GetIsGamePaused() {return _isGamePaused;}

    private void Start()
    {
        Resume();
    }

    public void UpdatePauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    
    

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.18f;
        _isGamePaused = true;
    }

    private void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        _isGamePaused = false;
    }
}