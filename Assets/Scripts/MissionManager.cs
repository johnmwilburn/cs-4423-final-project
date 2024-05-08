using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MissionManager : MonoBehaviour
{
    [Header("Functional References")]
    [SerializeField] private MissionSO missionSO;
    [SerializeField] private AbstractLevelGenerator levelGenerator;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private Tilemap objectTilemap;
    [SerializeField] private ScreenFader screenFader;

    [Header("Prefabs")]
    [SerializeField] private GameObject creatureRatPrefab;
    [SerializeField] private GameObject ammoPickupPrefab;
    [SerializeField] private GameObject levelExitPrefab;

    [Header("Mission Config")]
    public int numEnemiesMin;
    public int numEnemiesMax;
    public int numPickupsMin;
    public int numPickupsMax;
    
    private int playerSpawnIsolationDistance = 10;

    private void Awake()
    {
        HashSet<Vector2Int> floorPositions = levelGenerator.GenerateLevel();

        int numEnemies = UnityEngine.Random.Range(numEnemiesMin, numEnemiesMax + 1);
        int numPickups = UnityEngine.Random.Range(numPickupsMin, numPickupsMax + 1);
        missionSO.StartMission(numEnemies, numPickups);

        MoveObjectToPosition(playerObject, Vector2Int.zero);

        SpawnGameObjects(floorPositions);
    }

    public void FailMission()
    {
        screenFader.FadeToColor("PostGame");
    }

    public void SpawnGameObjects(HashSet<Vector2Int> floorPositions)
    {
        for (int i = 0; i < missionSO.numPickups; i++)
        {
            GameObject pickupObject = Instantiate(ammoPickupPrefab);
            MoveObjectToPosition(pickupObject, GetRandomPosition(floorPositions, isolate: true));
        }

        CreaturePlayer playerCreature = playerObject.GetComponent<CreaturePlayer>();
        for (int i = 0; i < missionSO.numEnemies; i++)
        {
            GameObject enemyObject = Instantiate(creatureRatPrefab);
            CreatureAI enemyAI = enemyObject.transform.GetChild(0).gameObject.GetComponent<CreatureAI>();
            enemyAI.targetCreature = playerCreature;
            MoveObjectToPosition(enemyObject, GetRandomPosition(floorPositions, isolate: true));
        }
        GameObject exitObject = Instantiate(levelExitPrefab);
        exitObject.GetComponent<Exit>().screenFader = screenFader;
        MoveObjectToPosition(exitObject, GetRandomPosition(floorPositions, isolate: true));
    }

    private Vector2Int GetRandomPosition(HashSet<Vector2Int> positions, bool isolate, bool withReplacement = false)
    {
        Vector2Int position = Vector2Int.zero;
        if (isolate)
        {
            int spawnAttempts = 0;
            while (Vector2Int.Distance(position, Vector2Int.zero) < playerSpawnIsolationDistance)
            {
                spawnAttempts++;
                position = positions.ElementAt(UnityEngine.Random.Range(0, positions.Count));

                if (spawnAttempts > 5)
                {
                    playerSpawnIsolationDistance--;
                    spawnAttempts = 0;
                }
            }
        }
        else
        {
            position = positions.ElementAt(UnityEngine.Random.Range(0, positions.Count));
        }

        if (!withReplacement)
        {
            positions.Remove(position);
        }

        return position;
    }

    private void MoveObjectToPosition(GameObject objectToMove, Vector2Int position)
    {
        Vector2Int atomizedPosition = position * 2;

        Vector3 worldPosition = objectTilemap.CellToWorld((Vector3Int)atomizedPosition) + objectTilemap.cellSize / 2;
        objectToMove.transform.position = worldPosition;
    }
}
