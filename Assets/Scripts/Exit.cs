using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Exit : MonoBehaviour
{
    [SerializeField] private MissionSO missionSO;
    public ScreenFader screenFader;
    private RenderManager renderManager;
    private bool isOpen;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isOpen && other.GetComponent<CreaturePlayer>())
        {
            Debug.Log("Player has reached the exit!");
            screenFader.FadeToColor("PostGame");
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
