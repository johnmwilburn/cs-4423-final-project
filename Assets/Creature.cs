using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector3 direction)
    {
        rb.velocity = direction * moveSpeed;
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
