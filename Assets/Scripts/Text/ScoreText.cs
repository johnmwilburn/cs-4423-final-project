using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{

    [SerializeField] private MissionSO missionSO;
    private TextMeshProUGUI missionText;

    void Start()
    {
        missionText = GetComponent<TextMeshProUGUI>();
        missionText.text = string.Format("Your Score:\n {0}", missionSO.numEnemiesKilled);
    }
}
