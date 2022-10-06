using AESWithJava.Con;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Photon.Pun;
using Photon.Realtime;

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
    public string potatoSeedCount;
    public string appleSeedCount;
    public string cabbageSeedCount;
    public string carrotSeedCount;
    public string eggplantSeedCount;
    public string houseCount;
    public string wellCount;



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

    public int convertpotatoSeedCount { get; set; }
    public int convertappleSeedCount { get; set; }
    public int convertcabbageSeedCount { get; set; }
    public int convertcarrotSeedCount { get; set; }
    public int converteggplantSeedCount { get; set; }
    public int convertHouseCount { get; set; }
    public int convertWellCount { get; set; }

    Transform[] fence = new Transform[5];
    Transform[] tree = new Transform[4];

    private GameObject PoolingZone;

    private void Start()
    {
        PoolingZone = GameObject.Find("PoolingZone");
        DontDestroyOnLoad(this.gameObject);
    }

    public void Save()
    {
        
        if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Dragon Seed/" + "/Saves/") == false) // There's no directory
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Dragon Seed/" + "/Saves/");
        }
        Debug.Log("저장시작" + Time.time);

        objectCount = PoolingZone.transform.childCount;
        Data data = new Data();

        data.groundState = DefenseUIManager.INSTANCE.MapState.ToString();
        data.potatoSeedCount = DefenseUIManager.INSTANCE.potatoSeedCount.ToString();
        data.appleSeedCount = DefenseUIManager.INSTANCE.appleSeedCount.ToString();
        data.cabbageSeedCount = DefenseUIManager.INSTANCE.cabbageSeedCount.ToString();
        data.carrotSeedCount = DefenseUIManager.INSTANCE.carrotSeedCount.ToString();
        data.eggplantSeedCount = DefenseUIManager.INSTANCE.eggplantSeedCount.ToString();
        data.houseCount = DefenseUIManager.INSTANCE.houseCount.ToString();
        data.wellCount = DefenseUIManager.INSTANCE.wellCount.ToString();

        for (int i = 0; i < PoolingZone.transform.childCount; i++)
        {
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
            data.waterListCount = data.water.Count.ToString();
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

        int waterListCount = 0;
        int wellBarListCount = 0;
        convertpotatoSeedCount = int.Parse(data2.potatoSeedCount);
        convertappleSeedCount = int.Parse(data2.appleSeedCount);
        convertcabbageSeedCount = int.Parse(data2.cabbageSeedCount);
        convertcarrotSeedCount = int.Parse(data2.carrotSeedCount);
        converteggplantSeedCount = int.Parse(data2.eggplantSeedCount);
        convertHouseCount = int.Parse(data2.houseCount);
        convertWellCount = int.Parse(data2.wellCount);

        bool parseCount = int.TryParse(data2.saveListCount, out count);
        convertGroundState = int.Parse(data2.groundState);

        if (parseCount == true)
        {
            for (int i = 0; i < count; i++)
            {
                convertX = float.Parse(data2.xPos[i]);
                convertY = float.Parse(data2.yPos[i]);
                convertZ = float.Parse(data2.zPos[i]);

                Vector3 vector3 = new Vector3(convertX, convertY, convertZ);

                Init(data2.name[i], vector3);
                //}
            }

        }
        else
        {
            GroundInit();
        }

        SeedCount(convertpotatoSeedCount, convertappleSeedCount, convertcabbageSeedCount, convertcarrotSeedCount, converteggplantSeedCount);
    }
    public void SeedCount(int potato, int apple, int cabbage, int carrot, int eggplant)
    {
        DefenseUIManager.INSTANCE.potatoSeedCount = potato;
        DefenseUIManager.INSTANCE.appleSeedCount = apple;
        DefenseUIManager.INSTANCE.cabbageSeedCount = cabbage;
        DefenseUIManager.INSTANCE.carrotSeedCount = carrot;
        DefenseUIManager.INSTANCE.eggplantSeedCount = eggplant;
    }

    [PunRPC]
    public void Init(string name, Vector3 pos)
    {
        GameObject obj = Resources.Load<GameObject>($"Prefebs/{name}");
        GameObject instObject = null;
        if (GameManager.INSTANCE.ISGAMEIN == true)
        {
            Debug.Log("포톤 인스턴시" + name);
            //포톤 인스턴시 에이트 적용 
            instObject = PhotonNetwork.Instantiate(name, pos, Quaternion.identity);
        }
        else
        {
            Debug.Log("로컬 인스턴시");
            PoolingZone = GameObject.Find("PoolingZone");
            Debug.Log(name);
            instObject = Instantiate(obj, pos, Quaternion.identity, PoolingZone.transform);
            Debug.Log(instObject.name);
        }
    }
    [PunRPC]
    public void VegetableInit(string name, Vector3 pos,float growthValue,int countValue)
    {
        GameObject obj;
        GameObject instObject;

        obj = Resources.Load<GameObject>($"Prefebs/{name}");

        if (GameManager.INSTANCE.ISGAMEIN==true)
        {
            Debug.Log("포톤 인스턴시" + name);
            //포톤 인스턴시 에이트 적용 
            instObject = PhotonNetwork.Instantiate(name, pos, Quaternion.identity);
            instObject.GetComponent<Vegetable>().PhotonInstOffenseVegetable(growthValue, countValue);
            
        }
        else
        {
            Debug.Log("로컬 인스턴시");
            instObject = Instantiate(obj, pos, Quaternion.identity, PoolingZone.transform);
            instObject.GetComponent<Vegetable>().PhotonInstDefenseVegetable(growthValue, countValue);
        }

        
    }
    [PunRPC]
    public void WellInit(string name, Vector3 pos, float fillValue)
    {
        GameObject obj;
        GameObject instObject;

        obj = Resources.Load<GameObject>($"Prefebs/{name}");

        if (GameManager.INSTANCE.ISGAMEIN == true)
        {
            Debug.Log("포톤 인스턴시" + name);
            //포톤 인스턴시 에이트 적용 
            instObject = PhotonNetwork.Instantiate(name, pos, Quaternion.identity);
            instObject.GetComponent<WellBar>().PhotonOffenseFillWater(fillValue);
        }
        else
        {
            Debug.Log("로컬 인스턴시");
            instObject = Instantiate(obj, pos, Quaternion.identity, PoolingZone.transform);
            instObject.GetComponent<WellBar>().PhotonDefenseFillWater(fillValue);
        }



        
    }
    [PunRPC]
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
    [PunRPC]
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


