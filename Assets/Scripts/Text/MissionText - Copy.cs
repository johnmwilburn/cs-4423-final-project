using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionText : MonoBehaviour
{

    [SerializeField] private MissionSO missionSO;
    private TextMeshProUGUI missionText;

    // Start is called before the first frame update
    void Start()
    {
        missionText = GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        missionText.text = string.Format("Kills: {0} / {1}", missionSO.numEnemiesKilled, missionSO.numEnemies);
    }
}
