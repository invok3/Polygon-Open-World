using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openable : MonoBehaviour
{
    [SerializeField] private bool isOpen;
    [SerializeField] private Quaternion openRots;
    [SerializeField] private Quaternion closedRots;
    [SerializeField] private Quaternion currentRots;
    [SerializeField] private float _rotationVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Toggle()
    {
        isOpen = !isOpen;
    }
    // Update is called once per frame
    void Update()
    {
        float _targetRotation;
        if (isOpen)
        {
            //sets target rotation to force the door open
            _targetRotation = openRots.eulerAngles.y; 
        }
        else
        {
            //sets target rotation to force the door close
            _targetRotation = closedRots.eulerAngles.y;
        }
        //somehow it smooths the rotation from y1, to y2 using velocity reference over float time
        //idc how it works but its good since you cant use - or / with quaternions.

        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                    0.5f);
        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
    }
}
//TODO Stop the door from pushing the player so hard, it might Hurt!
//TODO Use Raycast to check if player has eye contact with the door to prevent using it throw walls