using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using System;

[System.Serializable]
public class Data
{
    public List<Vector3> position = new List<Vector3>();
    public List<Vector3> scale = new List<Vector3>();
    public List<string> name = new List<string>();
    //public int mapScale;
    //public List<Vector3> rotation = new List<Vector3>();
}

public class SaveLoadManager : MonoSingleTon<SaveLoadManager>
{
    public int objectCount;
    public string str;

    private GameObject PoolingZone;

    private void Awake()
    {
        PoolingZone = GameObject.Find("PoolingZone");
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Load();
        }
    }

    public void Save()
    {
        Debug.Log("저장시작" + Time.time);
        objectCount = PoolingZone.transform.childCount;
        Data data = new Data();
        for (int i = 0; i < objectCount; i++)
        {
            data.name.Add(PoolingZone.transform.GetChild(i).name);
            data.position.Add(PoolingZone.transform.GetChild(i).position);
            data.scale.Add(PoolingZone.transform.GetChild(i).localScale);
        }
        Debug.Log("저장완료" + Time.time);
            
        File.WriteAllText(Application.dataPath + "/Test.json", JsonUtility.ToJson(data));
    }

    public void Load()
    {
        
        Debug.Log("로드시작");
        Data data2 = JsonUtility.FromJson<Data>(File.ReadAllText(Application.dataPath + "/Test.json"));

        for (int i = 0; i < objectCount; i++)
        {
            str = data2.name[i];
            string[] words = str.Split('(');
            Init(words[0], data2.position[i], data2.scale[i]);
        }
    }
    public GameObject[] Prefebs;
    public void Init(string name, Vector3 pos, Vector3 scale)
    {
        GameObject obj = Resources.Load<GameObject>($"Prefebs/{name}");
        Instantiate(obj, pos, Quaternion.identity, PoolingZone.transform);
    }
}


