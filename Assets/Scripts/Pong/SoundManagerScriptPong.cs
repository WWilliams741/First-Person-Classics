using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScriptPong : MonoBehaviour
{

    [SerializeField] AudioClip ballBounce;
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
        switch (sound) {
            case "bounce":
                soundManager.PlayOneShot(ballBounce);
                break;
        }
    }
}
