using AESWithJava.Con;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class Data
{
    public string saveListCount;

    public string waterListCount;
    public string plantBarListCount;
    public string wellBarListCount;

    public List<string> xPos = new List<string>();
    public List<string> yPos = new List<string>();
    public List<string> zPos = new List<string>();
    public List<string> name = new List<string>();
    public List<string> water = new List<string>();
    public List<string> plantBar = new List<string>();
    public List<string> wellBar = new List<string>();
    

    public string groundState;
}

public class SaveLoadManager : MonoSingleTon<SaveLoadManager>
{
    public int objectCount;
    public int waterCount;
    public int plantBarCount;
    public int wellBarCount;

    public string str;
    public string xpos;
    public string ypos;
    public string zpos;

    int convertWaterCount;
    float convertPlantBar;
    int convertGroundState;
    float convertWellBar;


    Transform[] fence = new Transform[5];
    Transform[] tree = new Transform[4];

    private GameObject PoolingZone;

    private void Awake()
    {
        PoolingZone = GameObject.Find("PoolingZone");
        DontDestroyOnLoad(this.gameObject);
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
        

        if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Dragon Seed/" + "/Saves/") == false) // There's no directory
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Dragon Seed/" + "/Saves/");
        }
        Debug.Log("저장시작" + Time.time);

        Data data = new Data();
        for (int i = 0; i < PoolingZone.transform.childCount; i++)
        {

            objectCount = PoolingZone.transform.childCount;


          

            str = PoolingZone.transform.GetChild(i).name;
            xpos = PoolingZone.transform.GetChild(i).position.x.ToString();
            ypos = PoolingZone.transform.GetChild(i).position.y.ToString();
            zpos = PoolingZone.transform.GetChild(i).position.z.ToString();
            string[] words = str.Split('(');
            data.name.Add(words[0]);
            data.xPos.Add(xpos);
            data.yPos.Add(ypos);
            data.zPos.Add(zpos);
            data.saveListCount = objectCount.ToString();
            data.groundState = DefenseUIManager.INSTANCE.MapState.ToString();
            
            if (words[0] == "Well")
            {
                data.wellBar.Add(PoolingZone.transform.GetChild(i).GetComponent<WellBar>().FillValue.ToString());
            }
            if(words[0] == "V_Potato")
            {
                data.plantBar.Add(PoolingZone.transform.GetChild(i).GetComponent<Vegetable>().GrowthValue.ToString());
                data.water.Add(PoolingZone.transform.GetChild(i).GetComponent<Vegetable>().CountValue.ToString());
            }
            if (words[0] == "V_Apple")
            {
                data.plantBar.Add(PoolingZone.transform.GetChild(i).GetComponent<Vegetable>().GrowthValue.ToString());
                data.water.Add(PoolingZone.transform.GetChild(i).GetComponent<Vegetable>().CountValue.ToString());
            }
            if (words[0] == "V_Cabbage")
            {
                data.plantBar.Add(PoolingZone.transform.GetChild(i).GetComponent<Vegetable>().GrowthValue.ToString());
                data.water.Add(PoolingZone.transform.GetChild(i).GetComponent<Vegetable>().CountValue.ToString());
            }
            if (words[0] == "V_Carrot")
            {
                data.plantBar.Add(PoolingZone.transform.GetChild(i).GetComponent<Vegetable>().GrowthValue.ToString());
                data.water.Add(PoolingZone.transform.GetChild(i).GetComponent<Vegetable>().CountValue.ToString());
            }
            if (words[0] == "V_Eggplant")
            {
                data.plantBar.Add(PoolingZone.transform.GetChild(i).GetComponent<Vegetable>().GrowthValue.ToString());
                data.water.Add(PoolingZone.transform.GetChild(i).GetComponent<Vegetable>().CountValue.ToString());
            }
            //plantBarCount = data.plantBar.Count;
            data.waterListCount = data.water.Count.ToString();
            //wellBarCount = data.wellBar.Count;




        }

        
        Debug.Log("저장완료" + Time.time);



