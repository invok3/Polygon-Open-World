using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<string> consumables;
    [SerializeField] Interactable _interactable;
    [SerializeField] LootContainerController _holdInteractable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

}
