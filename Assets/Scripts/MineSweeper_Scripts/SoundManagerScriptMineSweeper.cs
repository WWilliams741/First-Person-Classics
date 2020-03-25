using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScriptMineSweeper : MonoBehaviour
{

    [SerializeField] AudioClip left_click_sound;
    [SerializeField] AudioClip kaboom;
    [SerializeField] AudioSource audioPlayer;

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
            case "left_click":
                audioPlayer.PlayOneShot(left_click_sound);
                break;
            case "kaboom":
                audioPlayer.PlayOneShot(kaboom);
                break;
        }
    }
}
