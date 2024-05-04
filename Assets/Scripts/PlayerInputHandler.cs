using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Creature playerCreature;
    // Update is called once per frame
    void Update()
    {

        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.up;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.down;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }

        playerCreature.MoveCreature(direction);
        
        if (Input.GetMouseButtonDown(0))
        {
            playerCreature.AttackRanged(playerCreature.facingDirection);
        }
    }
}
