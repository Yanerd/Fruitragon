using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : MonoBehaviour
{

    [Header("VegetableObject")]
    [SerializeField] MeshRenderer Seed;
    [SerializeField] GameObject Stem;
    [SerializeField] GameObject Effect;

    [Header("DropFruit")]
    [SerializeField] DropFruit[] fruit;

    [Header("DragonPrefab")]
    [SerializeField] GameObject Dragon;

    [Header("DragonState")]
    [SerializeField] public bool onWater=false;
    [SerializeField] bool GrownDragon;

    
    public void startGrowth()
    {
        if (GrownDragon == true) return;
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

        if(GrownDragon == false)
        {
            InstantiateDragon();
        }
    }

    void InstantiateDragon()
    {
        GrownDragon = true;
        GameObject instDragon = ObjectPoolingManager.inst.Instantiate(Dragon, transform.position, Quaternion.Euler(0, -180f, 0), ObjectPoolingManager.inst.PoolingZone);

        AllDragonCount(instDragon);
    }
    void AllDragonCount(GameObject instDragon)
    {
        string prefabId = instDragon.name.Replace("(Clone)", "");

        if (prefabId == "D_Potatagon")
        {
            DefenseUIManager.INSTANCE.potatoDragonList.Add(instDragon);
        }
        else if(prefabId == "D_Appleagon")
        {
            DefenseUIManager.INSTANCE.appleDragonList.Add(instDragon);
        }
        else if (prefabId == "D_Carrotagon")
        {
            DefenseUIManager.INSTANCE.carrotDragonList.Add(instDragon);
        }
        else if (prefabId == "D_Cabbagon")
        {
            DefenseUIManager.INSTANCE.cabbageDragonList.Add(instDragon);
        }
        else if (prefabId == "D_Eggplagon")
        {
            DefenseUIManager.INSTANCE.eggplantDragonList.Add(instDragon);
        }

    }

    IEnumerator onEffect()
    {
        Effect.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        Effect.gameObject.SetActive(false);
    }

   




}
