using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCanvasScript : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        var x = mainCamera.transform.rotation;
        x.x = 0f;
        x.z = 0f;
        transform.rotation = x;
    }
}
