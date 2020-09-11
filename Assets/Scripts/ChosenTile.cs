using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChosenTile : MonoBehaviour
{
    public TileType tileType;

    public bool clicked = false;

    public int xTile, yTile;


    private void OnMouseDown()
    {
        SelectTile();
    }

    
    //Selects tile based on its tiletype
    public void SelectTile()
    {
        if (!clicked)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();

            switch (tileType)
            {
                case TileType.nearest:
                    sr.color = Color.yellow;
                    break;
                case TileType.chosen:
                    sr.color = Color.red;
                    SpawnTiles.instance._correctTiles++;
                    break;
                case TileType.notchosen:
                    sr.color = Color.green;
                    break;
            }

            SpawnTiles.instance._remainingChances--;

            KeyboardInput.instance.SetTile(this.gameObject, xTile, yTile);

            clicked = true;
        }
    }
}
