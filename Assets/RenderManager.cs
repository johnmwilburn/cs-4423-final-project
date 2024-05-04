using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderManager : MonoBehaviour
{
    private new BoxCollider2D collider;
    private new SpriteRenderer renderer;

    public bool needsIllumination;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        if (renderer && collider)
        {
            FitBoxColliderToSprite();
        }

    }

    void Update()
    {
        if (needsIllumination)
        {
            renderer.enabled = false; // light source will set to true when illuminating
        }
    }

    void FitBoxColliderToSprite()
    {
        // body.transform.position = Vector3.zero; // this doesnt work right, but make sure the body is at 0,0,0 relative to the parent
        collider.size = new Vector2(renderer.size.x, renderer.size.y);
        collider.offset = new Vector2(0, 0);
    }

    public void EnableRenderer()
    {
        if (renderer.enabled == false)
        {
            renderer.enabled = true;
        }
    }

    public void SetColor(Color newColor)
    {
        renderer.color = newColor;
    }

    public SpriteRenderer GetRenderer()
    {
        return renderer;
    }
}
