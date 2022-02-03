using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    public Vector2 id;
    public Vector2 position;

    public TileType type;

    public bool SelectedTile;

    UnityEvent myEvent = new UnityEvent();

    public void CreateTile(Vector2 pos, Vector2 idNum, TileType tileType, bool selected)
    {
        position = pos;
        id = idNum;
        type = tileType;
        SelectedTile = selected;
    }

    public void SetPosition(int x, int y)
    {
        position.x = x;
        position.y = y;
    }

    public Vector2 GetId()
    {
        return id;
    }

    public void SetMaxTile(Vector2 maxId)
    {
        if (id.Equals(maxId)){ type = TileType.MAXIMUM; }
    }

    public void SetHalfTile(Vector2 maxId)
    {
        if (id.Equals(maxId)) { type = TileType.HALF; }
    }

    public void SetQuarterTile(Vector2 maxId)
    {
        if (id.Equals(maxId)) { type = TileType.QUARTER; }
    }

    public void SetMinimalTile()
    {
        if (!type.Equals(TileType.MAXIMUM) && !type.Equals(TileType.HALF) && !type.Equals(TileType.QUARTER))
        {
            type = TileType.MINIMAL;
        }
    }

    public Tile FindTile(Vector2 idNum)
    {
        if (id.Equals(idNum)) return this;
        else return null;
    }

    public void ShowTileColor()
    {
        if (GridManager.instance.ScanMode)
        {
            switch (type)
            {
                case TileType.MAXIMUM:
                    gameObject.GetComponent<Image>().color = Color.red;
                    break;
                case TileType.HALF:
                    gameObject.GetComponent<Image>().color = Color.magenta;
                    break;
                case TileType.QUARTER:
                    gameObject.GetComponent<Image>().color = Color.yellow;
                    break;
                default:
                    gameObject.GetComponent<Image>().color = Color.gray;
                    break;
            }
        }
    }

    public void ShowNeighbouringTiles()
    {
        GridManager.instance.ShowNeighbouringTiles();
    }
}
