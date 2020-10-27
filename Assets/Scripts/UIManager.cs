using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;



    #region Variables
    public Image blackScreen;
    public float fadeSpeed = 2f;
    public bool fadeToBlack, fadeFromBlack;

    public GameObject pauseScreen, optionsScreen;

    public Slider musicVolSlider, sfxVolSlider;

    public string mainMenu;
    #endregion
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }


    void Update()
    {
        #region Fade in and out BlackScreen
        if (fadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }

        if (fadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {
                fadeFromBlack = false;
            }
        }
        #endregion
    }
    public void Resume()
    {
        GameManager.instance.PauseUnpause();
    }
    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }
    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
    public void SetMusicLevel()
    {
        AudioManager.instance.SetMusicLevel();
    }
    public void SetSFXLevel()
    {
        AudioManager.instance.SetSFXLevel();
    }
}
