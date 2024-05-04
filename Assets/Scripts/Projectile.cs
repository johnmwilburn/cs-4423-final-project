using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public AudioSourceManager audioSourceManager;
    public LayerMask collisionMask;
    private float moveSpeed;
    private float rotationSpeed;
    private float ttl;
    private float timer;
    private int damage;
    private float size;
    private Vector3 direction;
    private Rigidbody2D rb;
    private Creature.Type projectileType;

    public void Setup(Vector3 direction, Creature.Type type, float moveSpeed, float rotationSpeed, float size, int damage, float ttl)
    {
        this.direction = direction;
        this.projectileType = type;
        this.moveSpeed = moveSpeed;
        this.rotationSpeed = rotationSpeed;
        this.size = size;
        this.damage = damage;
        this.ttl = ttl;

        rb = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(size, size, 1f);
        transform.Rotate(Vector3.forward, Random.Range(0, 360));
        timer = 0;

        AudioSourceManager.Instance.PlayClip("shoot");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Creature>()?.creatureType == projectileType)
        {
            return;
        }

        other.gameObject.GetComponent<Creature>()?.TakeDamage(damage);
        if (!other.gameObject.GetComponent<Projectile>())
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > ttl)
        {
            Destroy(gameObject);
        }

        // Move the projectile
        rb.velocity = direction * moveSpeed;

        // Rotate the projectile
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
