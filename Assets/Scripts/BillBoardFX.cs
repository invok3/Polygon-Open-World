using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardFX : MonoBehaviour
{
    [SerializeField] Transform _mainCamera;
    [SerializeField] Transform _player;
    [SerializeField] float _distance;
    [SerializeField] CanvasGroup _canvasGroup;


    private void Awake()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = _mainCamera.rotation;
        _distance = Vector3.Distance(transform.position, _player.position);
    }
}
