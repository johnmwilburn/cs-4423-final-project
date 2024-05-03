using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Creature playerCreature;
    [SerializeField] private AudioManager audioManager;
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

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerCreature.AttackRanged(Vector3.right);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerCreature.AttackRanged(Vector3.left);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerCreature.AttackRanged(Vector3.up);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerCreature.AttackRanged(Vector3.down);
        }


        if (Input.GetKeyDown(KeyCode.Delete)){
            audioManager.Play("rat_death");
        }
    }
}
