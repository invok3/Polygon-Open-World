using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SplashProperties : MonoBehaviour
{
    [SerializeField] float simulationSpeed = 4f;
    [SerializeField] Color color;


    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        MainModule mMain = GetComponent<ParticleSystem>().main;
        mMain.simulationSpeed = simulationSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
