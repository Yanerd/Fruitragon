using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    #region SingleTon
    
    private static ButtonManager instance = null;
    public static ButtonManager inst
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ButtonManager>();
                if (instance == null)
                {
                    instance = new GameObject("ButtonManager").AddComponent<ButtonManager>();
                }
            }
            return instance;
        }
    }

    #endregion

    [SerializeField] public bool buildingMode { get; set; } 
    [SerializeField] public bool OnPotato { get; set; }
    [SerializeField] public bool OnApple { get; set; }
    [SerializeField] public bool OnCabbage { get; set; }
    [SerializeField] public bool OnCarrot { get; set; }
    [SerializeField] public bool OnEggplant { get; set; }

    [SerializeField] public bool OnWater { get; set; }


    [SerializeField] public bool OnHouse { get; set; }
    [SerializeField] public bool OnWell { get; set; }

    [Header("[ UI ]")]
    [SerializeField] public GameObject Menu;
    [SerializeField] public GameObject buldingModeMenu;

    [SerializeField] GameObject []BulidingModeScroll;
    [SerializeField] GameObject[] BulidingModeScrollButton;
    [SerializeField] GameObject SearchRoomPage;

    #region BuildingMode ButtonEvent
    public void OpenScrollVegetable()
    {
        StartCoroutine(Gotrans(BulidingModeScroll[0]));
        SwitchButtonBack(0);
    }
    public void CloseScrollVegetable()
    {
        StartCoroutine(Backtrans(BulidingModeScroll[0]));
        SwitchButtonOpen(0);
    }
    public void OpenScrollBuilding()
    {
        StartCoroutine(Gotrans(BulidingModeScroll[1]));
        SwitchButtonBack(1);
    }
    public void CloseScrollBuilding()
    {
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
        while(true)
        {
            scroll.transform.position = 
                Vector3.Lerp(scroll.transform.position, scroll.transform.position + new Vector3(-100f, 0, 0), Time.deltaTime * 25f);
            yield return null;

            if(scroll.transform.position.x<=-350f)
            {
                yield break;
            }
        }
        
    }
    #endregion

    public void UpImage()
    {
        StartCoroutine(Uptrans(SearchRoomPage));
    }
    public void DownImage()
    {
        StartCoroutine(Downtrans(SearchRoomPage));
    }


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


    Ground ground;

    private void Awake()
    {
        ground = FindObjectOfType<Ground>();
        Initializing();
    }



    public void Go_buildingMode()
    {
        buildingMode = true;
        Menu.SetActive(false);
        buldingModeMenu.SetActive(true);
        //ground.Execute_bulidingMode();
    }
    public void Back_buildingMode()
    {
        buildingMode = false;
        Menu.SetActive(true);
        buldingModeMenu.SetActive(false);
        Initializing();
        ObjectPoolingManager.inst.ObjectDisappear();
        //ground.DeExecute_bulidingMode();
    }

    public void Go_OffenseScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("OffenceScene");
        asyncOperation.allowSceneActivation = true;
    }
    private void Initializing()
    {
        ButtonManager.inst.OnWell = false;
        ButtonManager.inst.OnPotato = false;
        ButtonManager.inst.OnHouse = false;
        ButtonManager.inst.OnPotato = false;
        ButtonManager.inst.OnApple = false;
        ButtonManager.inst.OnCabbage = false;
        ButtonManager.inst.OnCarrot = false;
        ButtonManager.inst.OnEggplant = false;
    }
    public void SelectWell()
    {
        CloseScrollBuilding();

        ButtonManager.inst.OnWell = true;

        ButtonManager.inst.OnPotato = false;
        ButtonManager.inst.OnHouse = false;
        ButtonManager.inst.OnPotato = false;
        ButtonManager.inst.OnApple = false;
        ButtonManager.inst.OnCabbage = false;
        ButtonManager.inst.OnCarrot = false;
        ButtonManager.inst.OnEggplant = false;
        ObjectPoolingManager.inst.ObjectDisappear();
    }
    public void SelectHouse()
    {
        CloseScrollBuilding();

        ButtonManager.inst.OnHouse = true;
        ButtonManager.inst.OnPotato = false;
        ButtonManager.inst.OnWell = false;
        ButtonManager.inst.OnApple = false;
        ButtonManager.inst.OnCabbage = false;
        ButtonManager.inst.OnCarrot = false;
        ButtonManager.inst.OnEggplant = false;
        ObjectPoolingManager.inst.ObjectDisappear();
    }
    public void SelectPotato()
    {
        CloseScrollVegetable();

        ButtonManager.inst.OnPotato = true;

        ButtonManager.inst.OnWell = false;
        ButtonManager.inst.OnHouse = false;
        ButtonManager.inst.OnApple = false;
        ButtonManager.inst.OnCabbage = false;
        ButtonManager.inst.OnCarrot = false;
        ButtonManager.inst.OnEggplant = false;
        ObjectPoolingManager.inst.ObjectDisappear();
    }
   
    public void SelectApple()
    {
        CloseScrollVegetable();

        ButtonManager.inst.OnApple = true;

        ButtonManager.inst.OnWell = false;
        ButtonManager.inst.OnPotato = false;
        ButtonManager.inst.OnHouse = false;
        ButtonManager.inst.OnPotato = false;
        ButtonManager.inst.OnCabbage = false;
        ButtonManager.inst.OnCarrot = false;
        ButtonManager.inst.OnEggplant = false;
        ObjectPoolingManager.inst.ObjectDisappear();
    }
    public void SelectCabbage()
    {
        CloseScrollVegetable();

        ButtonManager.inst.OnCabbage = true;

        ButtonManager.inst.OnWell = false;
        ButtonManager.inst.OnPotato = false;
        ButtonManager.inst.OnHouse = false;
        ButtonManager.inst.OnPotato = false;
        ButtonManager.inst.OnApple = false;
        ButtonManager.inst.OnCarrot = false;
        ButtonManager.inst.OnEggplant = false;
        ObjectPoolingManager.inst.ObjectDisappear();
    }
    public void SelectCarrot()
    {
        CloseScrollVegetable();

        ButtonManager.inst.OnCarrot = true;

        ButtonManager.inst.OnWell = false;
        ButtonManager.inst.OnPotato = false;
        ButtonManager.inst.OnHouse = false;
        ButtonManager.inst.OnPotato = false;
        ButtonManager.inst.OnApple = false;
        ButtonManager.inst.OnCabbage = false;
        ButtonManager.inst.OnEggplant = false;
        ObjectPoolingManager.inst.ObjectDisappear();
    }
    public void SelectEggplant()
    {
        CloseScrollVegetable();

        ButtonManager.inst.OnEggplant = true;

        ButtonManager.inst.OnWell = false;
        ButtonManager.inst.OnPotato = false;
        ButtonManager.inst.OnHouse = false;
        ButtonManager.inst.OnPotato = false;
        ButtonManager.inst.OnApple = false;
        ButtonManager.inst.OnCabbage = false;
        ButtonManager.inst.OnCarrot = false;
        ObjectPoolingManager.inst.ObjectDisappear();
    }

}
