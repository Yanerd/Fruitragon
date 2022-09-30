using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoSingleTon<SaveLoadManager>
{
    //���̺�->application ����, ħ�Թޱ� ����, ħ���ϱ� ����

    //�ε� ->application ����, ħ�� ����
    //������ ������ �� json�ε带 ���� 
    //�ʻ���
    //�ǹ�����
    //�巡�� ����

    //Ǯ���� ��ü���� �θ� �� ��ġ
    public Transform SaveLoadManagerPos;
    //���̺� �ε� �Ŵ����� �ڽ� ��ü�� �迭
    public Transform[] InstObjects = null;


    //���̾��Űâ���� ���̺� �ε� �Ŵ����� �ڽ����� ���� ��ü�� ���� �� ����Ʈ
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
