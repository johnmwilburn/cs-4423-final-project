using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturePlayer : Creature
{
    [Header("Functional References")]
    public MissionSO missionSO;
    public PlayerSO playerSO;

    protected override void Awake()
    {
        base.Awake();
        projectileType = Projectile.Type.Player;
    }

    public void Update()
    {
        playerSO.health = health;
        playerSO.maxHealth = maxHealth;
        playerSO.ammo = ammo;
        playerSO.maxAmmo = maxAmmo;
    }

    public override void Die()
    {
        // missionSO.FailMission();
    }
}