        File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Dragon Seed/" + "/Saves/" + "/Save01.json", Encryption.Encrypt((JsonUtility.ToJson(data)),"key"));
    }

    public void Load()
    {
        int count;
        float convertX;
        float convertY;
        float convertZ;
        

        string parse = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Dragon Seed/" + "/Saves/" + "/Save01.json");
        File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Dragon Seed/" + "/Saves/" + "/Read.json", Encryption.Decrypt(parse, "key"));

        Debug.Log("로드시작");
        Data data2 = JsonUtility.FromJson<Data>(File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Dragon Seed/" + "/Saves/" + "/Read.json"));
        
        
        count = int.Parse(data2.saveListCount);
        waterCount = int.Parse(data2.waterListCount);
        int j = 0;
        for (int i = 0; i < count; i++)
        {
            convertX = float.Parse(data2.xPos[i]);
            convertY = float.Parse(data2.yPos[i]);
            convertZ = float.Parse(data2.zPos[i]);
            
            convertGroundState = int.Parse(data2.groundState);

            Vector3 vector3 = new Vector3(convertX, convertY, convertZ);

            if(data2.name[i]== "V_Potato"|| data2.name[i] == "V_Apple" || data2.name[i] == "V_Cabbage" || 
                data2.name[i] == "V_Carrot" || data2.name[i] == "V_Eggplant")
            {
                
                Debug.Log(waterCount+"Save");
              
                
                convertWaterCount = int.Parse(data2.water[j]);
                convertPlantBar = float.Parse(data2.plantBar[j]);

                VegetableInit(data2.name[i], vector3, convertPlantBar, convertWaterCount);
                j++;
                //j = 0;
            }
            else
            {
                Init(data2.name[i], vector3);
            }
            
        }

        GroundInit();

        //File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Dragon Seed/" + "/Saves/" + "/Read.json");
    }
    public GameObject[] Prefebs;
    public void Init(string name, Vector3 pos)
    {
        GameObject obj = Resources.Load<GameObject>($"Prefebs/{name}");
        GameObject instObject = Instantiate(obj, pos, Quaternion.identity, PoolingZone.transform);

        

    }
    public void VegetableInit(string name, Vector3 pos,float growthValue,int countValue)
    {
        GameObject obj = Resources.Load<GameObject>($"Prefebs/{name}");
        GameObject instObject = Instantiate(obj, pos, Quaternion.identity, PoolingZone.transform);

        //instObject.GetComponent<Vegetable>().GrowthValue = growthValue;
        //instObject.GetComponent<Vegetable>().CountValue = countValue;
        instObject.GetComponent<Vegetable>().PhotonInstOffenseVegetable(growthValue, countValue);


    }
    public void GroundInit()
    {
        for (int i = 0; i < GameObject.Find("fence").transform.childCount; i++)
        {
            fence[i] = GameObject.Find("fence").transform.GetChild(i);
        }

        for (int i = 0; i < GameObject.Find("tree").transform.childCount; i++)
        {
            tree[i] = GameObject.Find("tree").transform.GetChild(i);
        }

        PhotonInstGround(fence, tree, convertGroundState);
    }

    public void PhotonInstGround(Transform[] fence, Transform[] tree, int mapState)
    {
        if (mapState == 4)
        {
            fence[0].transform.position = new Vector3(10, 10, 10);
            fence[1].gameObject.SetActive(true);
            tree[0].gameObject.SetActive(false);
        }
        else if (mapState == 3)
        {
            fence[0].transform.position = new Vector3(10, 10, 10);
            fence[1].transform.position = new Vector3(10, 10, 10);
            fence[2].gameObject.SetActive(true);
            tree[0].gameObject.SetActive(false);
            tree[1].gameObject.SetActive(false);
        }
        else if (mapState == 2)
        {
            fence[0].transform.position = new Vector3(10, 10, 10);
            fence[1].transform.position = new Vector3(10, 10, 10);
            fence[2].transform.position = new Vector3(10, 10, 10);
            fence[3].gameObject.SetActive(true);
            tree[0].gameObject.SetActive(false);
            tree[1].gameObject.SetActive(false);
            tree[2].gameObject.SetActive(false);
        }
        else if (mapState == 1)
        {
            fence[0].transform.position = new Vector3(10, 10, 10);
            fence[1].transform.position = new Vector3(10, 10, 10);
            fence[2].transform.position = new Vector3(10, 10, 10);
            fence[3].transform.position = new Vector3(10, 10, 10);
            fence[4].gameObject.SetActive(true);

            tree[0].gameObject.SetActive(false);
            tree[1].gameObject.SetActive(false);
            tree[2].gameObject.SetActive(false);
            tree[3].gameObject.SetActive(false);
        }
        else if (mapState == 0)
        {
            fence[0].transform.position = new Vector3(10, 10, 10);
            fence[1].transform.position = new Vector3(10, 10, 10);
            fence[2].transform.position = new Vector3(10, 10, 10);
            fence[3].transform.position = new Vector3(10, 10, 10);
            fence[4].transform.position = new Vector3(10, 10, 10);
            tree[0].gameObject.SetActive(false);
            tree[1].gameObject.SetActive(false);
            tree[2].gameObject.SetActive(false);
            tree[3].gameObject.SetActive(false);
        }

    }
}


