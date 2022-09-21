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

    IEnumerator GrowthSeed()
    {
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
        Debug.Log("물을 주었어요");
        if(other.tag=="Water")
        {
            onWater = true;
        }
    }
    //=================================================




}
