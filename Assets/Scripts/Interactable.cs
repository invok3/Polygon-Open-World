using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private enum InteractableType { Door };
    [SerializeField] private bool _locked = true;
    [SerializeField] private InteractableType _type = InteractableType.Door;
    [SerializeField] private float _timeBarrier = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_timeBarrier > 0f)
        {
            _timeBarrier -= Time.deltaTime;
        }
        if (_timeBarrier < 0f) _timeBarrier = 0f;
    }

    private void ToggleDoorOpen()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Openable _openable = transform.GetChild(i).GetComponent<Openable>();
            if (_openable is not null) _openable.Toggle();
        }
    }

    public void OfferItems()
    {
        if (_timeBarrier > 0f) return;
        _timeBarrier = 1f;
        if(_type == InteractableType.Door && _locked == false)
        {
            ToggleDoorOpen();
            return;
        }
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        for (int i = 0; i < player.consumables.Count; i++)
        {
            if(player.consumables[i] == "Key123")
            {
                player.consumables.RemoveAt(i);
                ToggleDoorOpen();
                _locked = false;
                break;
            }
        }
    }
}
