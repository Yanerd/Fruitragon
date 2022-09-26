using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : MonoBehaviour
{
    MeshRenderer myMeshRenderer;

    [SerializeField] GameObject Stem;
    [SerializeField] GameObject Dragon;
    [SerializeField] GameObject Effect;
    [SerializeField] DropFruit[] fruit;

    [SerializeField] public bool onWater=false;


    private void Awake()
    {
        myMeshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if(onWater==true)
        {
            startGrowth();
            onWater = false;
        }
    }

    public void startGrowth()
    {
        StartCoroutine(GrowthSeed());
    }
    int a;
    IEnumerator GrowthSeed()
    {
        if (10<a) yield break;

        yield return new WaitForSeconds(3f);
        myMeshRenderer.enabled = false;
        Stem.gameObject.SetActive(true);
        
        StartCoroutine(onEffect());
        for (int i = 0; i < fruit.Length; i++)
        {
            fruit[i].SendMessage("dropObject", SendMessageOptions.DontRequireReceiver);
        }
       
        yield return new WaitForSeconds(3f);

        StartCoroutine(onEffect());

        Stem.gameObject.SetActive(false);
        Dragon.gameObject.SetActive(true);

    }
    IEnumerator onEffect()
    {
        Effect.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        Effect.gameObject.SetActive(false);
    }

    //=================================================
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Water")
        {
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("¹°À» Áá¾î¿ä");
                onWater = true;
            }
        }


    }
    //=================================================




}
