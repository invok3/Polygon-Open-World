using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class LootContainerController : MonoBehaviour
{
    [SerializeField] Transform lootDescription;
    [SerializeField] LineRenderer innerPillar;
    [SerializeField] LineRenderer innerRing;
    [SerializeField] LineRenderer outerRing;
    [SerializeField] Canvas canvas;
    [SerializeField] Transform containerPillar;
    [SerializeField] Transform dropSplash;
    [SerializeField] bool playerIsNear = true;
    [SerializeField] Color color;
    [SerializeField] Gradient colorGradiant = new Gradient();
    [SerializeField] Gradient fadeColorGradiant = new Gradient();
    [SerializeField] Gradient invisibleColorGradiant = new Gradient();
    [SerializeField] float key0Alpha;
    [SerializeField] float antiKey0Alpha;
    [SerializeField] bool satup = false;
    [SerializeField] Transform itemList;
    [SerializeField] ItemTile itemTile;
    [SerializeField] String[] items;


    private void Awake()
    {
        lootDescription = transform.Find("LootDescription");
        innerPillar = transform.Find("LootDescription/InnerPillar").GetComponent<LineRenderer>();
        canvas = transform.Find("LootDescription/InnerPillar/Canvas").GetComponent<Canvas>();
        innerRing = transform.Find("LootDescription/InnerRing").GetComponent<LineRenderer>();
        outerRing = transform.Find("OuterRing").GetComponent<LineRenderer>();
        containerPillar = transform.Find("ContainerPillar");
        dropSplash = transform.Find("DropSplash");
        itemList = transform.Find("LootDescription/InnerPillar/Canvas/ItemList");
    }

    // Start is called before the first frame update
    void Start()
    {
        //This is called if the Prefab wasn't Instainsiated, Default Values;
        if (!satup)
        {
            Setup(transform.position, items);

        }
    }

    // Update is called once per frame
    void Update()
    {
        LerpVisibility();

    }

    public void Setup(Vector3 newPosition, String[] newItems)
    {
        //TODO Deal With Items
        for (int i = 0; i < items.Length; i++)
        {
            itemTile.itemName = newItems[i];
            GameObject.Instantiate(itemTile, itemList.position + new Vector3(0f, i * 0.24f /*canvas scale * item scale*/, 0f),
                new Quaternion(), itemList );
        }

        //TODO Calculate Color of Highest Tier Item

        transform.position = newPosition;
        GradientColorKey[] colorKeys = new GradientColorKey[2];
        colorKeys[0].color = color;
        colorKeys[1].color = color;
        colorKeys[0].time = 0f;
        colorKeys[1].time = 1f;
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0].time = 0f;
        alphaKeys[0].alpha = 1f;
        alphaKeys[1].time = 1f;
        alphaKeys[1].alpha = 1f;


        colorGradiant.colorKeys = colorKeys;
        colorGradiant.alphaKeys = alphaKeys;

        fadeColorGradiant.colorKeys = colorKeys;
        alphaKeys[1].alpha = 0f;
        fadeColorGradiant.alphaKeys = alphaKeys;

        invisibleColorGradiant.colorKeys = colorKeys;
        alphaKeys[0].alpha = 0f;
        invisibleColorGradiant.alphaKeys = alphaKeys;

        containerPillar.GetComponent<LineRenderer>().colorGradient = fadeColorGradiant;
        innerPillar.colorGradient = colorGradiant;
        outerRing.colorGradient = colorGradiant;
        innerRing.colorGradient = colorGradiant;
        ParticleSystem[] splashModule = dropSplash.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particalSystem in splashModule)
        {
            MainModule splashMain = particalSystem.main;
            Color tColor = color;
            tColor.a = 0.165f;
            splashMain.startColor = tColor;
        }
        satup = true;
    }
    private void LerpVisibility()
    {
        key0Alpha = playerIsNear ? Mathf.LerpAngle(key0Alpha, 0f, Time.deltaTime * 10f) : Mathf.Lerp(key0Alpha, 1f, Time.deltaTime * 10f);
        antiKey0Alpha = !playerIsNear ? Mathf.LerpAngle(antiKey0Alpha, 0f, Time.deltaTime * 10f) : Mathf.Lerp(antiKey0Alpha, 1f, Time.deltaTime * 10f);
        key0Alpha = Mathf.Round(key0Alpha * 1000) / 1000;
        antiKey0Alpha = Mathf.Round(antiKey0Alpha * 1000) / 1000;

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0].time = 0f;
        alphaKeys[1].time = 1f;
        alphaKeys[0].alpha = key0Alpha;
        alphaKeys[1].alpha = 0f;
        fadeColorGradiant.alphaKeys = alphaKeys;
        containerPillar.GetComponent<LineRenderer>().colorGradient = fadeColorGradiant;

        //lootDescription.gameObject.SetActive(playerIsNear);
        //innerpillar
        alphaKeys[0].alpha = antiKey0Alpha;
        alphaKeys[1].alpha = antiKey0Alpha;
        colorGradiant.alphaKeys = alphaKeys;
        innerPillar.colorGradient = colorGradiant;

        //innerring
        innerRing.colorGradient = colorGradiant;
        //canvas
        canvas.GetComponent<CanvasGroup>().alpha = antiKey0Alpha;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerIsNear = false;
    }
}
