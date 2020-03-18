using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSweeperGameController : MonoBehaviour{
    [SerializeField] MineSweeperTileController[] tileArray;
    private int[][] bombArray;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()    {
        if (Input.GetKeyDown(KeyCode.P)) {
            for (int i = 0; i < tileArray.Length; i ++) {
                tileArray[i].revealTile(i);

            }
        }



    }
}
