using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractLevelGenerator : MonoBehaviour
{
    [SerializeField]
    protected TilemapVisualizer tilemapVisualizer = null;

    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    public HashSet<Vector2Int> GenerateLevel()
    {
        tilemapVisualizer.ClearTilemaps();
        HashSet<Vector2Int> floorPositions = RunProceduralGeneration();
        return floorPositions;
    }

    protected abstract HashSet<Vector2Int> RunProceduralGeneration();
}
