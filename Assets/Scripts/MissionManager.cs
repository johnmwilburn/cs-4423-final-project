using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MissionManager : MonoBehaviour
{
    [Header("Functional References")]
    [SerializeField]
    private MissionSO missionSO;
    [SerializeField]
    private AbstractLevelGenerator levelGenerator;
    [SerializeField]
    private GameObject playerCreature;
    [SerializeField]
    private Tilemap objectTilemap;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject creatureRatPrefab;
    [SerializeField]
    private GameObject ammoPickupPrefab;
    [SerializeField]
    private GameObject levelExitPrefab;

    [Header("Mission Config")]
    public int numEnemiesMin;
    public int numEnemiesMax;
    public int numPickupsMin;
    public int numPickupsMax;    

    private void Awake(){
        HashSet<Vector2Int> floorPositions = levelGenerator.GenerateLevel();

        int numEnemies = Random.Range(numEnemiesMin, numEnemiesMax + 1);
        int numPickups = Random.Range(numPickupsMin, numPickupsMax + 1);
        

        SpawnGameObjects(floorPositions);

        missionSO.StartMission(numEnemies, numPickups);
    }


    public void SpawnGameObjects(HashSet<Vector2Int> floorPositions){
        for (int i=0; i<missionSO.numPickups; i++)
        {
            GameObject pickupObject = Instantiate(ammoPickupPrefab);
            MoveObjectToPosition(pickupObject, GetRandomPosition(floorPositions));
        }
        for (int i=0; i<missionSO.numEnemies; i++)
        {
            GameObject enemyObject = Instantiate(creatureRatPrefab);
            // Fix this so that the enemy AI has a reference to the player (target) creature
            MoveObjectToPosition(enemyObject, GetRandomPosition(floorPositions));
        }
        GameObject exitObject = Instantiate(levelExitPrefab);
        MoveObjectToPosition(exitObject, GetRandomPosition(floorPositions));

        MoveObjectToPosition(playerCreature, GetRandomPosition(floorPositions));
    }

    private Vector2Int GetRandomPosition(HashSet<Vector2Int> positions, bool withReplacement=false){
        Vector2Int position = positions.ElementAt(Random.Range(0, positions.Count));
        if (!withReplacement){
            positions.Remove(position);
        }
        return position;
    }

    private void MoveObjectToPosition(GameObject objectToMove, Vector2Int position){
        Vector3 worldPosition = objectTilemap.CellToWorld((Vector3Int)position) + objectTilemap.cellSize / 2;
        objectToMove.transform.position = worldPosition;
    }
}
