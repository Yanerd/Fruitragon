using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : MonoBehaviour
{
    MeshRenderer myMeshRenderer;

    [SerializeField] MeshRenderer Seed;
    [SerializeField] GameObject Stem;
    [SerializeField] GameObject Effect;
    [SerializeField] DropFruit[] fruit;

    [SerializeField] GameObject Dragon;

    [SerializeField] public bool onWater=false;

    bool GrownDragon;
  
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
        ObjectPoolingManager.inst.Instantiate(Dragon, transform.position, Quaternion.Euler(0, -180f, 0), ObjectPoolingManager.inst.PoolingZone);

        AllDragonCount(Dragon.name);
    }
    void AllDragonCount(string prefabname)
    {
        ObjectPoolingManager.inst.totalDragonCount++;
        if(prefabname=="PotatoDragon")
        {
            ObjectPoolingManager.inst.potatoDragonCount++;
        }
        else if(prefabname == "Applagon")
        {
            ObjectPoolingManager.inst.AppleDragonCount++;
        }
        else if (prefabname == "Carragon")
        {
            ObjectPoolingManager.inst.CarrotDragonCount++;
        }
        else if (prefabname == "Cabbagon")
        {
            ObjectPoolingManager.inst.CabbageDragonCount++;
        }
        else if (prefabname == "Eggplagon")
        {
            ObjectPoolingManager.inst.EggplantDragonCount++;
        }




    }



    IEnumerator onEffect()
    {
        Effect.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        Effect.gameObject.SetActive(false);
    }

   



}
