using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private int tilesPerPosition = 2;

    [Header("Floor Tiles (Collision Disabled)")]
    [SerializeField]
    private Tilemap floorTilemap;
    [SerializeField]
    private TileBase floorTile;

    [Header("Wall Tiles (Collision Enabled)")]
    [SerializeField]
    private Tilemap wallTilemap;
    [SerializeField]
    private TileBase wallTile;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    public void PaintWallTiles(IEnumerable<Vector2Int> wallPositions)
    {
        PaintTiles(wallPositions, wallTilemap, wallTile);
    }

    public void ClearTilemaps()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (Vector2Int position in positions)
        {
            for (int i = 0; i < tilesPerPosition; i++)
            {
                for (int j = 0; j < tilesPerPosition; j++)
                {
                    Vector2Int adjustedPosition = (position * tilesPerPosition) + Vector2Int.up * i + Vector2Int.right * j;
                    tilemap.SetTile((Vector3Int)adjustedPosition, tile);
                }
            }

        }
    }
}
