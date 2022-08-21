using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRing : MonoBehaviour
{
    [SerializeField] int pointCount = 32;
    [SerializeField] float radius = 1f;
    [SerializeField] LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = pointCount + 2;
        for (int i = 0; i < pointCount+2; i++)
        {
            float cfp = (float) i / (pointCount);
            float cR = cfp * 2 * Mathf.PI;
            float x = Mathf.Cos(cR);
            float y = Mathf.Sin(cR);
            float scaledX = x * radius;
            float scaledy = y * radius;
            lineRenderer.SetPosition(i, 
                new Vector3(scaledX + transform.position.x,  0.05f + transform.position.y, scaledy + transform.position.z)
                );
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
