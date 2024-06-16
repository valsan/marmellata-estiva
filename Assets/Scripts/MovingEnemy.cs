using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
  [SerializeField] private Transform _enemy;
  [SerializeField] private Transform _positionA;
  [SerializeField] private Transform _positionB;

  [SerializeField] private float _movingSpeed = 5.0f;

  private Transform _target;

  private void Awake()
  {
    _target = _positionA;
  }

  private void Update()
  {
    if (Vector2.Distance(_enemy.position, _target.position) <= 0.0f)
    {
      _target = _target == _positionA ? _positionB : _positionA;
      Quaternion localRotation = _enemy.transform.localRotation;
      localRotation.y = _enemy.localRotation.y == 0 ? 180 : 0;
      _enemy.transform.localRotation = localRotation;
    }
  }

  private void FixedUpdate()
  {
    _enemy.position = Vector2.MoveTowards(_enemy.position, _target.position, _movingSpeed * Time.fixedDeltaTime);
  }
}
