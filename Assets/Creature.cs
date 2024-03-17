using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public float moveSpeed = 1f;
    public GameObject body;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        FitBoxColliderToSprite();
    }

    void FitBoxColliderToSprite()
    {
        // body.transform.position = Vector3.zero; // this doesnt work right, but make sure the body is at 0,0,0
        SpriteRenderer renderer = body.GetComponent<SpriteRenderer>();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.size = new Vector2(renderer.size.x, renderer.size.y);
        collider.offset = new Vector2(0, 0);
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

    public void Stop()
    {
        MoveCreature(Vector3.zero);
    }

    // private void LaunchProjectile(InputAction.CallbackContext context)
    // {
    //     GameObject obj = Instantiate(pfProjectile, transform.position, Quaternion.identity);
    //     Rigidbody2D objRb = obj.GetComponent<Rigidbody2D>();

    //     Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //     mouseWorldPosition.z = transform.position.z; // Match the z-axis to the transform 
    //     Vector3 direction = (mouseWorldPosition - transform.position).normalized;

    //     objRb.AddForce(direction, ForceMode2D.Impulse);
    //     Destroy(obj, 5f);
    // }
}
