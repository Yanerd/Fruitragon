using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OffenseUIManager : MonoBehaviour
{
    //camera components
    GameObject cameraArm = null;
    Vector3 initCamDir = Vector3.zero;

    //Hp UI components
    Player player = null;
    Canvas playerUI = null;
    Slider hpSlider = null;
    Slider hpFollowSlider = null;

    //Dragon Hp UI components
    GameObject dragonHpGroupObj = null;
    Slider dragonHpSlider = null;
    Slider dragonHpFollowSlider = null;
    List<Dragon> fruitragons = null;

    //Cine Producton components
    RectTransform cineUp = null;
    RectTransform cineDown = null;

    //variables value
    public float ADDVALUE { get; set; }

    Coroutine curCoroutine = null;

    //constant value
    Vector3 initUp;
    Vector3 initDown;

    private void Awake()
    {
        //camera components
        cameraArm = GameObject.Find("CameraArm");

        //hp ui components
        player = FindObjectOfType<Player>();
        playerUI = GameObject.Find("PlayerUI").GetComponent<Canvas>();
        hpSlider = GameObject.Find("HpSlider").GetComponent<Slider>();
        hpFollowSlider = GameObject.Find("HpFollowSlider").GetComponent<Slider>();

        //dragon hp ui components
        fruitragons = new List<Dragon>();
        dragonHpGroupObj = GameObject.Find("DragonHp");
        dragonHpSlider = GameObject.Find("DragonHpSlider").GetComponent<Slider>();
        dragonHpFollowSlider = GameObject.Find("DragonHpFollowSlider").GetComponent<Slider>();

        //cine production components
        cineUp = GameObject.Find("CineViewUp").GetComponent<RectTransform>();
        cineDown = GameObject.Find("CineViewDown").GetComponent<RectTransform>();
    }

    private void Start()
    {
        //
        initCamDir = cameraArm.transform.forward;

        //player hp delegate
        player.playerEvent.callBackPlayerHPChangeEvent += OnChangedHp;

        //dragon hp delegate
        for (int i = 0; i < FindObjectsOfType<Dragon>().Length; i++)
        {
            fruitragons.Add(FindObjectsOfType<Dragon>()[i]);
            fruitragons[i].dragonEvent.callBackDragonHPChangeEvent += OnChangeDragonHP;
        }

        //dragon hp view init
        dragonHpGroupObj.SetActive(false);

        GameManager.INSTANCE.ISLOCKON = false;
        ADDVALUE = 200f;

        initUp = cineUp.position;
        initDown = cineDown.position;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerUIControll();
        CineProductControll();
    }


    private void PlayerUIControll()
    {
        if (GameManager.INSTANCE.ISLOCKON)
        {
            playerUI.transform.localRotation = Quaternion.LookRotation(initCamDir);
            //ui방향 고정 시키도록 하기
        }
        else 
        {
            playerUI.transform.LookAt(Camera.main.transform);
        }
    }
    private void CineProductControll()
    {
        if (GameManager.INSTANCE.ISLOCKON)
        {
            curCoroutine = StartCoroutine(ProductionOn());
        }
        else if (!GameManager.INSTANCE.ISLOCKON)
        {
            curCoroutine = StartCoroutine(ProductionOff());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("DefenseScene");
            asyncOperation.allowSceneActivation = true;
        }
    }
    IEnumerator ProductionOn()
    {
        float timer = 0f;

        if (curCoroutine != null)
        {
            StopCoroutine(curCoroutine);
        }

        while (true)
        {
            timer += Time.deltaTime;

            cineDown.position = Vector3.Lerp(cineDown.position, initDown + Vector3.up * ADDVALUE, 0.05f);
            cineUp.position = Vector3.Lerp(cineUp.position, initUp + Vector3.down * ADDVALUE, 0.05f);

            yield return null;
            if (timer > 0.8f) yield break;
        }
    }
    IEnumerator ProductionOff()
    {
        float timer = 0f;

        if (curCoroutine != null)
        {
            StopCoroutine(curCoroutine);
        }

        while (true)
        {
            timer += Time.deltaTime;

            cineDown.position = Vector3.Lerp(cineDown.position, initDown, 0.05f);
            cineUp.position = Vector3.Lerp(cineUp.position, initUp, 0.05f);

            yield return null;
            if (timer > 0.8f) yield break;
        }
    }

    //oversee function
    private void OnChangeDragonHP(float curHp, float maxHp)
    {
        dragonHpGroupObj.SetActive(true);
        dragonHpSlider.value = curHp / maxHp;
        StartCoroutine(DragonFollowSlider());
    }

    IEnumerator DragonFollowSlider()
    {
        while (true)
        {
            dragonHpFollowSlider.value = Mathf.Lerp(dragonHpFollowSlider.value, dragonHpSlider.value, Time.deltaTime / 2f);

            if (dragonHpFollowSlider.value == dragonHpSlider.value)
            {
                yield break;
            }

            yield return null;
        }
    }

    private void OnChangedHp(float curHp, float maxHp)
    {
        hpSlider.value = curHp / maxHp;
        StartCoroutine(FollowSlider());
    }

    IEnumerator FollowSlider()
    {
        while (true)
        {
            hpFollowSlider.value = Mathf.Lerp(hpFollowSlider.value, hpSlider.value, Time.deltaTime / 5f);

            if (hpFollowSlider.value == hpSlider.value) yield break;

            yield return null;
        }
    }
}


