using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class MineSweeperTileController : MonoBehaviour {
    [SerializeField] Renderer tileMaterial;
    [SerializeField] GameObject tile;
    [SerializeField] Material darkerTile;
    [SerializeField] TextMeshProUGUI mineCount;
    public SpriteRenderer flag;
    public SpriteRenderer bomb;
    public bool pressed = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void revealTile(int TileValue) {
        if (!pressed && TileValue > -1) {
            Debug.Log("Revealed with tile value: " + TileValue);
            Vector3 newPos = new Vector3(tile.transform.position.x, tile.transform.position.y - 0.2f, tile.transform.position.z);
            tile.transform.position = Vector3.Lerp(tile.transform.position, newPos, 1.0f);
            tileMaterial.material = darkerTile;
            if (TileValue > 0)
                mineCount.text = TileValue.ToString();
            pressed = true;
            flag.enabled = false;
        }
        else if (TileValue == -1) {
            //bakoom
            bomb.enabled = true;

        }

    }

    public void markBlock() {
        if (!pressed) {
            Debug.Log("marking" + flag.enabled);
            flag.enabled = flag.enabled ? false : true;
        }
    }


}
