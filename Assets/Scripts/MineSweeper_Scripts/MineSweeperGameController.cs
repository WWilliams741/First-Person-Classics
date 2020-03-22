using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSweeperGameController : MonoBehaviour{
    
    [SerializeField] MineSweeperTileController[] tileArray;
    MineSweeperTileController[,] multiTileArray;
    public int[,] bombArray;
    public int boardSize;
    public int bombCount;
    private bool firstClick = false;


    // Start is called before the first frame update
    void Start() {
        boardSize = 24;
        bombArray = new int[boardSize, boardSize];
        /*
        multiTileArray = new MineSweeperTileController[boardSize, boardSize];
        for (int row = 0; row < boardSize; row++) {
            for (int col = 0; col < boardSize; col++) {
                multiTileArray[row, col] = tileArray[row * 23 + col];
                //tileArray[row * 23 + col].transform.parent.gameObject.SetActive(false);
            }
        }*/
    }

    // Update is called once per frame
    void Update()    {
        if (Input.GetKeyDown(KeyCode.P)) {
            for (int i = 0; i < tileArray.Length; i ++) {
                tileArray[i].revealTile(bombArray[(int)(i/24f) , i%24]);

            }
        }

    }


    public void checkTile(int xCord, int zCord) {
        if (!firstClick) {
            firstClick = true;
            generateBoard(xCord, zCord);
        }
        if (tileArray[xCord * 24 + zCord].flag.enabled) {
            return;
        }
        int value = bombArray[xCord, zCord];
        Debug.Log("revealing tile at (" + xCord + ", " + zCord + ") with value: " + value + ", " + (xCord * 24 + zCord));
        tileArray[xCord * 24 + zCord].revealTile(value);
        if (value == 0) {
            revealNeighbors(xCord, zCord);
        }
    }
    public void checkTileIgnoreFlag(int xCord, int zCord) {
        int value = bombArray[xCord, zCord];
        Debug.Log("revealing tile at (" + xCord + ", " + zCord + ") with value: " + value + ", " + (xCord * 24 + zCord));
        tileArray[xCord * 24 + zCord].revealTile(value);
        if (value == 0) {
            revealNeighbors(xCord, zCord);
        }
    }



    private void generateBoard(int xCord, int zCord) {
        //checks size of board then assigns the amount of bombs accordingly
        bombCount = boardSize == 10 ? 10 : boardSize == 18 ? 40 : boardSize == 24 ? 100 : boardSize * 3;
        int randX = Random.Range(0, 24);
        int randZ = Random.Range(0, 24);
        int[,] bombLocations = new int[bombCount,2];
   
        for (int i = 0; i < bombCount; i++) {
            do {
                randX = Random.Range(0, 24);
                randZ = Random.Range(0, 24);
            } while ((Mathf.Abs(randX - xCord) < 3 && Mathf.Abs(randZ - zCord) < 3) || (bombArray[randX, randZ] == -1));
            bombArray[randX, randZ] = -1;
            bombLocations[i, 0] = randX;
            bombLocations[i, 1] = randZ;
        }

        //adds 0-8 on bomb array for how many bombs are near each tile
        for (int i = 0; i < bombCount; i++) {
            //left
            if ( bombLocations[i, 0] > 0) {
                if(bombArray[bombLocations[i, 0] - 1, bombLocations[i, 1]]!=-1)
                    bombArray[bombLocations[i, 0] - 1, bombLocations[i, 1]]++;
            }
            //top left
            if (bombLocations[i, 1] > 0 && bombLocations[i, 0] > 0) {
               if (bombArray[bombLocations[i, 0] - 1, bombLocations[i, 1] - 1] != -1)
                    bombArray[bombLocations[i, 0]-1, bombLocations[i, 1] - 1]++;
            }
            //top
            if (bombLocations[i, 1] > 0) {
                if(bombArray[bombLocations[i, 0], bombLocations[i, 1] - 1] != -1)
                    bombArray[bombLocations[i, 0], bombLocations[i, 1]-1]++;
            }
            //top right
            if (bombLocations[i, 0] > 0 && bombLocations[i, 1] < boardSize - 1) {
                if(bombArray[bombLocations[i, 0] - 1, bombLocations[i, 1] + 1] != -1)
                    bombArray[bombLocations[i, 0] - 1, bombLocations[i, 1] + 1]++;
            }
            //right
            if (bombLocations[i, 1] < boardSize - 1) {
                if (bombArray[bombLocations[i, 0] , bombLocations[i, 1] + 1] != -1)
                    bombArray[bombLocations[i, 0] , bombLocations[i, 1] + 1]++;
            }
            //bottom right
            if (bombLocations[i,0] < boardSize-1 && bombLocations[i, 1] < boardSize - 1) {
                if (bombArray[bombLocations[i, 0] + 1, bombLocations[i, 1] + 1] != -1)
                    bombArray[bombLocations[i, 0] + 1, bombLocations[i, 1] + 1]++;
            }
            //bottom
            if (bombLocations[i, 0] < boardSize - 1) {
                if (bombArray[bombLocations[i, 0] + 1, bombLocations[i, 1]] != -1)
                    bombArray[bombLocations[i, 0] + 1, bombLocations[i, 1]]++;
            }
            //bottom left
            if (bombLocations[i, 0] < boardSize - 1 && bombLocations[i,1] > 0) {
                if (bombArray[bombLocations[i, 0] + 1, bombLocations[i, 1] - 1] != -1)
                    bombArray[bombLocations[i, 0] + 1, bombLocations[i, 1] - 1]++;
            }
            


        }




    }

    private void revealNeighbors(int xCord, int zCord) {
        //left
        //Debug.Log("left " + !tileArray[xCord - 1 * 24 + zCord].pressed);
        Debug.Log(xCord + ", "+ zCord + ", " + ((xCord - 1) * 24 + zCord));
        if (xCord > 0 && !tileArray[(xCord-1) * 24 + zCord].pressed) {
            checkTileIgnoreFlag(xCord - 1, zCord);
           
        }

        //bottom left
        
        if (zCord > 0 && xCord > 0 && !tileArray[(xCord-1) * 24 + zCord-1].pressed) {
            checkTile(xCord - 1, zCord - 1);
        }
        //bottom
        if (zCord > 0 && !tileArray[xCord * 24 + zCord-1].pressed) {
            checkTile(xCord, zCord - 1);
        }
        //bottom right
        if (xCord > 0 && zCord < boardSize - 1 && !tileArray[(xCord-1) * 24 + zCord+1].pressed) {
            checkTile(xCord - 1, zCord + 1);
        }
        //right
        if (zCord < boardSize - 1 && !tileArray[xCord * 24 + zCord+1].pressed) {
                checkTile(xCord, zCord + 1);
        }
        //top right
        if (xCord < boardSize - 1 && zCord < boardSize - 1 && !tileArray[(xCord+1) * 24 + zCord+1].pressed) {
                checkTile(xCord + 1, zCord + 1);
        }
        //bottom
        if (xCord < boardSize - 1 && !tileArray[(xCord+1) * 24 + zCord].pressed) {
                checkTile(xCord + 1, zCord);
        }
        //bottom left
        if (xCord < boardSize - 1 && zCord > 0 && !tileArray[(xCord+1) * 24 + zCord-1].pressed) {
                checkTile(xCord + 1, zCord - 1);
        }

    }

    public void markBlock(int xCord, int zCord) {
        tileArray[xCord * 24 + zCord].markBlock();


    }


}
