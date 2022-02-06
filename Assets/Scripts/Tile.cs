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
    public int resources;
    public bool isRevealed;

    public void CreateTile(Vector2 pos, Vector2 idNum, TileType tileType, bool founded)
    {
        position = pos;
        id = idNum;
        type = tileType;
        isRevealed = founded;
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

    public void Scan()
    {
        GridManager.instance.Scan();
    }

    public void BreakTile()
    {
        type = TileType.MINIMAL;
    }

    public void DegradeTile()
    {
        if (type != TileType.MINIMAL) { type++; }
    }

    public void Extract()
    {
        GridManager.instance.Extract();
    }

    public int CollectResources()
    {
        switch (type)
        {
            case TileType.MAXIMUM:
                return resources = 5000;
            case TileType.HALF:
                return resources = 2500;
            case TileType.QUARTER:
                return resources = 1250;
            default:
                return resources = 0;
        }
    }
}
