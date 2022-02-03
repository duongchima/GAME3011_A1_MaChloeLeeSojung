using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private float gridWidth, gridHeight;
    [SerializeField]
    private Tile tilePrefab = null;
    [SerializeField]
    private Canvas canvas = null;
    [SerializeField]
    private int NumOfMaximumTiles = 0;

    private List<Tile> tilesList = new List<Tile>();

    private void Start()
    {
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
                spawnedTile.position = new Vector2(spawnedTile.position.x, spawnedTile.position.y);
                spawnedTile.id = new Vector2(x, y);
                spawnedTile.type = TileType.QUARTER;
                tilesList.Add(spawnedTile);
            }
        }

        GenerateMaximumTiles();
        foreach (Tile tile in tilesList)
        {
            if (tile.type.Equals(TileType.MAXIMUM)) GenerateHalfTiles(tile);
        }
        GenerateQuarterTiles();
    }

    public void GenerateMaximumTiles()
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

    public void GenerateHalfTiles(Tile maxTile)
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

    public void GenerateQuarterTiles()
    {
        foreach(Tile tile in tilesList)
        {
            tile.SetQuarterTile();
        }
    }
}
