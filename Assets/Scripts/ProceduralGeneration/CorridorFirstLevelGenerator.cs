using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CorridorFirstLevelGenerator : SimpleRandomWalkLevelGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5, numIterations;
    [SerializeField]
    private bool randomizeStartLoc;
    [SerializeField]
    [Range(0.001f, 1)]
    private float roomPercent;

    protected override void RunProceduralGeneration()
    {
        // Initialize floor position hash set 
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        // Get corridor positions
        HashSet<Vector2Int> corridorPositions, corridorCornerPositions;
        GenerateCorridors(out corridorPositions, out corridorCornerPositions, numIterations, randomizeStartLoc);

        // Add corridors to floor positions
        floorPositions.UnionWith(corridorPositions);

        // Select roomPercent of corridor corners to place rooms at 
        IEnumerable<Vector2Int> randomizedCornerPositions = corridorCornerPositions.OrderBy(x => Guid.NewGuid());
        int numRoomsToCreate = (int)Math.Round(roomPercent * corridorCornerPositions.Count);
        List<Vector2Int> selectedCornerPositions = corridorCornerPositions.Take(numRoomsToCreate).ToList();

        // Also place rooms at dead ends
        List<Vector2Int> deadEnds;
        FindDeadEnds(out deadEnds, corridorPositions);
        selectedCornerPositions.AddRange(deadEnds);

        // Place rooms at selected corners
        HashSet<Vector2Int> roomPositions;
        GenerateRooms(out roomPositions, startPositions: selectedCornerPositions);

        // Add rooms to floor positions
        floorPositions.UnionWith(roomPositions);

        // Find wall positions given floor positions
        HashSet<Vector2Int> wallPositions;
        WallGenerator.FindWallsAroundFloor(out wallPositions, floorPositions);

        // Paint floors and walls onto tilemap
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        tilemapVisualizer.PaintWallTiles(wallPositions);
    }

    private void GenerateCorridors(out HashSet<Vector2Int> floorPositions, out HashSet<Vector2Int> corridorCornerPositions, int numIterations, bool randomizeStartLoc)
    {
        floorPositions = new HashSet<Vector2Int>();
        corridorCornerPositions = new HashSet<Vector2Int>();

        Vector2Int iterationStartPosition = startPosition;
        for (int i = 0; i < numIterations; i++)
        {
            if (randomizeStartLoc && floorPositions.Count > 0)
            {
                iterationStartPosition = floorPositions.ElementAt(UnityEngine.Random.Range(0, floorPositions.Count));
            } 

            Vector2Int corridorStartPosition = iterationStartPosition;

            corridorCornerPositions.Add(corridorStartPosition);
            for (int j = 0; j < corridorCount; j++)
            {
                List<Vector2Int> corridorPositions = ProceduralGenerationAlgorithms.RandomWalkCorridor(corridorStartPosition, corridorLength);
                floorPositions.UnionWith(corridorPositions);

                Vector2Int corridorEndPosition = corridorPositions[corridorPositions.Count - 1];
                corridorCornerPositions.Add(corridorEndPosition);

                corridorStartPosition = corridorEndPosition;
            }
        }
    }

    private void GenerateRooms(out HashSet<Vector2Int> floorPositions, List<Vector2Int> startPositions)
    {
        floorPositions = new HashSet<Vector2Int>();
        foreach (Vector2Int cornerPosition in startPositions)
        {
            HashSet<Vector2Int> randomWalkFloorPositions;
            RunSimpleRandomWalk(out randomWalkFloorPositions, randomWalkParameters, cornerPosition);
            floorPositions.UnionWith(randomWalkFloorPositions);
        }
    }

    private void FindDeadEnds(out List<Vector2Int> deadEnds, HashSet<Vector2Int> corridorPositions)
    {
        deadEnds = new List<Vector2Int>();
        foreach (Vector2Int position in corridorPositions)
        {
            int neighborsCount = 0;
            foreach (Vector2Int direction in Direction2D.cardinalDirections)
            {
                if (corridorPositions.Contains(position + direction))
                {
                    neighborsCount++;
                }
            }
            if (neighborsCount == 1)
            {
                deadEnds.Add(position);
            }
        }
    }
}
