using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform _platform;
    [SerializeField] private Transform _positionA;
    [SerializeField] private Transform _positionB;

    [SerializeField] private float _movingSpeed = 5.0f;

    private Transform _target;

    private void Awake()
    {
        _target = _positionB;
    }

    private void Update()
    {
        if (Vector2.Distance(_platform.position, _target.position) <= 0.0f)
        {
            _target = _target == _positionA ? _positionB : _positionA;
        }
    }

    private void FixedUpdate()
    {
        _platform.position = Vector2.MoveTowards(_platform.position, _target.position, _movingSpeed * Time.fixedDeltaTime);
    }
}
