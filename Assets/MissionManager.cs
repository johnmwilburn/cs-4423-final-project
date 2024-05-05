using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField]
    private MissionSO missionSO;

    [Header("Mission Config")]
    public int numEnemies;
    public int numPickups;

    private void Awake(){
        missionSO.StartMission(numEnemies, numPickups);
    }
}
