using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    public static KeyboardInput instance;

    private void Awake()
    {
        instance = this;
    }

    public int xTile, yTile;

    public GameObject selectedTile;

    public Color hColor, nColor;

    private void Start()
    {
        selectedTile = SpawnTiles.instance.tiles[xTile, yTile];
    }

    public void ResetKeyboard() { SpawnTiles.instance.HighlightTile(selectedTile, nColor); xTile = 0; yTile = 0; selectedTile = SpawnTiles.instance.tiles[xTile, yTile]; }

    public void SetTile(GameObject sTile, int x, int y)
    {
        SpawnTiles.instance.HighlightTile(selectedTile, nColor);
        selectedTile = sTile;
        SpawnTiles.instance.HighlightTile(selectedTile, hColor);
        xTile = x;
        yTile = y;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SpawnTiles.instance.HighlightTile(selectedTile, nColor);
            yTile++;
            selectedTile = SpawnTiles.instance.tiles[xTile, yTile];
            SpawnTiles.instance.HighlightTile(selectedTile, hColor);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnTiles.instance.HighlightTile(selectedTile, nColor);
            xTile--;
            selectedTile = SpawnTiles.instance.tiles[xTile, yTile];
            SpawnTiles.instance.HighlightTile(selectedTile, hColor);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnTiles.instance.HighlightTile(selectedTile, nColor);
            yTile--;
            selectedTile = SpawnTiles.instance.tiles[xTile, yTile];
            SpawnTiles.instance.HighlightTile(selectedTile, hColor);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SpawnTiles.instance.HighlightTile(selectedTile, nColor);
            xTile++;
            selectedTile = SpawnTiles.instance.tiles[xTile, yTile];
            SpawnTiles.instance.HighlightTile(selectedTile, hColor);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            selectedTile.GetComponent<ChosenTile>().SelectTile();
        }
    }
}
