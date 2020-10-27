using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject cpOn, cpOff;

    public int soundToPlay;

    void Start()
    {

    }
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        #region CheckPoint System
        if (other.tag == "Player")
        {
            GameManager.instance.SetSpawnPoint(transform.position);

            //Deactivete previous checkpoints when activate new one
            CheckPoint[] allCP = FindObjectsOfType<CheckPoint>();

            for (int i = 0; i < allCP.Length; i++)
            {
                allCP[i].cpOff.SetActive(true);
                allCP[i].cpOn.SetActive(false);
            }

            //Showing the checkpoint is activated
            cpOff.SetActive(false);
            cpOn.SetActive(true);
            
        }
        #endregion
        AudioManager.instance.PlaySFX(soundToPlay);
    }
}
