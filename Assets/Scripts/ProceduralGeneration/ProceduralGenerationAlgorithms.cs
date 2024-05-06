using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>{
            startPosition
        };

        Vector2Int newPosition, oldPosition = startPosition;


        for (int i = 0; i < walkLength; i++)
        {
            newPosition = oldPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            oldPosition = newPosition;
        }

        return path;
    }


    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> path = new List<Vector2Int>();

        Vector2Int newPosition, oldPosition = startPosition;
        Vector2Int direction = Direction2D.GetRandomCardinalDirection();
        for (int i = 0; i < corridorLength; i++)
        {
            newPosition = oldPosition + direction;
            path.Add(newPosition);
            oldPosition = newPosition;
        }

        return path;
    }
}

public static class Direction2D
{

    public static List<Vector2Int> cardinalDirections = new List<Vector2Int>
        {
            new Vector2Int(0, 1),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0)
        };

    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirections[Random.Range(0, cardinalDirections.Count)];
    }
}

