using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemTile : MonoBehaviour
{
    public string itemName;
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI txt = transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        txt.text = itemName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
