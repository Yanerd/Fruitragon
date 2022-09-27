using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ObjectPoolingManager : MonoBehaviour
{
    #region ΩÃ±€≈Ê

    private static ObjectPoolingManager instance = null;
    public static ObjectPoolingManager inst
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ObjectPoolingManager>();
                if (instance == null)
                {
                    instance = new GameObject("ObjectPoolingManager").AddComponent<ObjectPoolingManager>();
                }
            }
            return instance;
        }
    }

    #endregion

    [SerializeField] public Transform PoolingZone;

    private List<GameObject> InstAlphaObjects = new List<GameObject>();//Save Alpha PrefabList

    private Dictionary<string, List<GameObject>> instList = new Dictionary<string, List<GameObject>>();
    //Call instantiate
    public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation,Transform parent)
    {
        List<GameObject> list = null;
        GameObject instance = null;

        bool listCheck = instList.TryGetValue(prefab.name, out list);
        if (listCheck==false)
        {
            list = new List<GameObject>();
            instList.Add(prefab.name, list);
        }
        if (list.Count == 0)
        {
            instance = GameObject.Instantiate(prefab, position, rotation, parent);
        }
        else if (list.Count > 0)
        {
            instance = list[0];
            instance.transform.position = position + new Vector3(0, 0.3f, 0);
            instance.transform.rotation = rotation;
            list.RemoveAt(0);
        }

        if (instance != null)
        {
            instance.gameObject.SetActive(true);
            return instance;
        }
        else
        {
            return null;
        }
    }
    //Call bringObject & Pooling
    public void Destroy(GameObject Prefab)
    {
        List<GameObject> list = null;
        string prefabId = Prefab.name.Replace("(Clone)", "");
        bool listCached = instList.TryGetValue(prefabId, out list);
        if (listCached==false)
        {
            Debug.LogError("Not Found " + Prefab.name);
            return;
        }

        Prefab.transform.position = PoolingZone.position;
        Prefab.SetActive(false);
        list.Add(Prefab);

    }

    //BuldingMode Pooling
    public void inst_AlphaPrefab(GameObject[] prefab)
    {
        for (int i = 0; i < prefab.Length; i++)
        {
            GameObject instObject = Instantiate(prefab[i], PoolingZone.position, Quaternion.identity);
            InstAlphaObjects.Add(instObject);
            //InstAlphaObjects[i].SetActive(false);
        }
    }

    public void Objectapperear(Vector3 tr)
    {
        if (ButtonManager.inst.OnHouse == true)
        {
            InstAlphaObjects[5].transform.position = tr + new Vector3(0, 0.3f, 0);
            //InstAlphaObjects[5].SetActive(true);
        }
        else if (ButtonManager.inst.OnWell == true)
        {
            InstAlphaObjects[6].transform.position = tr + new Vector3(0, 0.3f, 0);
            //InstAlphaObjects[0].SetActive(true);
        }
        else if (ButtonManager.inst.OnPotato == true)
        {
            InstAlphaObjects[0].transform.position = tr + new Vector3(0, 0.3f, 0);
            //InstAlphaObjects[0].SetActive(true);
        }
        else if (ButtonManager.inst.OnApple==true)
        {
            InstAlphaObjects[1].transform.position = tr + new Vector3(0, 0.4f, 0);
            //InstAlphaObjects[0].SetActive(true);
        }
        else if (ButtonManager.inst.OnCabbage == true)
        {
            InstAlphaObjects[2].transform.position = tr + new Vector3(0, 0.4f, 0);
            //InstAlphaObjects[0].SetActive(true);
        }
        else if (ButtonManager.inst.OnCarrot == true)
        {
            InstAlphaObjects[3].transform.position = tr + new Vector3(0, 0.4f, 0);
            //InstAlphaObjects[0].SetActive(true);
        }
        else if (ButtonManager.inst.OnEggplant == true)
        {
            InstAlphaObjects[4].transform.position = tr + new Vector3(0, 0.5f, 0);
            //InstAlphaObjects[6].SetActive(true);
        }
        else if (ButtonManager.inst.WaterRay == true)
        {
            InstAlphaObjects[7].transform.position = tr + new Vector3(0, 0.3f, 0);

        }
        else if (ButtonManager.inst.OnWater == true)
        {
            InstAlphaObjects[8].transform.position = tr + new Vector3(0, 0.3f, 0);

        }
    }
    public void ObjectDisappear()
    {
        
        if (ButtonManager.inst.OnHouse == false)
        {
            InstAlphaObjects[5].transform.position = PoolingZone.position;
            //InstAlphaObjects[5].SetActive(false);
        }
        if (ButtonManager.inst.OnPotato == false)
        {
            InstAlphaObjects[0].transform.position = PoolingZone.position;
            //InstAlphaObjects[0].SetActive(false);
        }
        if (ButtonManager.inst.OnWell == false)
        {
            InstAlphaObjects[6].transform.position = PoolingZone.position;
           //InstAlphaObjects[6].SetActive(false);
        }
        if (ButtonManager.inst.OnApple == false)
        {
            InstAlphaObjects[1].transform.position = PoolingZone.position;
            //InstAlphaObjects[6].SetActive(false);
        }
        if (ButtonManager.inst.OnCabbage == false)
        {
            InstAlphaObjects[2].transform.position = PoolingZone.position;
            //InstAlphaObjects[6].SetActive(false);
        }
        if (ButtonManager.inst.OnCarrot == false)
        {
            InstAlphaObjects[3].transform.position = PoolingZone.position;
            //InstAlphaObjects[6].SetActive(false);
        }
        if (ButtonManager.inst.OnEggplant == false)
        {
            InstAlphaObjects[4].transform.position = PoolingZone.position;
            //InstAlphaObjects[6].SetActive(false);
        }
        if (ButtonManager.inst.WaterRay == false)
        {
            InstAlphaObjects[7].transform.position = PoolingZone.position;

        }
        if (ButtonManager.inst.OnWater == false)
        {
            InstAlphaObjects[8].transform.position = PoolingZone.position;

        }

    }

    


   



    









    //=================================================





    //=================================================
}
