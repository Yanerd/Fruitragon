using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseOffenseUIToggle : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.INSTANCE.WANTINVASION)
        {
            if (this.gameObject.name == "DEFMainCamera" || this.gameObject.name == "DefenseBattleUIManager")
            {
                this.gameObject.SetActive(false);
            }
        }
        else if (GameManager.INSTANCE.INVASIONALLOW)
        {
            if (this.gameObject.name == "CameraArm" || this.gameObject.name == "OffenseUIManager" || this.gameObject.name == "PlayerUI")
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
