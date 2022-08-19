using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<string> consumables;
    [SerializeField] Interactable _interactable;
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
        _interactable = other.gameObject.GetComponent<Interactable>();
    }
    private void OnTriggerExit(Collider other)
    {
        _interactable = null;
    }

    public void Interact()
    {
        if (_interactable is null) return;
        
        _interactable.OfferItems();
    }
}
