using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public Slider volumeSlider;
    public TMP_InputField width;
    public TMP_InputField height;

    private static bool _isGamePaused;
    
    public bool GetIsGamePaused() {return _isGamePaused;}

    private void Start()
    {
        Resume();
        settingsMenuUI.SetActive(false);
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

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void OpenSettingsMenu()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }

    public void BackSettingsMenu()
    {
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
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

    public void UpdateVolume()
    {
        float newVolume = volumeSlider.value/10;
        foreach (Object a in GameObject.FindObjectsOfType(typeof(AudioSource)))
        {
            a.GetComponent<AudioSource>().volume = newVolume;
        }
    }

    public void ChangeResolution()
    {
        int widthint;
        int heightint;
        if (int.TryParse(width.text, out widthint))
        {
            if (int.TryParse(height.text, out heightint))
            {
                Screen.SetResolution(widthint,heightint,Screen.fullScreen);
            } 
        }
    }
}