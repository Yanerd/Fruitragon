using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class Ground : MonoBehaviour
{
    MeshRenderer mr;
    public bool OnBuilding;
    public Material Floor;
    public bool onbulidingMode = false;
    [SerializeField] bool OnAlpha=false;
    [SerializeField] public bool OnWater = false;

    int mask;

    ButtonManager B;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        B = FindObjectOfType<ButtonManager>();
        mask = 1 << 9 | 1 << 10;
    }

   

    void Update()
    {

        if (ButtonManager.inst.buildingMode == true)
        {
            Camera.main.cullingMask = mask;
            if (OnBuilding == true)
            {
                mr.materials[0].color = Color.red;
            }
            else if (OnAlpha == true)
            {
                mr.materials[0].color = Color.blue;
            }
            else if (OnAlpha == false)
            {
                mr.materials[0].color = Color.white;
            }
            
        }
        if (ButtonManager.inst.buildingMode == false)
        {
            if (OnBuilding == true || OnAlpha == true)
            {
                Camera.main.cullingMask = -1;
                mr.materials[0].color = Color.white;
            }
            if(OnWater==true)
            {
                if (OnWater == false)
                {
                    mr.materials[0].color = Color.white;
                    return;
                }
                    
                mr.materials[0].color = Color.cyan;
            }
            else if (OnWater == false)
            {
                mr.materials[0].color = Color.white;
            }

        }
    }

    public void Execute_bulidingMode()
    {
        Camera.main.cullingMask = mask;
        if (OnBuilding == true)
        {
            mr.materials[0].color = Color.red;
        }
        else if (OnAlpha == true)
        {
            mr.materials[0].color = Color.blue;
        }
        else if (OnAlpha == false)
        {
            mr.materials[0].color = Color.white;
        }

    }



    public void DeExecute_bulidingMode()
    {
        if (OnBuilding == true || OnAlpha == true)
        {
            Camera.main.cullingMask = -1;
            mr.materials[0].color = Color.white;
        }
    }




    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Building")
        {
            this.gameObject.tag = "OnGround";
            OnBuilding = true;
        }
        if (collision.collider.tag == "plant")
        {
            this.gameObject.tag = "OnGround";
            OnBuilding = true;
        }
        if (collision.collider.tag == "well")
        {
            this.gameObject.tag = "OnGround";
            OnBuilding = true;
        }
        if (collision.collider.tag == "Alpha")
        {
            OnAlpha = true;
        }
       

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WaterRay")
        {
            OnWater = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "well")
        {
            this.gameObject.tag = "GroundEmpty";
            OnBuilding = false;
        }
        if (collision.collider.tag == "Alpha")
        {
            this.gameObject.tag = "GroundEmpty";
            OnAlpha = false;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "WaterRay")
        {
            OnWater = false;
        }
    }


}
