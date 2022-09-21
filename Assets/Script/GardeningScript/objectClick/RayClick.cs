using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayClick : MonoBehaviour
{


    Renderer color;
    public GameObject Menu;
    public GameObject buldingModeMenu;

    [Header("��ġ�� ä��")]
    public GameObject potatoPrefab;
    public GameObject waterPrefab;
    public GameObject housePrefab;

    [Header("������ ������Ʈ")]
    public GameObject WellPrefab;
    int mask;
    private void Awake()
    {
        color = GetComponent<Renderer>();

        mask = Camera.main.cullingMask = (1 << 9);
    }

    private void OnEnable()
    {
       
    }


    bool callreal;
    Vector3 tr;

    void Update()
    {
        if (ButtonManager.inst.buildingMode == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f,mask))
            {
                if(tr==Vector3.zero)
                {
                    tr = hit.transform.position;
                }
                if(tr!= hit.transform.position&& hit.transform.gameObject.tag == "GroundEmpty")
                {
                    //hit.transform.gameObject.GetComponent<MeshRenderer>().material[0].color=
                    ObjectPoolingManager.inst.Objectapperar(hit.transform.position);
                    tr = hit.transform.position;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.transform.gameObject.tag == "GroundEmpty")
                    {
                        if (ButtonManager.inst.OnHouse == true)
                        {
                            ButtonManager.inst.OnHouse = false;
                            GameObject house = Instantiate(housePrefab, hit.transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
                            ObjectPoolingManager.inst.ObjectDisappear();
                        }
                        else if (ButtonManager.inst.OnPotato == true)
                        {
                            ButtonManager.inst.OnPotato = false;
                            GameObject plant = Instantiate(potatoPrefab, hit.transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
                            ObjectPoolingManager.inst.ObjectDisappear();
                        }
                        else if (ButtonManager.inst.OnWell == true)
                        {
                            ButtonManager.inst.OnWell = false;
                            GameObject Well = Instantiate(WellPrefab, hit.transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
                            ObjectPoolingManager.inst.ObjectDisappear();
                        }

                       
                    }
                }
            }
            
        }

        if(Input.GetMouseButtonDown(0)&& ButtonManager.inst.buildingMode == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.transform.gameObject.tag == "well")
                {
                    if(ObjectPoolingManager.inst.waterList.Count==0)
                    {
                        GameObject water = Instantiate(waterPrefab, hit.transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
                    }
                    else if(ObjectPoolingManager.inst.waterList.Count != 0)
                    {
                        ObjectPoolingManager.inst.waterList[0].SetActive(true);
                        ObjectPoolingManager.inst.waterList[0].transform.position = hit.transform.position + new Vector3(0, 1f, 0);
                    }
                   
                }
            }
        }
        





        //if (Input.GetMouseButtonDown(0))
        //{
        //    int mask = Camera.main.cullingMask = (1 << 9);
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit, 100f, mask))
        //    {
        //        if (hit.transform.gameObject.tag == "GroundEmpty" && ButtonManager.inst.buildingMode==true)
        //        {
        //            //Debug.Log(hit.collider.gameObject.name);
        //            if (ButtonManager.inst.OnHouse == true)
        //            {

        //                Debug.Log("readyHouse");
        //                GameObject house = Instantiate(housePrefab, hit.transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        //                ButtonManager.inst.OnHouse = false;

        //                return;
        //            }
        //            else if (ButtonManager.inst.OnPotato == true)
        //            {
        //                Debug.Log("readyPotato");
        //                GameObject plant = Instantiate(potatoPrefab, hit.transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
        //                ButtonManager.inst.OnPotato = false;
        //                return;
        //            }
        //            else if(ButtonManager.inst.OnWell == true)
        //            {
        //                GameObject Well = Instantiate(WellPrefab, hit.transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        //                ButtonManager.inst.OnWell = false;
        //            }
        //        }
        //        if (Physics.Raycast(ray, out hit))
        //        {
        //            if (hit.transform.gameObject.tag == "well" && ButtonManager.inst.buildingMode == false)
        //            {
        //                GameObject water = Instantiate(waterPrefab, hit.transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        //            }

        //        }
        //    }

        //}

    }







}
