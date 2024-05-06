using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_", menuName = "ScriptableObjects/PCG/SimpleRandomWalkSO")]
public class SimpleRandomWalkSO : ScriptableObject
{
    public int numIterations = 10;
    public int walkLength = 10;
    public bool randomizeStartLoc = true;
}
