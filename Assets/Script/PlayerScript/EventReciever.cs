using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnAttackStartEvent();
public delegate void OnAttackEndEvent();

public class EventReciever : MonoBehaviour
{
    public OnAttackEndEvent callBackAttackEndEvent = null;
    public OnAttackStartEvent callBackAttackStartEvent = null;
    
    public void AttackStartEvent()
    {
        if(callBackAttackStartEvent != null)
            callBackAttackStartEvent();
    }
    public void AttackEndEvent()
    {
        if (callBackAttackEndEvent != null)
            callBackAttackEndEvent();
    }
}
