using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderManager_Script : MonoBehaviour
{

    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject sprite1;
    [SerializeField] private GameObject sprite2;
    [SerializeField] private soundManager_spaceInvaders soundManager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(19))
        {
            explosion.SetActive(true);
            explosion.transform.position = transform.position;
            soundManager.playSound("enemyExplosion");
            gameObject.SetActive(false);
        }
    }

    public void changeSprite() {
        sprite1.SetActive(!sprite1.activeSelf);
        sprite2.SetActive(!sprite2.activeSelf);
    }

}
