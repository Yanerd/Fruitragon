using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActiveFalse : MonoBehaviour
{
    // Start is called before the first frame update

    private void Update()
    {
        //first set active false for start scene
        if (GameManager.INSTANCE.SCENENUM == 0)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
        
    }
}
