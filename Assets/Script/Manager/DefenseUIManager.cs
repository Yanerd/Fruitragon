using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DefenseUIManager : MonoSingleTon<DefenseUIManager>
{
    #region Click Object Value
    [SerializeField] public bool BUILDINGMODE { get; set; } 
    [SerializeField] public bool onPOTATO     { get; set; }
    [SerializeField] public bool onAPPLE      { get; set; }
    [SerializeField] public bool onCABBAGE    { get; set; }
    [SerializeField] public bool onCARROT     { get; set; }
    [SerializeField] public bool onEEGPLANT   { get; set; }
    [SerializeField] public bool onWATER      { get; set; }
    [SerializeField] public bool onHOUSE      { get; set; }
    [SerializeField] public bool onWELL       { get; set; }
    [SerializeField] public bool WATERRAY     { get; set; }
    #endregion

    #region Ui
    [Header("[ UI ]")]
    [SerializeField] GameObject GardeningMenu;
    [SerializeField] GameObject BulidingModeMenu;
    [SerializeField] GameObject StorePage;
    //StorPage Func & Sell & purchase
    /////////////////////////////////////////////////
    #region StorePage Menu
    [Header("[Store Menu]")]
    [SerializeField] Button StoreVegetablePage;
    [SerializeField] GameObject VegetableScrollView;

    [SerializeField] Button StoreBuildingPage;
    [SerializeField] GameObject BuildingScrollView;

    [SerializeField] Button StoreGroundPage;
    [SerializeField] GameObject GroundScrollView;
    #endregion
    #region Vegetable Sell & Buy
    [Header("[PotatoDraon & Seed]")]
    [SerializeField] Button PotatoDragonSellButton;
    [SerializeField] Button PotatoSeedBuyButton;
    [SerializeField] TextMeshProUGUI PotatoCount;

    [Header("[AppleDraon & Seed]")]
    [SerializeField] Button AppleDragonSellButton;
    [SerializeField] Button AppleSeedBuyButton;
    [SerializeField] TextMeshProUGUI AppleCount;

    [Header("[CabbageDraon & Seed]")]
    [SerializeField] Button CabbageDragonSellButton;
    [SerializeField] Button CabbageSeedBuyButton;
    [SerializeField] TextMeshProUGUI CabbageCount;

    [Header("[CarrotDraon & Seed]")]
    [SerializeField] Button CarrotDragonSellButton;
    [SerializeField] Button CarrotSeedBuyButton;
    [SerializeField] TextMeshProUGUI CarrotCount;

    [Header("[EggplantDraon & Seed]")]
    [SerializeField] Button EggplantDragonSellButton;
    [SerializeField] Button EggplantSeedBuyButton;
    [SerializeField] TextMeshProUGUI EggplantCount;
    #endregion
    #region Building,Ground Sell & Buy
    [Header("Building & Ground Sell,Buy Button")]
    [SerializeField] Button HouseSellButton;
    [SerializeField] Button HouseBuyButton;
    [SerializeField] TextMeshProUGUI curHouseCount;

    [SerializeField] Button WellSellButton;
    [SerializeField] Button WellBuyButton;
    [SerializeField] TextMeshProUGUI curWellCount;

    [SerializeField] Button GroundBuyButton;
    [SerializeField] TextMeshProUGUI curGroundState;
    #endregion
    [Header("Ground State")]
    [SerializeField] GameObject[] fence;
    [SerializeField] GameObject[] tree;
    [SerializeField] int MapState;
    /////////////////////////////////////////////////
    #region BuildingMode Scroll & Button
    [Header("[Vegetable Menu]")]
    [SerializeField] GameObject VegetableScroll;
    [SerializeField] GameObject VegetableScrollOpenButton;
    [SerializeField] GameObject VegetableScrollCloseButton;

    [Header("[Building Menu]")]
    [SerializeField] GameObject BuildingScroll;
    [SerializeField] GameObject BuildingScrollOpenButton;
    [SerializeField] GameObject BuildingScrollCloseButton;

    [Header("BuildingMode inst Button")]
    [SerializeField] Button[] InstButton;
    #endregion

    //object curCount
    /////////////////////////////////////////////////
    [Header("Cur DragonCount")]
    [SerializeField] TextMeshProUGUI curPotato;
    [SerializeField] TextMeshProUGUI curApple;
    [SerializeField] TextMeshProUGUI curCabbage;
    [SerializeField] TextMeshProUGUI curCarrot;
    [SerializeField] TextMeshProUGUI curEggplant;
    [SerializeField] TextMeshProUGUI curHouse;
    [SerializeField] TextMeshProUGUI curWell;
    /////////////////////////////////////////////////
    #endregion

    [Header("[GameMoney(Zera)]")]
    [SerializeField] TextMeshProUGUI GameMoney;

    //dragon data
    /////////////////////////////////////////////////////
    #region Dragon List

    [SerializeField] public List<GameObject> potatoDragonList = new List<GameObject>();
    [SerializeField] public List<GameObject> appleDragonList = new List<GameObject>();
    [SerializeField] public List<GameObject> cabbageDragonList = new List<GameObject>();
    [SerializeField] public List<GameObject> carrotDragonList = new List<GameObject>();
    [SerializeField] public List<GameObject> eggplantDragonList = new List<GameObject>();

    #endregion
    [SerializeField] DragonData[] dragonData = new DragonData[5];
    [SerializeField] VegetableData[] vegetableData = new VegetableData[5];
    /////////////////////////////////////////////////////

    //GameManager value
    /////////////////////////////////////////////////////
    int Gold;
    int groundPrice = 1000;
    int housePrice = 500;
    int wellPrice = 500;

    int potatoSeedCount;
    int appleSeedCount;
    int cabbageSeedCount;
    int carrotSeedCount;
    int eggplantSeedCount;
    int houseCount;
    int wellCount;
    /////////////////////////////////////////////////////

    CursorChange myCursor;
    Vector3 VegetableMenuOriginPos;
    Vector3 BuildingMenuOriginPos;
    Vector3 originMenuPos;
    bool onMenu;

    private void Awake()
    {
        MapState = 5;
        Gold = 5000;
        GameMoney.text = Gold.ToString();

        //button is clicked Permit
        onMenu = false;
        WATERRAY = false;

        //Cursor Change Image 
        myCursor = FindObjectOfType<CursorChange>();

        //Reset Value
        Initializing();

        //Back to the Defense Scene transform initializing Scroll
        VegetableMenuOriginPos = VegetableScroll.transform.position;
        BuildingMenuOriginPos = BuildingScroll.transform.position;

    }

    //Value Reset 
    /////////////////////////////////////////////////////
    private void Initializing()
    {
        DefenseUIManager.INSTANCE.onWELL = false;
        DefenseUIManager.INSTANCE.onPOTATO = false;
        DefenseUIManager.INSTANCE.onHOUSE = false;
        DefenseUIManager.INSTANCE.onPOTATO = false;
        DefenseUIManager.INSTANCE.onAPPLE = false;
        DefenseUIManager.INSTANCE.onCABBAGE = false;
        DefenseUIManager.INSTANCE.onCARROT = false;
        DefenseUIManager.INSTANCE.onEEGPLANT = false;
    }
    private void BringObjectCount()
    {
        curGroundState.text = MapState.ToString();
        PotatoCount.text = potatoDragonList.Count.ToString();
        AppleCount.text = appleDragonList.Count.ToString();
        CabbageCount.text = cabbageDragonList.Count.ToString();
        CarrotCount.text = carrotDragonList.Count.ToString();
        EggplantCount.text = eggplantDragonList.Count.ToString();
        curHouseCount.text = houseCount.ToString();
        curWellCount.text = wellCount.ToString();

        curPotato.text = "Poato : " + potatoSeedCount.ToString();
        curApple.text = "Apple : " + appleSeedCount.ToString();
        curCarrot.text = "Carrot : " + carrotSeedCount.ToString();
        curCabbage.text = "Cabbage : " + cabbageSeedCount.ToString();
        curEggplant.text = "Eggplant : " + eggplantSeedCount.ToString();
        curWell.text = "Well : " + wellCount.ToString();
        curHouse.text = "House : " + houseCount.ToString();
        GameMoney.text = Gold.ToString();
    }
    /////////////////////////////////////////////////////


    #region BuildingMode Scroll Func

    //Vegetable Scroll Open&Close
    /////////////////////////////////////////////////////////////////
    public void OpenScrollVegetable()
    {
        BuildingScrollOpenButton.GetComponent<Button>().interactable = false;

        StartCoroutine(OpenScroll(VegetableScroll));

        SwitchBackButton(VegetableScrollOpenButton);
    }
    public void CloseScrollVegetable()
    {
        BuildingScrollOpenButton.GetComponent<Button>().interactable = true;
        StartCoroutine(CloseScroll(VegetableScroll));
        SwitchOpenButton(VegetableScrollCloseButton);
    }
    /////////////////////////////////////////////////////////////////

    //Building Scroll Open&Close
    /////////////////////////////////////////////////////////////////
    public void OpenScrollBuilding()
    {
        VegetableScrollOpenButton.GetComponent<Button>().interactable = false;

        StartCoroutine(OpenScroll(BuildingScroll));

        SwitchBackButton(BuildingScrollOpenButton);
    }
    public void CloseScrollBuilding()
    {
        VegetableScrollOpenButton.GetComponent<Button>().interactable = true;
        StartCoroutine(CloseScroll(BuildingScroll));
        SwitchOpenButton(BuildingScrollCloseButton);
    }
    /////////////////////////////////////////////////////////////////

    //Scroll Button Chane (Switch Go & Back)
    /////////////////////////////////////////////////////////////////
    void SwitchBackButton(GameObject button)
    {
        if(button == VegetableScrollOpenButton)
        {
            VegetableScrollOpenButton.SetActive(false);
            VegetableScrollCloseButton.SetActive(true);
        }
        else if (button == BuildingScrollOpenButton)
        {
            BuildingScrollOpenButton.SetActive(false);
            BuildingScrollCloseButton.SetActive(true);
        }
    }
    void SwitchOpenButton(GameObject button)
    {
        if (button == VegetableScrollCloseButton)
        {
            VegetableScrollOpenButton.SetActive(true);
            VegetableScrollCloseButton.SetActive(false);
        }
        else if (button == BuildingScrollCloseButton)
        {
            BuildingScrollOpenButton.SetActive(true);
            BuildingScrollCloseButton.SetActive(false);
        }
    }
    /////////////////////////////////////////////////////////////////

    #endregion
    #region BuildingMenu Open&Close Corutine

    IEnumerator OpenScroll(GameObject scroll)
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
    IEnumerator CloseScroll(GameObject scroll)
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

    //Building Mode & home
    /////////////////////////////////////////////////////
    public void Go_buildingMode()
    {
        if (onMenu) return;
        onMenu = true;

        BringObjectCount();
        InstButtonTurnOff();
        BUILDINGMODE = true;
        GardeningMenu.SetActive(false);
        BulidingModeMenu.SetActive(true);
    }
    public void Back_buildingMode()
    {
        if (!onMenu) return;
        if (onMenu)
            onMenu = false;

        VegetableScroll.transform.position = VegetableMenuOriginPos;
        BuildingScroll.transform.position = BuildingMenuOriginPos;
        SwitchOpenButton(VegetableScrollCloseButton);
        SwitchOpenButton(BuildingScrollCloseButton);

        BuildingScrollOpenButton.GetComponent<Button>().interactable = true;
        VegetableScrollOpenButton.GetComponent<Button>().interactable = true;

        BUILDINGMODE = false;
        GardeningMenu.SetActive(true);
        BulidingModeMenu.SetActive(false);
        Initializing();
        ObjectPoolingManager.inst.ObjectDisappear();
        myCursor.BasicCursor();
    }
    void InstButtonTurnOff()
    {
        if (potatoSeedCount <= 0)
        {
            InstButton[0].interactable = false;
        }
        else
        {
            InstButton[0].interactable = true;
        }

        if (appleSeedCount <= 0)
        {
            InstButton[1].interactable = false;
        }
        else
        {
            InstButton[1].interactable = true;
        }

        if (cabbageSeedCount <= 0)
        {
            InstButton[2].interactable = false;
        }
        else
        {
            InstButton[2].interactable = true;
        }

        if (carrotSeedCount <= 0)
        {
            InstButton[3].interactable = false;
        }
        else
        {
            InstButton[3].interactable = true;
        }

        if (eggplantSeedCount <= 0)
        {
            InstButton[4].interactable = false;
        }
        else
        {
            InstButton[4].interactable = true;
        }

    }
    /////////////////////////////////////////////////////
    #region UI Transform Up&Down Corutine
    IEnumerator Uptrans(GameObject page)
    {
        while (true)
        {
            page.transform.position =
                Vector3.Lerp(page.transform.position, page.transform.position + new Vector3(0, 100f, 0), Time.deltaTime * 20f);
            yield return null;

            if (page.transform.position.y >= 540f)
            {
                yield break;
            }
        }

    }
    IEnumerator Downtrans(GameObject page)
    {
        while (true)
        {
            page.transform.position =
                Vector3.Lerp(page.transform.position, page.transform.position + new Vector3(0, -100f, 0), Time.deltaTime * 30f);
            yield return null;

            if (page.transform.position.y <= -540f)
            {
                yield break;
            }
        }

    }
    #endregion

    
    //StorePage
    /////////////////////////////////////////////////////
    #region ClickStorePage
    public void ClickStoreVegetablePage()
    {
        VegetableScrollView.SetActive(true);
        BuildingScrollView.SetActive(false);
        GroundScrollView.SetActive(false);

        StoreVegetablePage.interactable = false;
        StoreBuildingPage.interactable = true;
        StoreGroundPage.interactable = true;
    }
    public void ClickStoreBildingPage()
    {
        BuildingScrollView.SetActive(true);
        VegetableScrollView.SetActive(false);
        GroundScrollView.SetActive(false);

        StoreVegetablePage.interactable = true;
        StoreBuildingPage.interactable = false;
        StoreGroundPage.interactable = true;
    }
    public void ClickStoreGroundPage()
    {
        GroundScrollView.SetActive(true);
        VegetableScrollView.SetActive(false);
        BuildingScrollView.SetActive(false);

        StoreVegetablePage.interactable = true;
        StoreBuildingPage.interactable = true;
        StoreGroundPage.interactable = false;
    }
    #endregion
    #region SellObject
    public void SellPotatoDragon()
    {
        if (potatoDragonList.Count == 0) return;

        Gold += dragonData[0].SalePrice;
        GameMoney.text = Gold.ToString();

        ObjectPoolingManager.inst.Destroy(potatoDragonList[0]);
        potatoDragonList.RemoveAt(0);
        BringObjectCount();
    }
    public void SellAppleDragon()
    {
        if (appleDragonList.Count == 0) return;

        Gold += dragonData[1].SalePrice;
        GameMoney.text = Gold.ToString();

        ObjectPoolingManager.inst.Destroy(appleDragonList[0]);
        appleDragonList.RemoveAt(0);
        BringObjectCount();
    }
    public void SellCabbageDragon()
    {
        if (cabbageDragonList.Count == 0) return;

        Gold += dragonData[2].SalePrice;
        GameMoney.text = Gold.ToString();

        ObjectPoolingManager.inst.Destroy(cabbageDragonList[0]);
        cabbageDragonList.RemoveAt(0);
        BringObjectCount();
    }
    public void SellCarrotDragon()
    {
        if (carrotDragonList.Count == 0) return;

        Gold += dragonData[3].SalePrice;
        GameMoney.text = Gold.ToString();

        ObjectPoolingManager.inst.Destroy(carrotDragonList[0]);
        carrotDragonList.RemoveAt(0);
        BringObjectCount();
    }
    public void SellEggplantDragon()
    {
        if (eggplantDragonList.Count == 0) return;

        Gold += dragonData[4].SalePrice;
        GameMoney.text = Gold.ToString();

        ObjectPoolingManager.inst.Destroy(eggplantDragonList[0]);
        eggplantDragonList.RemoveAt(0);
        BringObjectCount();
    }
    #endregion
    #region BuyObject
    public void BuyPotatoSeed()
    {
        potatoSeedCount++;
        Gold -= vegetableData[0].PurchasePrice;
        GameMoney.text = Gold.ToString();
    }
    public void BuyAppleSeed()
    {
        appleSeedCount++;
        Gold -= vegetableData[1].PurchasePrice;
        GameMoney.text = Gold.ToString();
    }
    public void BuyCabbageSeed()
    {
        cabbageSeedCount++;
        Gold -= vegetableData[2].PurchasePrice;
        GameMoney.text = Gold.ToString();
    }
    public void BuyCarrotSeed()
    {
        carrotSeedCount++;
        Gold -= vegetableData[3].PurchasePrice;
        GameMoney.text = Gold.ToString();
    }
    public void BuyEggplantSeed()
    {
        eggplantSeedCount++;
        Gold -= vegetableData[4].PurchasePrice;
        GameMoney.text = Gold.ToString();
    }
    public void BuyHouse()
    {
        houseCount++;
        Gold -= housePrice;
        GameMoney.text = Gold.ToString();
        BringObjectCount();
    }
    public void BuyWell()
    {
        wellCount++;
        Gold -= wellPrice;
        GameMoney.text = Gold.ToString();
        BringObjectCount();
    }
    public void BuyGround()
    {
        if (MapState == 0) return;

        Gold -= groundPrice;
        GameMoney.text = Gold.ToString();

        if (MapState == 5)
        {
            fence[0].SetActive(false);
            fence[1].SetActive(true);
            tree[0].SetActive(false);
            MapState = 4;
            curGroundState.text = MapState.ToString();
        }
        else if (MapState == 4)
        {
            fence[1].SetActive(false);
            fence[2].SetActive(true);
            tree[1].SetActive(false);
            MapState = 3;
            curGroundState.text = MapState.ToString();
        }
        else if (MapState == 3)
        {
            fence[2].SetActive(false);
            fence[3].SetActive(true);
            tree[2].SetActive(false);
            MapState = 2;
            curGroundState.text = MapState.ToString();
        }
        else if (MapState == 2)
        {
            fence[3].SetActive(false);
            fence[4].SetActive(true);
            tree[3].SetActive(false);
            MapState = 1;
            curGroundState.text = MapState.ToString();
        }
        else if (MapState == 1)
        {
            fence[4].SetActive(false);

            GroundBuyButton.interactable = false;
            MapState = 0;
            curGroundState.text = MapState.ToString();
        }

    }
    #endregion
    public void UpStorePage()
    {
        if (onMenu)
        {
            DownStorePage();
            return;
        }
        onMenu = true;

        StoreVegetablePage.interactable = false;
        BuildingScrollView.SetActive(false);
        GroundScrollView.SetActive(false);

        BringObjectCount();
        StartCoroutine(Uptrans(StorePage));
    }
    public void DownStorePage()
    {
        if (!onMenu) return;
        if (onMenu)
            onMenu = false;
        ClickStoreVegetablePage();
        StartCoroutine(Downtrans(StorePage));
    }
    /////////////////////////////////////////////////////
    #region Select Building&Vegetable Button
    public void SelectWell()
    {
        if (wellCount <=0) return;
        wellCount--;
        InstButtonTurnOff();
        curWell.text = "Well : " + wellCount.ToString();

        CloseScrollBuilding();
        //myCursor.BuildingCursor();
        DefenseUIManager.INSTANCE.onWELL = true;
        DefenseUIManager.INSTANCE.onPOTATO = false;
        DefenseUIManager.INSTANCE.onHOUSE = false;
        DefenseUIManager.INSTANCE.onPOTATO = false;
        DefenseUIManager.INSTANCE.onAPPLE = false;
        DefenseUIManager.INSTANCE.onCABBAGE = false;
        DefenseUIManager.INSTANCE.onCARROT = false;
        DefenseUIManager.INSTANCE.onEEGPLANT = false;
        ObjectPoolingManager.inst.ObjectDisappear();
    }
    public void SelectHouse()
    {
        if (houseCount <= 0) return;
        houseCount--;
        InstButtonTurnOff();
        curHouse.text = "House : " + houseCount.ToString();

        CloseScrollBuilding();
        //myCursor.BuildingCursor();
        DefenseUIManager.INSTANCE.onHOUSE = true;
        DefenseUIManager.INSTANCE.onPOTATO = false;
        DefenseUIManager.INSTANCE.onWELL = false;
        DefenseUIManager.INSTANCE.onAPPLE = false;
        DefenseUIManager.INSTANCE.onCABBAGE = false;
        DefenseUIManager.INSTANCE.onCARROT = false;
        DefenseUIManager.INSTANCE.onEEGPLANT = false;
        ObjectPoolingManager.inst.ObjectDisappear();
    }
    public void SelectPotato()
    {
        if (potatoSeedCount <= 0) return;
        potatoSeedCount--;
        InstButtonTurnOff();
        curPotato.text = "Poato : "+potatoSeedCount.ToString();

        CloseScrollVegetable();
        //myCursor.VegetableCursor();
        DefenseUIManager.INSTANCE.onPOTATO = true;
        DefenseUIManager.INSTANCE.onWELL = false;
        DefenseUIManager.INSTANCE.onHOUSE = false;
        DefenseUIManager.INSTANCE.onAPPLE = false;
        DefenseUIManager.INSTANCE.onCABBAGE = false;
        DefenseUIManager.INSTANCE.onCARROT = false;
        DefenseUIManager.INSTANCE.onEEGPLANT = false;
        ObjectPoolingManager.inst.ObjectDisappear();
    }
    public void SelectApple()
    {
        if (appleSeedCount <= 0) return;
        appleSeedCount--;
        InstButtonTurnOff();
        curApple.text = "Apple : " + appleSeedCount.ToString();

        CloseScrollVegetable();
        //myCursor.VegetableCursor();
        DefenseUIManager.INSTANCE.onAPPLE = true;
        DefenseUIManager.INSTANCE.onWELL = false;
        DefenseUIManager.INSTANCE.onPOTATO = false;
        DefenseUIManager.INSTANCE.onHOUSE = false;
        DefenseUIManager.INSTANCE.onPOTATO = false;
        DefenseUIManager.INSTANCE.onCABBAGE = false;
        DefenseUIManager.INSTANCE.onCARROT = false;
        DefenseUIManager.INSTANCE.onEEGPLANT = false;
        ObjectPoolingManager.inst.ObjectDisappear();
    }
    public void SelectCabbage()
    {
        if (cabbageSeedCount <= 0) return;
        cabbageSeedCount--;
        InstButtonTurnOff();

        curCabbage.text = "Cabbage : " + cabbageSeedCount.ToString();

        CloseScrollVegetable();
        //myCursor.VegetableCursor();
        DefenseUIManager.INSTANCE.onCABBAGE = true;
        DefenseUIManager.INSTANCE.onWELL = false;
        DefenseUIManager.INSTANCE.onPOTATO = false;
        DefenseUIManager.INSTANCE.onHOUSE = false;
        DefenseUIManager.INSTANCE.onPOTATO = false;
        DefenseUIManager.INSTANCE.onAPPLE = false;
        DefenseUIManager.INSTANCE.onCARROT = false;
        DefenseUIManager.INSTANCE.onEEGPLANT = false;
        ObjectPoolingManager.inst.ObjectDisappear();
    }
    public void SelectCarrot()
    {
        if (carrotSeedCount <= 0) return;
        carrotSeedCount--;
        InstButtonTurnOff();

        curCarrot.text = "Carrot : " + carrotSeedCount.ToString();

        CloseScrollVegetable();
        //myCursor.VegetableCursor();
        DefenseUIManager.INSTANCE.onCARROT = true;
        DefenseUIManager.INSTANCE.onWELL = false;
        DefenseUIManager.INSTANCE.onPOTATO = false;
        DefenseUIManager.INSTANCE.onHOUSE = false;
        DefenseUIManager.INSTANCE.onPOTATO = false;
        DefenseUIManager.INSTANCE.onAPPLE = false;
        DefenseUIManager.INSTANCE.onCABBAGE = false;
        DefenseUIManager.INSTANCE.onEEGPLANT = false;
        ObjectPoolingManager.inst.ObjectDisappear();
    }
    public void SelectEggplant()
    {
        if (eggplantSeedCount <= 0) return;
        eggplantSeedCount--;
        InstButtonTurnOff();

        curEggplant.text = "Eggplant : " + eggplantSeedCount.ToString();

        CloseScrollVegetable();
        //myCursor.VegetableCursor();
        DefenseUIManager.INSTANCE.onEEGPLANT = true;
        DefenseUIManager.INSTANCE.onWELL = false;
        DefenseUIManager.INSTANCE.onPOTATO = false;
        DefenseUIManager.INSTANCE.onHOUSE = false;
        DefenseUIManager.INSTANCE.onPOTATO = false;
        DefenseUIManager.INSTANCE.onAPPLE = false;
        DefenseUIManager.INSTANCE.onCABBAGE = false;
        DefenseUIManager.INSTANCE.onCARROT = false;
        ObjectPoolingManager.inst.ObjectDisappear();
    }
    #endregion

 


}
