using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
  [SerializeField] private Transform _sprite;
  [SerializeField] private Transform _contact;
  [SerializeField] private float _shakeDuration = 0.5f;
  [SerializeField] private float _fallingDuration = 2f;
  [SerializeField] private float _gravity = 1f;
  [SerializeField] private float _maxFallVelocity = 5f;
  [SerializeField] private float _shakeDistance = 0.05f;

  private Vector2 _initialPosition;
  private Rigidbody2D _rigidbody2D;
  private float _shakeCooldown = 0;
  private float _fallingCooldown = 0;

  private void Awake()
  {
    _initialPosition = transform.position;
    _rigidbody2D = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    if (_shakeCooldown > 0)
    {
      _shakeCooldown -= Time.deltaTime;
      _sprite.localPosition = _sprite.localPosition.x == _shakeDistance
       ? new Vector3(-_shakeDistance, -_shakeDistance, _sprite.localPosition.z)
       : new Vector3(_shakeDistance, _shakeDistance, _sprite.localPosition.z);

      if (_shakeCooldown < 0)
      {
        _shakeCooldown = 0;
        _fallingCooldown = _fallingDuration;
        _sprite.localPosition = new Vector3(0, 0, _sprite.localPosition.z);
      }
    }

    if (_fallingCooldown > 0)
    {
      _fallingCooldown -= Time.deltaTime;
      if (_fallingCooldown < 0)
      {
        Reset();
      }
    }
  }

  private void FixedUpdate()
  {
    if (_fallingCooldown > 0)
    {
      _rigidbody2D.velocity = new Vector2(
      0,
      Mathf.Min(_maxFallVelocity, _rigidbody2D.velocity.y - _gravity * Time.fixedDeltaTime));
    }
  }

  public void Reset()
  {
    Player player = FindAnyObjectByType<Player>();
    if (player && player.transform.parent == _contact) player.transform.parent = null;

    transform.position = _initialPosition;
    _rigidbody2D.velocity = Vector2.zero;
    _fallingCooldown = 0;
    _shakeCooldown = 0;

  }

  public void Shake()
  {
    _shakeCooldown = _shakeDuration;
  }
}
