using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Collider2D))]
public class Pickup : MonoBehaviour
{
    public UnityEvent _onPickedUp;
    public Transform _sprite;

    private void Start()
    {
        _sprite.DOScale(1.3f, 3.0f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        _onPickedUp.Invoke();
    }
}
