using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureEnemy : Creature
{
    [Header("Functional References")]
    public MissionSO missionSO;
    public GameObject ammoPickupPrefab;

    protected override void Awake()
    {
        base.Awake();
        projectileType = Projectile.Type.Enemy;
    }

    public override void Die()
    {
        missionSO.IncrementEnemiesKilled();
        Destroy(gameObject);

        if (ammoPickupPrefab)
        {
            Instantiate(ammoPickupPrefab, transform.position, Quaternion.identity);
        }

        AudioSourceManager.Instance.PlayClip("rat_death");
    }
}
