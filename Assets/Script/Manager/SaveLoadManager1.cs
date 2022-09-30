using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoSingleTon<SaveLoadManager>
{
    //세이브->application 종료, 침입받기 직전, 침입하기 직전

    //로드 ->application 시작, 침입 종료
    //게임이 시작할 시 json로드를 통해 
    //맵생성
    //건물생성
    //드래곤 생성

    //풀링된 객체들의 부모가 될 위치
    public Transform SaveLoadManagerPos;
    //세이브 로드 매니저의 자식 객체의 배열
    public Transform[] InstObjects = null;


    //하이어라키창에서 세이브 로드 매니저의 자식으로 들어온 객체를 저장 할 리스트
    List<Transform> Info = new List<Transform>();
    
    private void Awake()
    {
        SaveLoadManagerPos = GetComponent<Transform>();
    }


    public void SaveUnitInfo( List<string> ObjectName, List<float> ObjectPosX, List<float> ObjectPosY, List<float> ObjectPosZ,
                         List<float> ObjectScaleX, List<float> ObjectScaleY, List<float> ObjectScaleZ)
    {
        for (int i = 0; i < InstObjects.Length; i++)
        {
            Info.Add(InstObjects[i]);
        }
        //string
        for (int i = 0; i < InstObjects.Length; i++)
        {
            ObjectName.Add(Info[i].name);
        }
        //transform
        for (int i = 0; i < InstObjects.Length; i++)
        {
            ObjectPosX.Add(Info[i].transform.position.x);
            ObjectPosY.Add(Info[i].transform.position.y);
            ObjectPosZ.Add(Info[i].transform.position.z);
        }
        //scale
        for (int i = 0; i < InstObjects.Length; i++)
        {
            ObjectScaleX.Add(Info[i].transform.lossyScale.x);
            ObjectScaleY.Add(Info[i].transform.lossyScale.y);
            ObjectScaleZ.Add(Info[i].transform.lossyScale.z);
        }
    }





}
