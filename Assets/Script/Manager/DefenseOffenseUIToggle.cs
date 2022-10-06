using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseOffenseUIToggle : MonoBehaviour
{
    [Header("[Defense]")]
    [SerializeField] GameObject DefenseBattleUIManager = null;
    [SerializeField] GameObject DEFMainCamera = null;

    [Header("[Offense]")]
    [SerializeField] GameObject OffenseUIManager = null;
    [SerializeField] GameObject CameraArm = null;
    //[SerializeField] GameObject PlayerUI = null;

    private void Start()
    {
        if (GameManager.INSTANCE.WANTINVASION)
        {
            CameraArm.SetActive(true);
            OffenseUIManager.SetActive(true);
            //PlayerUI.SetActive(true);
        }
        else if (GameManager.INSTANCE.INVASIONALLOW)
        {
            DEFMainCamera.SetActive(true);
            DefenseBattleUIManager.SetActive(true);
        }
    }
}
