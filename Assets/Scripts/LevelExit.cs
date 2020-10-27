using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public Animator anim;
    void Start()
    {

    }


    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetTrigger("Hit");

            StartCoroutine(GameManager.instance.LevelEndCo());
        }
    }
}
