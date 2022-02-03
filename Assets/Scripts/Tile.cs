using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public Vector2 id;
    public Vector2 position;

    public TileType type;

    public void SetPosition(int x, int y)
    {
        position.x = x;
        position.y = y;
    }

    public Vector2 GetId()
    {
        Debug.Log(id.x + "," + id.y);
        return id;
    }

    public void SetMaxTile(Vector2 maxId)
    {
        if (id.Equals(maxId))
        {
            type = TileType.MAXIMUM;
            this.gameObject.GetComponent<Image>().color = Color.red;
        }
    }

    public void SetHalfTile(Vector2 maxId)
    {
        if (id.Equals(maxId))
        {
            type = TileType.HALF;
            this.gameObject.GetComponent<Image>().color = Color.magenta;
        }
    }
    public void SetQuarterTile()
    {
        if (!type.Equals(TileType.MAXIMUM) && !type.Equals(TileType.HALF))
        {
            type = TileType.QUARTER;
            this.gameObject.GetComponent<Image>().color = Color.yellow;
        }
    }
}
