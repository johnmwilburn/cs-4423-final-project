using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To hold mission stats for communication between player, procedural generation, and level manager

[CreateAssetMenu(menuName = "ScriptableObjects/MissionSO")]
public class MissionSO : ScriptableObject
{
    public int numEnemies;
    public int numEnemiesKilled;
    public int numPickups;
    public int numPickupsCollected;

    public void StartMission(int numEnemies, int numPickups)
    {
        this.numEnemies = numEnemies;
        this.numPickups = numPickups;
        numEnemiesKilled = 0;
        numPickupsCollected = 0;
    }

    public void IncrementEnemiesKilled()
    {
        numEnemiesKilled++;
    }

    public void IncrementPickupsCollected()
    {
        numPickupsCollected++;
    }

    public bool IsMissionComplete()
    {
        return numEnemiesKilled >= numEnemies;
    }
}
