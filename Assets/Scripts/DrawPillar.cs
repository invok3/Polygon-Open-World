using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPillar : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, lineRenderer.GetPosition(0) + transform.position);
        lineRenderer.SetPosition(1, lineRenderer.GetPosition(1) + transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
