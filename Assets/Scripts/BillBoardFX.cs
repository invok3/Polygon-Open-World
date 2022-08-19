using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardFX : MonoBehaviour
{
    [SerializeField] Transform _mainCamera;


    private void Awake()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = _mainCamera.rotation;
    }
}
