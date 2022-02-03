using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    [SerializeField]
    private float gridWidth = 0, gridHeight = 0;
    [SerializeField]
    private Tile tilePrefab = null;
    [SerializeField]
    private Canvas canvas = null;
    [SerializeField]
    private int NumOfMaximumTiles = 0;

    public bool ScanMode = false;

    private List<Tile> tilesList = new List<Tile>();

    [System.Obsolete]
    private void Awake()
    {
        instance = this;
        Random.seed = 42;
    }

    private void ClearGrid()
    {
        if (tilesList.Count != 0) tilesList.Clear();
        if (canvas.transform.childCount > 0)
        {
            foreach (Transform child in canvas.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    public void GenerateGrid()
    {
        ClearGrid();

        Vector2 canvasSize = canvas.GetComponent<RectTransform>().rect.position;
        Rect tileSize = tilePrefab.GetComponent<RectTransform>().rect;

        for (float x = 0; x < gridWidth; x++)
        {
            for (float y = 0; y < gridHeight; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(((x - gridWidth / 2) * tileSize.width) - canvasSize.x, ((y - gridWidth / 2) * tileSize.height) - canvasSize.y), Quaternion.identity, canvas.transform);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.CreateTile(new Vector2(spawnedTile.position.x, spawnedTile.position.y), new Vector2(x, y), TileType.MINIMAL, false);
                tilesList.Add(spawnedTile);
            }
        }

        GenerateMaximumTiles();
        foreach (Tile tile in tilesList)
        {
            if (tile.type.Equals(TileType.MAXIMUM))
            {
                GenerateHalfTiles(tile);
                GenerateQuarterTiles(tile);
            }
        }
        GenerateMinimalTiles();
    }

    private void GenerateMaximumTiles()
    {
        int randomTileX, randomTileY; 
        for (int i = 0; i < NumOfMaximumTiles; i++)
        {
            randomTileX = (int)Random.Range(0, gridWidth);
            randomTileY = (int)Random.Range(0, gridHeight);

            foreach (Tile tile in tilesList)
            {
                if (tile.GetId().Equals(new Vector2(randomTileX, randomTileY)))
                {
                    if (tile.type.Equals(TileType.MAXIMUM)) i--;
                    else tile.SetMaxTile(new Vector2(randomTileX, randomTileY));
                }
            }
        }
    }

    private void GenerateHalfTiles(Tile maxTile)
    {
        foreach(Tile tile in tilesList)
        {
            for (int j = -1; j <= 1; j++)
            {
                tile.SetHalfTile(new Vector2(maxTile.GetId().x - 1, maxTile.GetId().y + j));
                tile.SetHalfTile(new Vector2(maxTile.GetId().x + 1, maxTile.GetId().y + j));
            }

            tile.SetHalfTile(new Vector2(maxTile.GetId().x, maxTile.GetId().y - 1));
            tile.SetHalfTile(new Vector2(maxTile.GetId().x, maxTile.GetId().y + 1));
        }
    }

    private void GenerateQuarterTiles(Tile maxTile)
    {
        foreach (Tile tile in tilesList)
        {
            for (int j = -2; j <= 2; j++)
            {
                tile.SetQuarterTile(new Vector2(maxTile.GetId().x - 2, maxTile.GetId().y + j));
                tile.SetQuarterTile(new Vector2(maxTile.GetId().x + 2, maxTile.GetId().y + j));

                tile.SetQuarterTile(new Vector2(maxTile.GetId().x + j, maxTile.GetId().y - 2));
                tile.SetQuarterTile(new Vector2(maxTile.GetId().x + j, maxTile.GetId().y + 2));
            }
        }
    }

    private void GenerateMinimalTiles()
    {
        foreach (Tile tile in tilesList)
        {
            tile.SetMinimalTile();
        }
    }

    public void ScanTiles()
    {
        ScanMode = !ScanMode;
    }

    public void ShowNeighbouringTiles()
    {
        Tile selectedTile = EventSystem.current.currentSelectedGameObject.GetComponent<Tile>();
        if (ScanMode)
        {
            foreach (Tile tile in tilesList)
            {
                for (int j = -1; j <= 1; j++)
                {
                    for (int i = -1; i <= 1; i++)
                    {
                        if (tile.FindTile(new Vector2(selectedTile.GetId().x - i, selectedTile.GetId().y - j)))
                        {
                            tile.ShowTileColor();
                        }
                    }
                }
            }
        }
    }
}
