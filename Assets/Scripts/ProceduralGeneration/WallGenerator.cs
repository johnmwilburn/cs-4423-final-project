using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void FindWallsAroundFloor(out HashSet<Vector2Int> wallPositions, HashSet<Vector2Int> floorPositions)
    {
        wallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirections);
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> cardinalDirections)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();

        foreach (var position in floorPositions)
        {
            foreach (Vector2Int direction in cardinalDirections)
            {
                Vector2Int neighborPosition = position + direction;
                if (!floorPositions.Contains(neighborPosition))
                {
                    wallPositions.Add(neighborPosition);
                }
            }
        }
        return wallPositions;
    }
}
