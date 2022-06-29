using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class Border : MonoBehaviour
{
    public float Radius = 1.0f;
    public int NumPoints = 32;

    EdgeCollider2D EdgeCollider;
    LineRenderer borderLine;
    float CurrentRadius = 0.0f;


    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start()
    {
        CreateCircle();
    }

    /// <summary>
    /// Update this instance.
    /// </summary>
    void Update()
    {
        // If the radius or point count has changed, update the circle
        if (NumPoints != EdgeCollider.pointCount || CurrentRadius != Radius)
        {
            CreateCircle();
        }
    }

    /// <summary>
    /// Creates the circle.
    /// </summary>
    void CreateCircle()
    {
        Vector2[] edgePoints = new Vector2[NumPoints + 1];
        Vector3[] edgePoints3D = new Vector3[NumPoints + 1];
        EdgeCollider = GetComponent<EdgeCollider2D>();
        borderLine = GetComponent<LineRenderer>();

        for (int loop = 0; loop <= NumPoints; loop++)
        {
            float angle = (Mathf.PI * 2.0f / NumPoints) * loop;
            edgePoints[loop] = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * Radius;
            edgePoints3D[loop] = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * Radius;
        }

        EdgeCollider.points = edgePoints;
        borderLine.positionCount = edgePoints3D.Length;
        borderLine.SetPositions(edgePoints3D);
        CurrentRadius = Radius;
    }
}
