using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridTest : MonoBehaviour
{
    public Grid gridBase;

    public Tilemap map;
    public Vector3Int position;
    public int XCoord = 0;
    public int YCoord = 0;

    public Tile[,] tiles = new Tile[22,22];
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < 22; x++)
        {
            for (int y = 0; y < 22; y++)
            {
                position = new Vector3Int(x, y, 0);
                tiles[x, y] = (Tile)map.GetTile(position);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (YCoord < 22)
        {
            if (XCoord < 22)
            {
                print(tiles[XCoord, YCoord]);
                XCoord++;
            }
            else
            {
                XCoord = 0;
                YCoord++;
            }
        }
    }
}
