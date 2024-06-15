using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Collider2D))]
public class Pickup : MonoBehaviour
{
    public UnityEvent _onPickedUp;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        _onPickedUp.Invoke();
    }
}
