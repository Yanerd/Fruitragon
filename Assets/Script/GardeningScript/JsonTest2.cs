using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData
{
    public List<string> Name = new List<string>();
    public List<float> Xpos = new List<float>();
    public List<float> Ypos = new List<float>();
    public List<float> Zpos = new List<float>();
    public List<float> Xscale = new List<float>();
    public List<float> Yscale = new List<float>();
    public List<float> Zscale = new List<float>();
}

public class JsonTest2 : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SaveLoadManager.INSTANCE.InstObjects = GetComponentsInChildren<Transform>();
            Save();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Load();
        }
    }

    
    public static void Save()
    {
        Debug.Log("저장시작");
        SaveData savedata = new SaveData();


        SaveLoadManager.INSTANCE.SaveUnitInfo( savedata.Name, savedata.Xpos, savedata.Ypos, savedata.Zpos, savedata.Xscale, savedata.Yscale, savedata.Zscale);


        File.WriteAllText(Application.dataPath + "/Test.json", JsonUtility.ToJson(savedata));
    }

    public static void Load()
    {
        Debug.Log("로드시작");
        SaveData saveData2 = JsonUtility.FromJson<SaveData>(File.ReadAllText(Application.dataPath + "/Test.json"));

        for (int i = 0; i < saveData2.Name.Count; i++)
        {
            Debug.Log(saveData2.Name[i]);
        }
    }
}
