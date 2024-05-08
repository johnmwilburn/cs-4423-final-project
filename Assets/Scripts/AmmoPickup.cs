using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        CreaturePlayer creaturePlayer = other.GetComponent<CreaturePlayer>();
        if (creaturePlayer)
        {
            creaturePlayer.AddAmmo(5);
            AudioSourceManager.Instance.PlayClip("pickup");
            Destroy(this.gameObject);
        }
    }
}