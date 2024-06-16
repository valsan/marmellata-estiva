using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using FMODUnity;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Collider2D))]
public class Pickup : MonoBehaviour
{
    public UnityEvent _onPickedUp;
    public Transform _sprite;
    [SerializeField] float _rotationSpeed = 180.0f;
    [SerializeField] private FloatValue _battery;
    [SerializeField] private float _pickpBatteryAmount =  10.0f;
    [SerializeField] private EventReference _pickupSFX;

    private void Start()
    {
        _sprite.DOScale(1.3f, 3.0f).SetLoops(-1, LoopType.Yoyo);
        _sprite.DORotate(_sprite.rotation.eulerAngles + (Vector3.forward * _rotationSpeed), 3.0f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<Player>(out Player player))
        {
            GetComponent<Collider2D>().enabled = false;
            _battery.Value = Mathf.Min(_battery.Value + _pickpBatteryAmount, 100.0f);
            RuntimeManager.PlayOneShot(_pickupSFX);
            _onPickedUp.Invoke();
            _sprite.gameObject.SetActive(false);
            Destroy(gameObject, 1.0f);
        }
    }
}
