using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MineSweeperGameController : MonoBehaviour {

    [SerializeField] Transform frontWall;
    [SerializeField] Transform rightWall;
    [SerializeField] Transform MineSweeperGuy;
    [SerializeField] GameObject DifficultyMenu;
    [SerializeField] GameObject WinMenu;
    [SerializeField] GameObject LoseMenu;
    [SerializeField] TextMeshProUGUI WinCount;
    [SerializeField] TextMeshProUGUI LoseCount;

    [SerializeField] SoundManagerScript soundManager;


    [SerializeField] MineSweeperTileController[] tileArray;
    MineSweeperTileController[,] multiTileArray;
    public int revealedTiles;
    public int[,] bombArray;
    public int boardSize;
    public int bombCount;
    private bool firstClick;
    private bool paused;


    // Start is called before the first frame update
    void Start() {
        revealedTiles = 0;
        //default board size
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

        //MineSweeperGuyStart = MineSweeperGuy.position;

        Time.timeScale = 0;
        firstClick = false;
        paused = true;
    }

    // Update is called once per frame
    void Update()    {
        if (Input.GetKeyDown(KeyCode.P)) {
            for (int i = 0; i < tileArray.Length; i ++) {
                tileArray[i].revealTile(bombArray[(int)(i/24f) , i%24]);

            }
        }
        un_Pause();

        if (revealedTiles == (boardSize * boardSize) - bombCount) {
            //win game
            WinMenu.SetActive(true);
            WinCount.text = "You uncovered " + revealedTiles + " tiles!";

        }


    }

    public void LoseGame() {
        soundManager.playSound("kaboom");
        LoseMenu.SetActive(true);
        LoseCount.text = "You uncovered " + revealedTiles + " tiles!";
    }


    public void checkTile(int xCord, int zCord) {
        if(!paused)
        {
            if (!firstClick)
            {
                firstClick = true;
                generateBoard(xCord, zCord);
            }
            if (tileArray[xCord * 24 + zCord].flag.enabled)
            {
                return;
            }
            int value = bombArray[xCord, zCord];
            Debug.Log("revealing tile at (" + xCord + ", " + zCord + ") with value: " + value + ", " + (xCord * 24 + zCord));
            tileArray[xCord * 24 + zCord].revealTile(value);

            soundManager.playSound("left_click");

            if (value == 0)
            {
                revealNeighbors(xCord, zCord);
            }
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
        int randX = Random.Range(0, boardSize);
        int randZ = Random.Range(0, boardSize);
        int[,] bombLocations = new int[bombCount,2];
   
        for (int i = 0; i < bombCount; i++) {
            do {
                randX = Random.Range(0, boardSize);
                randZ = Random.Range(0, boardSize);
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



    public void goToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void restart() {
        SceneManager.LoadScene("MineSweeper");

    }


    public void QuitGame(string a) {
        Debug.Log("Quit Game Button Clicked");

#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void setEasy()
    {
        revealedTiles = 0;
        frontWall.position = new Vector3(frontWall.position.x, frontWall.position.y, 10f);
        rightWall.position = new Vector3(10f, rightWall.position.y, rightWall.position.z);

        MineSweeperGuy.position = new Vector3(5f, 0f, 5f);

        for(int tile = 0; tile < tileArray.Length; tile++)
        {
            tileArray[tile].resetTile();
        }

        boardSize = 10;

        firstClick = false;
        paused = false;
        Time.timeScale = 1;

        DifficultyMenu.SetActive(false);
    }

    public void setMedium()
    {
        revealedTiles = 0;
        frontWall.position = new Vector3(frontWall.position.x, frontWall.position.y, 18f);
        rightWall.position = new Vector3(18f, rightWall.position.y, rightWall.position.z);

        MineSweeperGuy.position = new Vector3(9f, 0f, 9f);

        for (int tile = 0; tile < tileArray.Length; tile++)
        {
            tileArray[tile].resetTile();
        }

        boardSize = 18;

        firstClick = false;
        paused = false;
        Time.timeScale = 1;

        DifficultyMenu.SetActive(false);
    }

    public void setHard()
    {
        revealedTiles = 0;
        frontWall.position = new Vector3(frontWall.position.x, frontWall.position.y, 24f);
        rightWall.position = new Vector3(24f, rightWall.position.y, rightWall.position.z);

        MineSweeperGuy.position = new Vector3(12f, 0f, 12f);
        MineSweeperGuy.rotation = new Quaternion();

        for (int tile = 0; tile < tileArray.Length; tile++)
        {
            tileArray[tile].resetTile();
        }

        boardSize = 24;

        firstClick = false;
        paused = false;
        Time.timeScale = 1;

        DifficultyMenu.SetActive(false);
    }

    private void un_Pause()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !DifficultyMenu.activeSelf)
        {
            Debug.Log("Pausing the game");
            DifficultyMenu.SetActive(true);
            Time.timeScale = 0;
            paused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && DifficultyMenu.activeSelf)
        {
            Debug.Log("Un-pausing the game");
            DifficultyMenu.SetActive(false);
            Time.timeScale = 1;
            paused = false;
        }
    }
}
