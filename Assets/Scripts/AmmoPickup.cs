using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Creature>()?.creatureType == Creature.Type.Player)
        {
            other.GetComponent<Creature>().ammo += 5;
            AudioSourceManager.Instance.PlayClip("pickup");
            Destroy(gameObject);
        }
    }
}