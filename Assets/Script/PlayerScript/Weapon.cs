using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float damage = 10f;

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dragon"))
        {
            other.SendMessage("TransferDamage",damage,SendMessageOptions.DontRequireReceiver);
        }
    }
}
