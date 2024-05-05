using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public abstract class Creature : MonoBehaviour
{
    [Header("Creature Stats")]
    public float moveSpeed = 1f;
    public int health = 5;
    public int maxHealth = 5;
    public int ammo = 5;
    public int maxAmmo = 10;
    public int attackDamage = 1;

    [Header("Projectile Config")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 1f;
    public float projectileRotationSpeed = 100f;
    public float projectileSize = 0.5f;
    public float projectileTTL = 5f;
    public Projectile.Type projectileType;

    private Rigidbody2D rb;
    

    public abstract void Die();

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
            projectile.Setup(direction, projectileType, projectileSpeed, projectileRotationSpeed, projectileSize, attackDamage, projectileTTL);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Stop()
    {
        MoveCreature(Vector3.zero);
    }
}
