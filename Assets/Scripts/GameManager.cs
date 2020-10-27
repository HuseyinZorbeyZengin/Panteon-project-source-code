using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Vector3 respawnPosition;

    public GameObject deathEffect;

    public int levelEndMusic = 7;

    public string levelToLoad;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //Hiding and locking mouse cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        respawnPosition = PlayerController.instance.transform.position;

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCo());
    }
    public IEnumerator RespawnCo()
    {
        #region Respawn Player and Fixing camera shaking while spawning
        PlayerController.instance.gameObject.SetActive(false);

        CameraController.instance.theCMBrain.enabled = false;

        UIManager.instance.fadeToBlack = true;

        Instantiate(deathEffect, PlayerController.instance.transform.position + new Vector3(0f, 0.5f, 0f), PlayerController.instance.transform.rotation);

        yield return new WaitForSeconds(3f);

        HealthManager.instance.ResetHealth();

        UIManager.instance.fadeFromBlack = true;

        PlayerController.instance.transform.position = respawnPosition;

        CameraController.instance.theCMBrain.enabled = true;


        PlayerController.instance.gameObject.SetActive(true);
        #endregion
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
    }
    #region Pause Screen
    public void PauseUnpause()
    {
        if (UIManager.instance.pauseScreen.activeInHierarchy)
        {
            UIManager.instance.pauseScreen.SetActive(false);
            Time.timeScale = 1f;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UIManager.instance.pauseScreen.SetActive(true);
            UIManager.instance.CloseOptions();
            Time.timeScale = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            #endregion
        }
    }
    #region Loading Other levels
    public IEnumerator LevelEndCo()
    {
        AudioManager.instance.PlayMusic(levelEndMusic);
        PlayerController.instance.stopMove = true;

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelToLoad);


    }
    #endregion
}
