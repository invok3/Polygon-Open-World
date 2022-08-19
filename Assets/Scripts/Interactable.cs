using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    private enum InteractableType { Door };
    [SerializeField] private bool _locked = true;
    [SerializeField] private bool _isOpen = false;
    [SerializeField] private bool _canAfford = false;
    [SerializeField] private int _requiredItemIndex;
    [SerializeField] private InteractableType _type = InteractableType.Door;
    [SerializeField] private float _timeBarrier = 1.0f;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float showHint = 0f;
    [SerializeField] private string requiredItemTag;
    [SerializeField] private TextMeshProUGUI hintText;

    private void Awake()
    {
        canvasGroup = transform.GetComponentInChildren<CanvasGroup>();
        hintText = transform.Find("Canvas/HintText").GetComponent<TextMeshProUGUI>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CheckRequiredItem();
            SetHintText();
            showHint = 1f;
        }
    }

    private void CheckRequiredItem()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        for (int i = 0; i < player.consumables.Count; i++)
        {
            if(player.consumables[i] == requiredItemTag)
            {
                _canAfford = true;
                _requiredItemIndex = i;
                return;
            }
        }
    }

    private void SetHintText()
    {
        
        if (_locked)
        {
            if(_canAfford)
            {
                hintText.text = "Unlock";
            } else
            {
                hintText.text = "Locked";
            }
        } else
        {
            if(_isOpen)
            {
                hintText.text = "Close";
            } else
            {
                hintText.text = "Open";
            }
        }
        //locked - no item
        //locked - has item
        //unlocked open
        //unlocked close

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Player")
        {
            showHint = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_timeBarrier > 0f)
        {
            _timeBarrier -= Time.deltaTime;
        }
        if (_timeBarrier < 0f) _timeBarrier = 0f;
        canvasGroup.alpha += (showHint - canvasGroup.alpha) * Time.deltaTime * 10f;
    }

    private void ToggleDoorOpen()
    {
        _isOpen = !_isOpen;
        SetHintText();
        for (int i = 0; i < transform.childCount; i++)
        {
            Openable _openable = transform.GetChild(i).GetComponent<Openable>();
            if (_openable is not null) _openable.Toggle();
        }
    }


    //Interact
    public void Interact()
    {
        if (_timeBarrier > 0f) return;
        _timeBarrier = 1f;
        switch (_type)
        {
            case InteractableType.Door:
                DoorInteract();
                break;
            default:
                break; 
        }
    }

    private void DoorInteract()
    {
        //isDoor, isUnlocked
        if (!_locked)
        {
            ToggleDoorOpen();
            return;
        }

        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //isDoor, isLocked
        if (_canAfford)
        {

            player.consumables.RemoveAt(_requiredItemIndex);
            _locked = false;
            ToggleDoorOpen();
        }
    }
}
