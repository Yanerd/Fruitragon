using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    #region 싱글톤

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
    [SerializeField] GameObject[] all_Prefab;
    [SerializeField] MeshRenderer[] PrefabMesh;
    //0.감자,1.사과,2.당근,3.양배추,4.가지,5.집

    List<GameObject> InstGameObjects = new List<GameObject>();

    public List<GameObject> waterList = new List<GameObject>();


    void Awake()
    {
        for (int i = 0; i < all_Prefab.Length; i++)
        {
            GameObject InstObject = Instantiate(all_Prefab[i], PoolingZone.position, Quaternion.identity);
            InstGameObjects.Add(InstObject);
            InstGameObjects[i].SetActive(false);
        }
    }

    public void Objectapperar(Vector3 tr)
    {
        if (ButtonManager.inst.OnHouse == true)
        {
            InstGameObjects[5].transform.position = tr+new Vector3(0,0.3f,0);
            InstGameObjects[5].SetActive(true);
        }
        else if (ButtonManager.inst.OnPotato == true)
        {
            InstGameObjects[0].transform.position = tr + new Vector3(0, 0.3f, 0);
            InstGameObjects[0].SetActive(true);
        }
        else if (ButtonManager.inst.OnWell == true)
        {
            InstGameObjects[6].transform.position = tr + new Vector3(0, 0.5f, 0);
            InstGameObjects[6].SetActive(true);
        }

    }

    public void ObjectDisappear()
    {
        if (ButtonManager.inst.OnHouse == false)
        {
            InstGameObjects[5].transform.position = PoolingZone.position;
            InstGameObjects[5].SetActive(false);
        }
        if (ButtonManager.inst.OnPotato == false)
        {
            InstGameObjects[0].transform.position = PoolingZone.position;
            InstGameObjects[0].SetActive(false);
        }
        if (ButtonManager.inst.OnWell == false)
        {
            InstGameObjects[6].transform.position = PoolingZone.position;
            InstGameObjects[6].SetActive(false);
        }
    }


}
