using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActiveFalse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //first set active false for start scene
        if (GameManager.INSTANCE.ISGAMEIN == false)
        {
            this.gameObject.SetActive(false);
        }
        
    }
}
