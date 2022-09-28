using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayClick : MonoBehaviour
{
    [Header("Vegetable_Prefab")]
    [SerializeField] GameObject potatoPrefab;
    [SerializeField] GameObject ApplePrefab;
    [SerializeField] GameObject CarrotPrefab;
    [SerializeField] GameObject CabbagePrefab;
    [SerializeField] GameObject EggplantPrefab;

    [Header("Warter")]
    [SerializeField] GameObject waterPrefab;
    [Header("Building_Prefab")]
    [SerializeField] GameObject housePrefab;
    [SerializeField] GameObject WellPrefab;
    [Header("AlphaPrefab_List")]
    [SerializeField] GameObject[] all_Alpha_Prefab;
    CursorChange myCursor;

    private int mask; //cullingMask plag Save Value
    private int mask1;
    private Vector3 FirstRayPosition; //FirstRay hit point Position

    private void Awake()
    {
        myCursor = FindObjectOfType<CursorChange>();
        mask = Camera.main.cullingMask = (1 << 9);
        mask1= Camera.main.cullingMask = (1 << 7);
        ObjectPoolingManager.inst.inst_AlphaPrefab(all_Alpha_Prefab);
    }

    void Update()
    {
       
        if (DefenseUIManager.INSTANCE.buildingMode == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                if (hit.transform.name == "NoBuildingZone" || hit.transform.tag == "OnGround")
                {
                    myCursor.noBuildingZoneCursor();
                }
                if(hit.transform.name == "UiZone")
                {
                    myCursor.BasicCursor();
                }
                if (hit.transform.tag == "GroundEmpty")
                {
                    myCursor.BasicCursor();
                    if (DefenseUIManager.INSTANCE.OnHouse == true || DefenseUIManager.INSTANCE.OnWell == true)
                    {
                        myCursor.BuildingCursor();
                    }
                    if (DefenseUIManager.INSTANCE.OnApple == true || DefenseUIManager.INSTANCE.OnCabbage == true ||
                                DefenseUIManager.INSTANCE.OnCarrot == true || DefenseUIManager.INSTANCE.OnEggplant == true ||
                                DefenseUIManager.INSTANCE.OnPotato == true)
                    {
                        myCursor.VegetableCursor();
                    }
                }
            }
        }


        if ( DefenseUIManager.INSTANCE.buildingMode == true     &&
           ( DefenseUIManager.INSTANCE.OnHouse == true       ||
             DefenseUIManager.INSTANCE.OnWell == true        ||
             DefenseUIManager.INSTANCE.OnApple == true       ||
             DefenseUIManager.INSTANCE.OnCabbage == true     ||
             DefenseUIManager.INSTANCE.OnCarrot == true      ||
             DefenseUIManager.INSTANCE.OnEggplant == true    ||
             DefenseUIManager.INSTANCE.OnPotato == true))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);
            if (Physics.Raycast(ray, out hit, 100f, mask))
            {
                

                if (FirstRayPosition == Vector3.zero)
                {
                    FirstRayPosition = hit.transform.position;
                }
                if (FirstRayPosition != hit.transform.position && hit.transform.gameObject.tag == "GroundEmpty")
                {
                    ObjectPoolingManager.inst.Objectapperear(hit.transform.position);
                    FirstRayPosition = hit.transform.position;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.transform.tag == "OnGround") return;

                    if (DefenseUIManager.INSTANCE.OnHouse == true)
                    {
                        DefenseUIManager.INSTANCE.OnHouse = false;
                        ObjectPoolingManager.inst.Instantiate(housePrefab, hit.transform.position, Quaternion.identity, ObjectPoolingManager.inst.PoolingZone);
                        ObjectPoolingManager.inst.ObjectDisappear();
                    }
                    else if (DefenseUIManager.INSTANCE.OnPotato == true)
                    {
                        DefenseUIManager.INSTANCE.OnPotato = false;
                        ObjectPoolingManager.inst.Instantiate(potatoPrefab, hit.transform.position + new Vector3(0, 0.33f, 0), Quaternion.identity, ObjectPoolingManager.inst.PoolingZone);
                        ObjectPoolingManager.inst.ObjectDisappear();
                    }
                    else if (DefenseUIManager.INSTANCE.OnWell == true )
                    {
                        DefenseUIManager.INSTANCE.OnWell = false;
                        ObjectPoolingManager.inst.Instantiate(WellPrefab, hit.transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity, ObjectPoolingManager.inst.PoolingZone);
                        ObjectPoolingManager.inst.ObjectDisappear();
                    }
                    else if (DefenseUIManager.INSTANCE.OnApple == true)
                    {
                        DefenseUIManager.INSTANCE.OnApple = false;
                        ObjectPoolingManager.inst.Instantiate(ApplePrefab, hit.transform.position+new Vector3(0,0.33f,0), Quaternion.identity, ObjectPoolingManager.inst.PoolingZone);
                        ObjectPoolingManager.inst.ObjectDisappear();
                    }
                    else if (DefenseUIManager.INSTANCE.OnCabbage == true)
                    {
                        DefenseUIManager.INSTANCE.OnCabbage = false;
                        ObjectPoolingManager.inst.Instantiate(CabbagePrefab, hit.transform.position + new Vector3(0, 0.33f, 0), Quaternion.identity, ObjectPoolingManager.inst.PoolingZone);
                        ObjectPoolingManager.inst.ObjectDisappear();
                    }
                    else if (DefenseUIManager.INSTANCE.OnCarrot == true )
                    {
                        DefenseUIManager.INSTANCE.OnCarrot = false;
                        ObjectPoolingManager.inst.Instantiate(CarrotPrefab, hit.transform.position + new Vector3(0, 0.33f, 0), Quaternion.identity, ObjectPoolingManager.inst.PoolingZone);
                        ObjectPoolingManager.inst.ObjectDisappear();
                    }
                    else if (DefenseUIManager.INSTANCE.OnEggplant == true)
                    {
                        DefenseUIManager.INSTANCE.OnEggplant = false;
                        ObjectPoolingManager.inst.Instantiate(EggplantPrefab, hit.transform.position + new Vector3(0, 0.33f, 0), Quaternion.identity, ObjectPoolingManager.inst.PoolingZone);
                        ObjectPoolingManager.inst.ObjectDisappear();
                    }
                }
            }
        }

        if (Input.GetMouseButton(0) && DefenseUIManager.INSTANCE.buildingMode == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.transform.tag=="well")
                {
                    DefenseUIManager.INSTANCE.WaterRay = true;
                    myCursor.WarterCursor();
                }
                if (FirstRayPosition == Vector3.zero)
                {
                    FirstRayPosition = hit.transform.position;
                }
                if (FirstRayPosition != hit.transform.position &&
                hit.transform.gameObject.tag == "GroundEmpty" || hit.transform.gameObject.tag == "OnGround")
                {
                    ObjectPoolingManager.inst.Objectapperear(hit.transform.position);
                    FirstRayPosition = hit.transform.position;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && DefenseUIManager.INSTANCE.buildingMode == false && DefenseUIManager.INSTANCE.WaterRay == true)
        {
            myCursor.BasicCursor();
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000,mask1))
            {
                Debug.Log(hit.collider.tag);
                if (hit.collider.tag=="plant")
                {
                    if(hit.transform.gameObject.GetComponent<Vegetable>().onWater == false)
                    {
                        hit.transform.gameObject.GetComponent<Vegetable>().startGrowth();
                    }
                    DefenseUIManager.INSTANCE.WaterRay = false;
                    ObjectPoolingManager.inst.ObjectDisappear();
                }
            }
            else
            {
                DefenseUIManager.INSTANCE.WaterRay = false;
                ObjectPoolingManager.inst.ObjectDisappear();
            }
        }
    }
   

}