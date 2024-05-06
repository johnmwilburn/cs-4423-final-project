using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SimpleRandomWalkLevelGenerator : AbstractLevelGenerator
{
    [SerializeField]
    protected SimpleRandomWalkSO randomWalkParameters;

    protected override HashSet<Vector2Int> RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions;
        RunSimpleRandomWalk(out floorPositions, randomWalkParameters, startPosition);

        HashSet<Vector2Int> wallPositions;
        WallGenerator.FindWallsAroundFloor(out wallPositions, floorPositions);

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        tilemapVisualizer.PaintWallTiles(wallPositions);

        return floorPositions;
    }

    public void RunSimpleRandomWalk(out HashSet<Vector2Int> floorPositions, SimpleRandomWalkSO parameters, Vector2Int startPosition)
    {
        floorPositions = new HashSet<Vector2Int>();
        Vector2Int iterationStartPosition = startPosition;
        
        for (int i = 0; i < parameters.numIterations; i++)
        {
            if (parameters.randomizeStartLoc && floorPositions.Count > 0)
            {
                iterationStartPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }

            HashSet<Vector2Int> walkPath = ProceduralGenerationAlgorithms.SimpleRandomWalk(iterationStartPosition, parameters.walkLength);
            floorPositions.UnionWith(walkPath);
        }

    }
}
