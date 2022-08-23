using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Player : MonoBehaviour
{
    public List<string> consumables;
    [SerializeField] Interactable _interactable;
    [SerializeField] LootContainerController _holdInteractable;
    [SerializeField] Transform weaponPlaceHolder;
    public bool isAiming = false;
    public bool isArmed = false;
    [SerializeField] Transform aimPositionHolder;
    [SerializeField] Transform hipPositionHolder;
    [SerializeField] RigLayer rigAimConstrained;
    [SerializeField] RigLayer rigHipConstrained;
    [SerializeField] RigLayer rigArmConstrained;

    enum WeaponParent
    {
        LeftHand, RightHand, Holster
    }

    // Start is called before the first frame update
    void Start()
    {
        weaponPlaceHolder = GameObject.FindGameObjectWithTag("WeaponPlaceholder").transform;
        aimPositionHolder = GameObject.FindGameObjectWithTag("AimPositionHolder").transform;
        hipPositionHolder = GameObject.FindGameObjectWithTag("HipPositionHolder").transform;
        rigHipConstrained = GetComponent<RigBuilder>().layers[0];
        rigAimConstrained = GetComponent<RigBuilder>().layers[1];
        rigArmConstrained = GetComponent<RigBuilder>().layers[2];
    }

    // Update is called once per frame
    void Update()
    {
        rigArmConstrained.active = isArmed;
        rigHipConstrained.active = isArmed;
        rigAimConstrained.active = isAiming && isArmed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Door")
        _interactable = other.gameObject.GetComponent<Interactable>();
        else
            _holdInteractable = other.gameObject.GetComponent<LootContainerController>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Door")
            _interactable = null;
        else
            _holdInteractable = null;
    }

    public void Interact()
    {
        if (_interactable is null) return;
        _interactable.Interact();
    }
    public void HeldInteract()
    {
        if (_holdInteractable is null) return;
        _holdInteractable.LootAll();
        _holdInteractable = !_holdInteractable.isActiveAndEnabled ? null : _holdInteractable;
    }


    private void ChangeParent(Transform weapon, Transform parent)
    {
        weapon.SetParent(parent);
    }


}
