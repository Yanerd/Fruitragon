using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWater : MonoBehaviour
{
    //=================================================
    [SerializeField] float dropSpeed=-1f;//������� �������� �ӵ�
    [SerializeField] float dropPoint_Ypos = 1f;//������� y��
    [SerializeField] float MousePositionMinX = -0.5f;
    [SerializeField] float MousePositionMaxX = 10.5f;
    [SerializeField] float MousePositionMinz = -0.5f;
    [SerializeField] float MousePositionMaxZ = 10.5f;
    Vector3 FollowPoint;
    //=================================================

    void Update()
    {
        if (ButtonManager.inst.buildingMode == true) return;
        //=================================================
        #region WaterDrag&Drop
        //���콺 ��ư�� ������ �߿�
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
            if (Physics.Raycast(ray, out hit,1000))
            {

                FollowPoint = hit.point;
                FollowPoint.y = dropPoint_Ypos;
                this.transform.position = FollowPoint;
                
                if (FollowPoint.x <= MousePositionMinX)
                {
                    FollowPoint.x = MousePositionMinX;
                }

                if (FollowPoint.z <= MousePositionMinz)
                {
                    FollowPoint.z = MousePositionMinz;
                }

                if (FollowPoint.x >= MousePositionMaxX)
                {
                    FollowPoint.x = MousePositionMaxX;
                }

                if (FollowPoint.z >= MousePositionMaxZ)
                {
                    FollowPoint.z = MousePositionMaxZ;
                }

                Vector3 a = Camera.main.WorldToScreenPoint(FollowPoint);
                this.transform.position = Camera.main.ScreenToWorldPoint(a);


            }
        }
        else
        {
            transform.Translate(0, dropSpeed * Time.deltaTime, 0);
        }
        #endregion
        //=================================================
    }

    //=================================================
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="OnGround"||other.tag=="GroundEmpty")
        {
            Debug.Log("물이 닿아 있어요");
            other.GetComponent<Ground>().OnWater = false;
            gameObject.transform.position = ObjectPoolingManager.inst.PoolingZone.position;
            ObjectPoolingManager.inst.waterList.Add(this.gameObject);
            gameObject.SetActive(false);
        }

        
    }
    //=================================================


}
