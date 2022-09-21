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

    public bool buildingMode = false;
    public bool OnPotato = false;
    public bool OnHouse = false;
    public bool OnWell = false;
    
    [Header("[ UI ]")]
    [SerializeField] public GameObject Menu;
    [SerializeField] public GameObject buldingModeMenu;

    Ground ground;

    private void Awake()
    {
        ground = FindObjectOfType<Ground>();
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
        OnWell = false;
        OnPotato = false;
        OnHouse = false;
       //ground.DeExecute_bulidingMode();
    }
    public void Go_OffenseScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("OffenceScene");
        asyncOperation.allowSceneActivation = true;
    }

    public void install_Well()
    {

        ButtonManager.inst.OnWell = true;
        OnPotato = false;
        OnHouse = false;
    }
    public void install_Potato()
    {
        OnPotato = true;
        OnWell = false;
        OnHouse = false;
    }
    public void install_House()
    {
        OnHouse = true;
        OnPotato = false;
        OnWell = false;
    }



}
