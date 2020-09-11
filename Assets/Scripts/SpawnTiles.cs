using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTiles : MonoBehaviour
{
    public static SpawnTiles instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject[,] tiles = new GameObject[20, 20];

    public GameObject tilePrefab;

    private int correctTiles;

    public int _correctTiles
    {
        get { return correctTiles; }
        set
        {
            correctTiles = value;

            if (correctTiles == 10)
            {
                Result();

                _correctTiles = 0;

                InitNewTiles();
            }
        }
    }

    private int remainingChances;

    public int _remainingChances
    {
        get { return remainingChances; }
        set
        {
            remainingChances = value;

            print(remainingChances);

            if (remainingChances == 0)
            {
                Result();

                InitNewTiles();
            }
        }
    }

    private void Start()
    {
        Init();

        InitNewTiles();
    }

    void Init()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                GameObject newTile = Instantiate(tilePrefab, this.transform);

                tiles[i, j] = newTile.gameObject;

                tiles[i, j].transform.position = new Vector2(i * 5, j * 5);

                tiles[i, j].AddComponent<ChosenTile>();

                tiles[i, j].GetComponent<ChosenTile>().xTile = i;

                tiles[i, j].GetComponent<ChosenTile>().yTile = j;
            }
        }

        Camera.main.transform.position = new Vector3(95 / 2, 95 / 2, -1);

        Camera.main.orthographicSize = 50;
    }

    public void InitNewTiles()
    {
        Reset();

        KeyboardInput.instance.ResetKeyboard();

        SelectRandomTiles();
    }

    private void Reset()
    {
        _remainingChances = 20;

        _correctTiles = 0;

        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                tiles[i, j].GetComponent<ChosenTile>().tileType = TileType.notchosen;

                tiles[i, j].GetComponent<ChosenTile>().clicked = false;

                tiles[i, j].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    void SelectRandomTiles()
    {
        for (int i = 0; i < 10; i++)
        {
            int randomIDx = Random.Range(0, 20);

            int randomIDy = Random.Range(0, 20);

            ChosenTile chosenTile = tiles[randomIDx, randomIDy].GetComponent<ChosenTile>();

            chosenTile.tileType = TileType.chosen;
        }

        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                if (tiles[i, j].GetComponent<ChosenTile>())
                {
                    if (tiles[i, j].GetComponent<ChosenTile>().tileType == TileType.chosen)
                    {
                        if (isvalid(i - 2, j, 20))
                        {
                            tiles[i - 2, j].GetComponent<ChosenTile>().tileType = TileType.nearest;
                        }

                        if (isvalid(i + 2, j, 20))
                        {
                            tiles[i + 2, j].GetComponent<ChosenTile>().tileType = TileType.nearest;
                        }

                        if (isvalid(i, j - 2, 20))
                        {
                            tiles[i, j - 2].GetComponent<ChosenTile>().tileType = TileType.nearest;
                        }

                        if (isvalid(i, j + 2, 20))
                        {
                            tiles[i, j + 2].GetComponent<ChosenTile>().tileType = TileType.nearest;
                        }
                    }
                }
            }
        }
    }

    void Result()
    {
        if (correctTiles == 10)
        {
            print("WIN");
        }
        else
        {
            print("LOST");
        }

    }

    bool isvalid(int i, int j, int grid)
    {
        if (i >= 0 && j >= 0 && i < grid && j < grid && tiles[i, j].GetComponent<ChosenTile>().tileType != TileType.chosen)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void HighlightTile(GameObject select, Color color)
    {
        if (select)
            select.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;
    }
}
public enum TileType
{
    chosen, nearest, notchosen
}
