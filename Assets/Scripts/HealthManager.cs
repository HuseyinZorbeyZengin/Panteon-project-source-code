using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    #region Variables
    public static HealthManager instance;

    public int currentHealth, maxHealth;

    public int soundToPlay;
    #endregion


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentHealth = maxHealth;
    }


    void Update()
    {

    }
    #region Hurting Player
    public void Hurt()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            AudioManager.instance.PlaySFX(soundToPlay);

            GameManager.instance.Respawn();
        }
        else
        {
            PlayerController.instance.KnockBack();
        }
    }
    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}
#endregion