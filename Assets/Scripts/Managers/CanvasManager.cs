using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button startButton;
    public Button settingsButton;
    public Button quitButton;
    public Button backButton;
    public Button resumeGame;
    public Button returnToMenu;
    public Button muteButton;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject pauseMenu;

    [Header("Slider")]
    public Slider volSlider;

    [Header("Text")]
    public Text volSliderText;
    public Text mutedText;

    void Start()
    {
        if (startButton)
        {
            startButton.onClick.AddListener(() => StartGame());
        }

        if (settingsButton)
        {
            settingsButton.onClick.AddListener(() => ShowSettingsMenu());
        }

        if (quitButton)
        {
            quitButton.onClick.AddListener(() => QuitGame());
        }

        if (backButton)
        {
            backButton.onClick.AddListener(() => ShowMainMenu());
        }

        if (volSlider)
        {
            volSlider.onValueChanged.AddListener((value) => SliderValueChange(value));
            volSliderText.text = volSlider.value.ToString();
        }

        if (resumeGame)
        {
            resumeGame.onClick.AddListener(() => ResumeGame());
        }

        if (returnToMenu)
        {
            returnToMenu.onClick.AddListener(() => LoadMenu());
        }

        if (muteButton)
        {
            muteButton.onClick.AddListener(() => MuteVolume());
            mutedText.text = " ";
        }
    }

    void Update()
    {
        if (pauseMenu)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);

                if (pauseMenu.activeSelf)
                {
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1;
                }
            }    
        }
    }

    void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    void ShowSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    void SliderValueChange(float value)
    {
        if (volSliderText)
        {   
            if (muteButton)
            {
                if (mutedText.text == "X")
                {
                    value = 0;
                }
            }
            
            volSliderText.text = value.ToString();
        }
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("Title");
    }

    void MuteVolume()
    {
        if (mutedText.text == " ")
        {
            mutedText.text = "X";
            volSlider.value = 0;
            volSlider.enabled = false;
        }
        else
        {
            mutedText.text = " ";
            volSlider.enabled = true;
        }
    }
}
