using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private CreaturePlayer playerCreature;
    [SerializeField] private FieldOfView fov;
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

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPosition = new Vector3(playerCreature.transform.position.x, playerCreature.transform.position.y, 0);
        Vector3 facingDirection = mousePosition - playerPosition;
        fov.SetAimDirection(facingDirection);

        if (Input.GetMouseButtonDown(0))
        {
            playerCreature.AttackRanged(facingDirection);
        }
    }
}
