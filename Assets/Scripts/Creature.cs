using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class Creature : MonoBehaviour
{
    public enum Type { Player, Enemy };

    [Header("Creature Stats")]
    public Type creatureType = Type.Enemy;
    public float moveSpeed = 1f;
    public int health = 5;
    public int maxHealth = 5;
    public int ammo = 5;
    public int maxAmmo = 5;
    public int attackDamage = 1;

    [Header("Projectile Config")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 1f;
    public float projectileRotationSpeed = 100f;
    public float projectileSize = 0.5f;
    public float projectileTTL = 5f;

    [Header("Functional References")]
    public MissionSO missionSO;
    public PlayerSO playerSO;
    public GameObject body;
    public FieldOfView fieldOfView;
    public LayerMask lightMask;
    public GameObject ammoPickupPrefab;

    private Rigidbody2D rb;
    private RenderManager renderManager;
    public Vector3 facingDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        renderManager = GetComponent<RenderManager>();
    }
    void Update()
    {
        if (creatureType == Type.Player && playerSO != null)
        {
            playerSO.health = health;
            playerSO.maxHealth = maxHealth;
            playerSO.ammo = ammo;
            playerSO.maxAmmo = maxAmmo;
        }

        if (creatureType == Type.Player)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 playerPosition = new Vector3(transform.position.x, transform.position.y, 0);
            facingDirection = mousePosition - playerPosition;
        }

        if (fieldOfView)
        {
            fieldOfView.SetOrigin(transform.position);
            fieldOfView.SetAimDirection(facingDirection);
        }
    }

    public void MoveCreature(Vector2 direction)
    {
        rb.velocity = direction * moveSpeed;
    }

    public void MoveCreatureToward(Vector2 target)
    {
        Vector3 direction = target - new Vector2(transform.position.x, transform.position.y);
        MoveCreature(direction.normalized);
    }

    public void AttackRanged(Vector3 direction)
    {
        if (ammo > 0)
        {
            ammo--;

            GameObject obj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile projectile = obj.GetComponent<Projectile>();
            projectile.Setup(direction, creatureType, projectileSpeed, projectileRotationSpeed, projectileSize, attackDamage, projectileTTL);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            AudioSourceManager.Instance.PlayClip("hit");
        }
    }

    public void Die()
    {
        if (creatureType == Type.Enemy)
        {
            missionSO.IncrementEnemiesKilled();
            Destroy(gameObject);

            if (ammoPickupPrefab)
            {
                Instantiate(ammoPickupPrefab, transform.position, Quaternion.identity);
            }

            AudioSourceManager.Instance.PlayClip("rat_death");
        }
        else if (creatureType == Type.Player)
        {
            // missionSO.FailMission();
        }
    }

    public void Stop()
    {
        MoveCreature(Vector3.zero);
    }
}
