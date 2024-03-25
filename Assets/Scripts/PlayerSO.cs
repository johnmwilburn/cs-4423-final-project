using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To hold player stats for communication between player and UI

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public int health;
    public int maxHealth;
    public int ammo;
    public int maxAmmo;
}
