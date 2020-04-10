using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager_spaceInvaders : MonoBehaviour
{
    [SerializeField] AudioClip rocketFire;
    [SerializeField] AudioClip InvadersMove1;
    [SerializeField] AudioClip InvadersMove2;
    [SerializeField] AudioClip InvadersMove3;
    [SerializeField] AudioClip InvadersMove4;
    [SerializeField] AudioClip playerDeath;
    [SerializeField] AudioClip ufoMoveLeft;
    [SerializeField] AudioClip ufoMoveRight;
    [SerializeField] AudioClip enemyExplosion;
    [SerializeField] AudioSource soundManager;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playSound(string sound)
    {
        switch (sound)
        {
            case "enemyExplosion":
                soundManager.PlayOneShot(enemyExplosion);
                break;
            case "rocketFire":
                soundManager.PlayOneShot(rocketFire);
                break;
            case "InvadersMove1":
                soundManager.PlayOneShot(InvadersMove1);
                break;
            case "InvadersMove2":
                soundManager.PlayOneShot(InvadersMove2);
                break;
            case "InvadersMove3":
                soundManager.PlayOneShot(InvadersMove3);
                break;
            case "InvadersMove4":
                soundManager.PlayOneShot(InvadersMove4);
                break;
            case "ufoMoveLeft":
                soundManager.PlayOneShot(ufoMoveLeft);
                break;
            case "ufoMoveRight":
                soundManager.PlayOneShot(ufoMoveRight);
                break;
            case "playerDeath":
                soundManager.PlayOneShot(playerDeath);
                break;
        }
    }
}
