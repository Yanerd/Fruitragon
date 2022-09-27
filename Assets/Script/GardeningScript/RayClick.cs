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


        if (ButtonManager.inst.buildingMode == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                if (hit.transform.name == "EndGround"|| hit.transform.tag == "OnGround")
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
                    if (ButtonManager.inst.OnHouse == true || ButtonManager.inst.OnWell == true)
                    {
                        myCursor.BuildingCursor();
                    }
                    if (ButtonManager.inst.OnApple == true || ButtonManager.inst.OnCabbage == true ||
                               ButtonManager.inst.OnCarrot == true || ButtonManager.inst.OnEggplant == true ||
                               ButtonManager.inst.OnPotato == true)
                    {
                        myCursor.VegetableCursor();
                    }
                }
            }
        }


        if (ButtonManager.inst.buildingMode == true &&
            (ButtonManager.inst.OnHouse == true ||
            ButtonManager.inst.OnWell == true ||
            ButtonManager.inst.OnApple == true ||
            ButtonManager.inst.OnCabbage == true ||
            ButtonManager.inst.OnCarrot == true ||
            ButtonManager.inst.OnEggplant == true ||
            ButtonManager.inst.OnPotato == true))
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

                    if (ButtonManager.inst.OnHouse == true)
                    {
                        ButtonManager.inst.OnHouse = false;
                        ObjectPoolingManager.inst.Instantiate(housePrefab, hit.transform.position, Quaternion.identity, ObjectPoolingManager.inst.PoolingZone);
                        ObjectPoolingManager.inst.ObjectDisappear();
                    }
                    else if (ButtonManager.inst.OnPotato == true)
                    {
                        ButtonManager.inst.OnPotato = false;
                        ObjectPoolingManager.inst.Instantiate(potatoPrefab, hit.transform.position + new Vector3(0, 0.33f, 0), Quaternion.identity, ObjectPoolingManager.inst.PoolingZone);
                        ObjectPoolingManager.inst.ObjectDisappear();
                    }
                    else if (ButtonManager.inst.OnWell == true )
                    {
                        ButtonManager.inst.OnWell = false;
                        ObjectPoolingManager.inst.Instantiate(WellPrefab, hit.transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity, ObjectPoolingManager.inst.PoolingZone);
                        ObjectPoolingManager.inst.ObjectDisappear();
                    }
                    else if (ButtonManager.inst.OnApple == true)
                    {
                        ButtonManager.inst.OnApple = false;
                        ObjectPoolingManager.inst.Instantiate(ApplePrefab, hit.transform.position+new Vector3(0,0.33f,0), Quaternion.identity, ObjectPoolingManager.inst.PoolingZone);
                        ObjectPoolingManager.inst.ObjectDisappear();
                    }
                    else if (ButtonManager.inst.OnCabbage == true)
                    {
                        ButtonManager.inst.OnCabbage = false;
                        ObjectPoolingManager.inst.Instantiate(CabbagePrefab, hit.transform.position + new Vector3(0, 0.33f, 0), Quaternion.identity, ObjectPoolingManager.inst.PoolingZone);
                        ObjectPoolingManager.inst.ObjectDisappear();
                    }
                    else if (ButtonManager.inst.OnCarrot == true )
                    {
                        ButtonManager.inst.OnCarrot = false;
                        ObjectPoolingManager.inst.Instantiate(CarrotPrefab, hit.transform.position + new Vector3(0, 0.33f, 0), Quaternion.identity, ObjectPoolingManager.inst.PoolingZone);
                        ObjectPoolingManager.inst.ObjectDisappear();
                    }
                    else if (ButtonManager.inst.OnEggplant == true)
                    {
                        ButtonManager.inst.OnEggplant = false;
                        ObjectPoolingManager.inst.Instantiate(EggplantPrefab, hit.transform.position + new Vector3(0, 0.33f, 0), Quaternion.identity, ObjectPoolingManager.inst.PoolingZone);
                        ObjectPoolingManager.inst.ObjectDisappear();
                    }
                }
            }
        }

        if (Input.GetMouseButton(0) && ButtonManager.inst.buildingMode == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.transform.tag=="well")
                {
                    Debug.Log("¹°»ý¼º");
                    ButtonManager.inst.WaterRay = true;
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
        else if (Input.GetMouseButtonUp(0)&&ButtonManager.inst.buildingMode == false&& ButtonManager.inst.WaterRay == true)
        {
            myCursor.BasicCursor();
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000,mask1))
            {
                Debug.Log(hit.collider.tag);
                if (hit.collider.tag=="plant")
                {
                    hit.transform.gameObject.GetComponent<Vegetable>().onWater = true;
                    ButtonManager.inst.WaterRay = false;
                    ObjectPoolingManager.inst.ObjectDisappear();
                }
            }
            else
            {
                ButtonManager.inst.WaterRay = false;
                ObjectPoolingManager.inst.ObjectDisappear();
            }
        }
    }
   

}