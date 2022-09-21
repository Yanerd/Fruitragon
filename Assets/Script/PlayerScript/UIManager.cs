using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //components
    RectTransform cineUp = null;
    RectTransform cineDown = null;

    //variables value
    private bool islockon = false;
    private float addValue = 200f;

    Coroutine curCoroutine = null;

    //constant value
    Vector3 initUp;
    Vector3 initDown;

    private void Awake()
    {
        cineUp = GameObject.Find("CineViewUp").GetComponent<RectTransform>();
        cineDown = GameObject.Find("CineViewDown").GetComponent<RectTransform>();
    }

    private void Start()
    {
        initUp = cineUp.position;
        initDown = cineDown.position;
    }

    // Update is called once per frame
    void Update()
    {
        InputControll();
    }

    private void InputControll()
    {
        if (!islockon && Input.GetKeyDown(KeyCode.F))
        {
            islockon = true;

            curCoroutine = StartCoroutine(ProductionOn());
        }
        else if (islockon && Input.GetKeyDown(KeyCode.F))
        {
            islockon = false;

            curCoroutine = StartCoroutine(ProductionOff());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
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

            cineDown.position = Vector3.Lerp( cineDown.position, initDown + Vector3.up * addValue, 0.05f );
            cineUp.position = Vector3.Lerp(cineUp.position, initUp + Vector3.down * addValue, 0.05f);

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
}
