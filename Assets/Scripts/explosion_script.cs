using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("starting coroutine to reset self!");
        StartCoroutine(reset());
    }

    public IEnumerator reset()
    {
        while (gameObject.activeSelf)
        {
            Debug.Log("setting self to inactive!");
            yield return new WaitForSecondsRealtime(1f);
            this.gameObject.SetActive(false);
            StopCoroutine(reset());
        }
    }
}
