using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : MonoBehaviour
{
    MeshRenderer myMeshRenderer;

    [SerializeField] MeshRenderer Seed;
    [SerializeField] GameObject Stem;
    [SerializeField] GameObject Dragon;
    [SerializeField] GameObject Effect;
    [SerializeField] DropFruit[] fruit;

    [SerializeField] public bool onWater=false;


    private void Update()
    {
        if(onWater == true)
        {
            onWater = false;
            startGrowth();
        }
    }

    public void startGrowth()
    {
        Debug.Log("자라나라");
        StartCoroutine(GrowthSeed());
    }
    IEnumerator GrowthSeed()
    {
        yield return new WaitForSeconds(3f);
        Seed.enabled = false;
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

   



}
