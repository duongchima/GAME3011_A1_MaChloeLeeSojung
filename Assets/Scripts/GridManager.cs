using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int gridWidth, gridHeight;
    [SerializeField]
    private Tile tilePrefab;
    [SerializeField]
    private Transform camera;

    public void GenerateGrid()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
            }
        }

        camera.transform.position = new Vector3(gridWidth / 2.0f - 0.5f, gridHeight / 2.0f - 0.5f, -10);
    }
}
