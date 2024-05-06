using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum Type { Player, Enemy };
    public AudioSourceManager audioSourceManager;
    private float moveSpeed;
    private float rotationSpeed;
    private float ttl;
    private float timer;
    private int damage;
    private float size;
    private Vector3 direction;
    private Rigidbody2D rb;
    private Type type;

    public void Setup(Vector3 direction, Type type, float moveSpeed, float rotationSpeed, float size, int damage, float ttl)
    {

        this.direction = direction;
        this.type = type;
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
        if (other.gameObject.tag != "ProjectileHits")
        {
            return;
        }

        Creature targetCreature = other.gameObject.GetComponent<Creature>();

        if (targetCreature?.projectileType == type)
        {
            return;
        }

        if (targetCreature)
        {
            targetCreature.TakeDamage(damage);
        }

        AudioSourceManager.Instance.PlayClip("hit");
        Destroy(gameObject);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > ttl)
        {
            Destroy(gameObject);
        }

        rb.velocity = direction * moveSpeed;

        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
