using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap tilemap;

    public List<TileBase> newTile;

    private void Start()
    {
        //OpenTheDoor();
    }

    public void OpenTheDoor()
    {
        var one = new Vector3Int(-1, 6, 0);
        var two = new Vector3Int(0, 6, 0);
        tilemap.SetTile(one, newTile[0]);
        tilemap.SetTile(two, newTile[1]);
    }
}
