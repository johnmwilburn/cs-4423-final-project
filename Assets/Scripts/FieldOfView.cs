using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public LayerMask obstacles;
    private Mesh mesh;
    private MeshFilter meshFilter;
    private Vector3 origin;
    private float startingAngle;
    private float fov;

    Vector3 GetVectorFromAngle(float angle)
    {
        // angle =  0 -> 360
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    private void Start()
    {
        origin = Vector3.zero;
        startingAngle = 0f;
        fov = 90f;
        meshFilter = GetComponent<MeshFilter>();
        mesh = new Mesh();
        meshFilter.mesh = mesh;
    }
    private void LateUpdate()
    {
        meshFilter?.sharedMesh.RecalculateBounds();
        int rayCount = 50;
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;
        float viewDistance = 5f;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1]; // 1 for the origin, and 1 for the 'zero ray'
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3]; // 1 polygon for each ray, so *3

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, obstacles);
            Debug.DrawRay(origin, GetVectorFromAngle(angle) * viewDistance);
            if (raycastHit2D.collider == null)
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = raycastHit2D.point;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = (GetAngleFromVectorFloat(aimDirection) - fov / 2f + 90f ) % 360;
    }
}