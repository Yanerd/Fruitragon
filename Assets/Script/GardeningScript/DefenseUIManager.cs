using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DefenseUIManager : MonoSingleTon<DefenseUIManager>
{
    #region Click Object Value
    [SerializeField] public bool buildingMode { get; set; } 
    [SerializeField] public bool OnPotato { get; set; }
    [SerializeField] public bool OnApple { get; set; }
    [SerializeField] public bool OnCabbage { get; set; }
    [SerializeField] public bool OnCarrot { get; set; }
    [SerializeField] public bool OnEggplant { get; set; }
    [SerializeField] public bool OnWater { get; set; }

    [SerializeField] public bool OnHouse { get; set; }
    [SerializeField] public bool OnWell { get; set; }


    [SerializeField] public bool WaterRay { get; set; } = false;

    #endregion

    #region Ui
    [Header("[ UI ]")]
    [SerializeField] public GameObject Menu;
    [SerializeField] public GameObject buldingModeMenu;

    [SerializeField] GameObject[] BulidingModeScroll;
    [SerializeField] GameObject[] BulidingModeScrollButton;
    [SerializeField] GameObject SearchRoomPage;
    [SerializeField] GameObject StorePage;


    [SerializeField] TextMeshProUGUI text;

    #endregion

    CursorChange myCursor;
    Camera myCamera;
    Vector3 originCamearPos;

    bool onMene;
    bool onBuildingMene;

    Vector3 VegetableMenu;
    Vector3 BuildingMenu;
    Vector3 originMenuPos;

    private void Awake()
    {
        //Bring Camera Component
        myCamera = Camera.main;
        originCamearPos = myCamera.transform.position;

        //button is clicked Permit
        onMene = false;
        onBuildingMene = false;
        //Cursor Change Image 
        myCursor = FindObjectOfType<CursorChange>();

        //Reset Value
        Initializing();

        VegetableMenu = BulidingModeScroll[0].transform.position;
        BuildingMenu = BulidingModeScroll[1].transform.position;

    }

    //Value Reset 
    private void Initializing()
    {
        DefenseUIManager.INSTANCE.OnWell = false;
        DefenseUIManager.INSTANCE.OnPotato = false;
        DefenseUIManager.INSTANCE.OnHouse = false;
        DefenseUIManager.INSTANCE.OnPotato = false;
        DefenseUIManager.INSTANCE.OnApple = false;
        DefenseUIManager.INSTANCE.OnCabbage = false;
        DefenseUIManager.INSTANCE.OnCarrot = false;
        DefenseUIManager.INSTANCE.OnEggplant = false;
    }

    #region BuildingMode Func

    #region BuildingMode ButtonEvent
    public void OpenScrollVegetable()
    {
        BulidingModeScrollButton[1].GetComponent<Button>().interactable = false;

        StartCoroutine(Gotrans(BulidingModeScroll[0]));
        BulidingModeScroll[1].transform.position = BuildingMenu;
        SwitchButtonOpen(1);
        SwitchButtonBack(0);
    }
    public void CloseScrollVegetable()
    {
        BulidingModeScrollButton[1].GetComponent<Button>().interactable = true;
        StartCoroutine(Backtrans(BulidingModeScroll[0]));
        SwitchButtonOpen(0);
    }
    public void OpenScrollBuilding()
    {
        BulidingModeScrollButton[0].GetComponent<Button>().interactable = false;

        StartCoroutine(Gotrans(BulidingModeScroll[1]));
        BulidingModeScroll[0].transform.position = VegetableMenu;
        SwitchButtonOpen(0);
        SwitchButtonBack(1);
    }
    public void CloseScrollBuilding()
    {
        BulidingModeScrollButton[0].GetComponent<Button>().interactable = true;
        StartCoroutine(Backtrans(BulidingModeScroll[1]));
        SwitchButtonOpen(1);
    }
    public void OpenScrollMenu()
    {
        StartCoroutine(Gotrans(BulidingModeScroll[2]));
        SwitchButtonBack(2);
    }
    public void CloseScrollMenu()
    {
        StartCoroutine(Backtrans(BulidingModeScroll[2]));
        SwitchButtonOpen(2);
    }
    void SwitchButtonBack(int scrollnum)
    {
        if(scrollnum==0)
        {
            BulidingModeScrollButton[0].SetActive(false);
            BulidingModeScrollButton[3].SetActive(true);
        }
        else if (scrollnum == 1)
        {
            BulidingModeScrollButton[1].SetActive(false);
            BulidingModeScrollButton[4].SetActive(true);
        }
        else if (scrollnum == 2)
        {
            BulidingModeScrollButton[2].SetActive(false);
            BulidingModeScrollButton[5].SetActive(true);
        }
       
    }
    void SwitchButtonOpen(int scrollnum)
    {
        if (scrollnum == 0)
        {
            BulidingModeScrollButton[0].SetActive(true);
            BulidingModeScrollButton[3].SetActive(false);
        }
        else if (scrollnum == 1)
        {
            BulidingModeScrollButton[1].SetActive(true);
            BulidingModeScrollButton[4].SetActive(false);
        }
        else if (scrollnum == 2)
        {
            BulidingModeScrollButton[2].SetActive(true);
            BulidingModeScrollButton[5].SetActive(false);
        }

    }
    #endregion

    #region BuildingMenu Open&Close Corutine
    IEnumerator Gotrans(GameObject scroll)
    {
        originMenuPos = scroll.transform.position;
        while(true)
        {
            scroll.transform.position = 
                Vector3.Lerp(scroll.transform.position, scroll.transform.position + new Vector3(100f, 0, 0), Time.deltaTime * 15f);
            yield return null;

            if(scroll.transform.position.x>=450f)
            {
                yield break;
            }
        }
        
    }
    IEnumerator Backtrans(GameObject scroll)
    {
        while (true)
        {
            scroll.transform.position = 
                Vector3.Lerp(scroll.transform.position, scroll.transform.position + new Vector3(-100f, 0, 0), Time.deltaTime * 25f);
            yield return null;

            if(scroll.transform.position.x<=-350f)
            {
                scroll.transform.position = originMenuPos;
                yield break;
            }
        }
        
    }
    #endregion

    public void Go_buildingMode()
    {
        if (onMene) return;
        onMene = true;

        buildingMode = true;
        Menu.SetActive(false);
        buldingModeMenu.SetActive(true);
    }
    public void Back_buildingMode()
    {
        if (!onMene) return;
        if (onMene)
            onMene = false;

        BulidingModeScroll[0].transform.position = VegetableMenu;
        BulidingModeScroll[1].transform.position = BuildingMenu;
        SwitchButtonOpen(0);
        SwitchButtonOpen(1);
        onBuildingMene = false;

        buildingMode = false;
        Menu.SetActive(true);
        buldingModeMenu.SetActive(false);
        Initializing();
        ObjectPoolingManager.inst.ObjectDisappear();
        myCursor.BasicCursor();
    }
    #endregion

    #region SearchRoom Func

    #region Search Room ButtonEvent
    public void UpImage()
    {
        if (onMene)
        {
            DownImage();
            return;
        }
           
        onMene = true;

        StartCoroutine(ChangeCameraView());
       
        StartCoroutine(Uptrans(SearchRoomPage));
    }
    public void DownImage()
    {
        if (!onMene) 
        {
            UpImage();
            return; 
        } 
        if (onMene)
            onMene = false;

        myCamera.transform.position = originCamearPos;
        myCamera.orthographicSize = 2.5f;

        StartCoroutine(Downtrans(SearchRoomPage));
    }
    #endregion

    #region SearchRoom Open&Close Corutine
    IEnumerator Uptrans(GameObject image)
    {
        while (true)
        {
            image.transform.position =
                Vector3.Lerp(image.transform.position, image.transform.position + new Vector3(0, 100f, 0), Time.deltaTime * 20f);
            yield return null;

            if (image.transform.position.y >= 540f)
            {
                yield break;
            }
        }

    }
    IEnumerator Downtrans(GameObject image)
    {
        while (true)
        {
            image.transform.position =
                Vector3.Lerp(image.transform.position, image.transform.position + new Vector3(0, -100f, 0), Time.deltaTime * 30f);
            yield return null;

            if (image.transform.position.y <= -540f)
            {
                yield break;
            }
        }

    }
    #endregion

    #region SearchRoom CameraView
    IEnumerator ChangeCameraView()
    {
        StartCoroutine(CameraViewSize());
        while (true)
        {
            if (!onMene)
            {
                myCamera.transform.position = originCamearPos;
                myCamera.orthographicSize = 2.5f;
                yield break;
            }
            myCamera.transform.position =
                Vector3.Lerp(myCamera.transform.position, new Vector3(-4.9f, 4.8f, -6.5f), Time.deltaTime*2f);
            
            yield return null;
            if (myCamera.transform.position.x <= -4.89f)
            {
                yield break;
            }
        }

    }
    IEnumerator CameraViewSize()
    {
        while (true)
        {
            myCamera.orthographicSize -= 0.04f;
            yield return new WaitForSeconds(Time.deltaTime);
            if (myCamera.orthographicSize <= 0.3f)
            {
                yield break;
            }
        }
    }
    #endregion

    #endregion

    #region StorePage Func
    public void UpStorePage()
    {
        if (onMene) return;
        onMene = true;
        

        StartCoroutine(Uptrans(StorePage));
        text.text = ObjectPoolingManager.inst.potatoDragonCount.ToString();
    }
    public void DownStorePage()
    {
        if (!onMene) return;
        if(onMene)
        onMene = false;
        StartCoroutine(Downtrans(StorePage));
    }
    #endregion

    #region Select Building&Vegetable Button
    public void SelectWell()
    {
        CloseScrollBuilding();
        //myCursor.BuildingCursor();
        DefenseUIManager.INSTANCE.OnWell = true;
        DefenseUIManager.INSTANCE.OnPotato = false;
        DefenseUIManager.INSTANCE.OnHouse = false;
        DefenseUIManager.INSTANCE.OnPotato = false;
        DefenseUIManager.INSTANCE.OnApple = false;
        DefenseUIManager.INSTANCE.OnCabbage = false;
        DefenseUIManager.INSTANCE.OnCarrot = false;
        DefenseUIManager.INSTANCE.OnEggplant = false;
        ObjectPoolingManager.inst.ObjectDisappear();
    }
    public void SelectHouse()
    {
        CloseScrollBuilding();
        //myCursor.BuildingCursor();
        DefenseUIManager.INSTANCE.OnHouse = true;
        DefenseUIManager.INSTANCE.OnPotato = false;
        DefenseUIManager.INSTANCE.OnWell = false;
        DefenseUIManager.INSTANCE.OnApple = false;
        DefenseUIManager.INSTANCE.OnCabbage = false;
        DefenseUIManager.INSTANCE.OnCarrot = false;
        DefenseUIManager.INSTANCE.OnEggplant = false;
        ObjectPoolingManager.inst.ObjectDisappear();
    }
    public void SelectPotato()
    {
        CloseScrollVegetable();
        //myCursor.VegetableCursor();
        DefenseUIManager.INSTANCE.OnPotato = true;
        DefenseUIManager.INSTANCE.OnWell = false;
        DefenseUIManager.INSTANCE.OnHouse = false;
        DefenseUIManager.INSTANCE.OnApple = false;
        DefenseUIManager.INSTANCE.OnCabbage = false;
        DefenseUIManager.INSTANCE.OnCarrot = false;
        DefenseUIManager.INSTANCE.OnEggplant = false;
        ObjectPoolingManager.inst.ObjectDisappear();
    }
    public void SelectApple()
    {
        CloseScrollVegetable();
        //myCursor.VegetableCursor();
        DefenseUIManager.INSTANCE.OnApple = true;
        DefenseUIManager.INSTANCE.OnWell = false;
        DefenseUIManager.INSTANCE.OnPotato = false;
        DefenseUIManager.INSTANCE.OnHouse = false;
        DefenseUIManager.INSTANCE.OnPotato = false;
        DefenseUIManager.INSTANCE.OnCabbage = false;
        DefenseUIManager.INSTANCE.OnCarrot = false;
        DefenseUIManager.INSTANCE.OnEggplant = false;
        ObjectPoolingManager.inst.ObjectDisappear();
    }
    public void SelectCabbage()
    {
        CloseScrollVegetable();
        //myCursor.VegetableCursor();
        DefenseUIManager.INSTANCE.OnCabbage = true;
        DefenseUIManager.INSTANCE.OnWell = false;
        DefenseUIManager.INSTANCE.OnPotato = false;
        DefenseUIManager.INSTANCE.OnHouse = false;
        DefenseUIManager.INSTANCE.OnPotato = false;
        DefenseUIManager.INSTANCE.OnApple = false;
        DefenseUIManager.INSTANCE.OnCarrot = false;
        DefenseUIManager.INSTANCE.OnEggplant = false;
        ObjectPoolingManager.inst.ObjectDisappear();
    }
    public void SelectCarrot()
    {
        CloseScrollVegetable();
        //myCursor.VegetableCursor();
        DefenseUIManager.INSTANCE.OnCarrot = true;
        DefenseUIManager.INSTANCE.OnWell = false;
        DefenseUIManager.INSTANCE.OnPotato = false;
        DefenseUIManager.INSTANCE.OnHouse = false;
        DefenseUIManager.INSTANCE.OnPotato = false;
        DefenseUIManager.INSTANCE.OnApple = false;
        DefenseUIManager.INSTANCE.OnCabbage = false;
        DefenseUIManager.INSTANCE.OnEggplant = false;
        ObjectPoolingManager.inst.ObjectDisappear();
    }
    public void SelectEggplant()
    {
        CloseScrollVegetable();
        //myCursor.VegetableCursor();
        DefenseUIManager.INSTANCE.OnEggplant = true;
        DefenseUIManager.INSTANCE.OnWell = false;
        DefenseUIManager.INSTANCE.OnPotato = false;
        DefenseUIManager.INSTANCE.OnHouse = false;
        DefenseUIManager.INSTANCE.OnPotato = false;
        DefenseUIManager.INSTANCE.OnApple = false;
        DefenseUIManager.INSTANCE.OnCabbage = false;
        DefenseUIManager.INSTANCE.OnCarrot = false;
        ObjectPoolingManager.inst.ObjectDisappear();
    }

    #endregion

    public void Go_OffenseScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("OffenceScene");
        asyncOperation.allowSceneActivation = true;
    }


}
