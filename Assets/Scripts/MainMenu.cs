using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevel;
    void Start()
    {

    }


    void Update()
    {

    }

    public void NewGame()
    {
        SceneManager.LoadScene(firstLevel);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
