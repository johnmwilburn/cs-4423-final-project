using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public SpriteRenderer body;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Creature>()?.creatureType == Creature.Type.Player)
        {
            other.GetComponent<Creature>().ammo += 5;
            Destroy(gameObject);
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
    }
}