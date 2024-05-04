using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class Exit : MonoBehaviour
{
    public MissionSO missionSO;
    public GameObject missionCompleteUI;
    private RenderManager renderManager;
    private bool isOpen;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isOpen && other.GetComponent<Creature>()?.creatureType == Creature.Type.Player)
        {
            Debug.Log("Player has reached the exit!");
            missionCompleteUI.SetActive(true);
        }
    }

    void Start()
    {
        renderManager = GetComponent<RenderManager>();
        renderManager.SetColor(Color.red);
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpen && missionSO.IsMissionComplete())
        {
            renderManager.SetColor(Color.green);
            isOpen = true;
        }
    }
}
