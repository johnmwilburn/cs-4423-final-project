using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class Exit : MonoBehaviour
{
    public MissionSO missionSO;
    public SpriteRenderer body;
    public GameObject missionCompleteUI;
    bool isOpen;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isOpen && other.GetComponent<Creature>()?.creatureType == Creature.Type.Player)
        {
            Debug.Log("Player has reached the exit!");
            missionCompleteUI.SetActive(true);
        }
    }

    void FitBoxColliderToSprite()
    {
        // body.transform.position = Vector3.zero; // this doesnt work right, but make sure the body is at 0,0,0 relative to the parent
        SpriteRenderer renderer = body.GetComponent<SpriteRenderer>();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.size = new Vector2(renderer.size.x, renderer.size.y);
        collider.offset = new Vector2(0, 0);
    }

    void Start()
    {
        FitBoxColliderToSprite();
        body.color = Color.red;
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpen && missionSO.IsMissionComplete())
        {
            body.color = Color.green;
            isOpen = true;
        }
    }
}
