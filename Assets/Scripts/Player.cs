using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
  [Header("References")]
  [SerializeField]
  private Rigidbody2D _rigidbody2D;
  [SerializeField]
  private BoxCollider2D _boxCollider2D;
  private Animator _animator;

  [Header("Customization")]
  [SerializeField]
  private LayerMask _groundLayerMask;
  public float _moveSpeed = 5f;
  [SerializeField]
  private float _jumpForce = 10f;
  [SerializeField]
  private float _jumpDelay = 0.01f;
  [SerializeField]
  private float _coyoteTime = 0.5f;
  [SerializeField]
  private float _gravityUp = 1f;
  [SerializeField]
  private float _gravityDown = 1f;
  [SerializeField]
  private float _gravityDamp = 0.5f;
  [SerializeField]
  private float _maxFallVelocity = -10f;

  private bool _isGrounded = false;
  private float _timeSinceLastGrounded = 0f;
  private int _direction = 0;
  private float _jumpCooldown = 0f;
  private float _jumpQueuedDelay = 0f;
  private bool _isDampingGravity = false;

  private void Start()
  {
    _rigidbody2D = GetComponent<Rigidbody2D>();
    _animator = GetComponent<Animator>();

    _animator.SetBool("isGrounded", true);
    _animator.SetBool("isMoving", false);
  }

  public void OnJump(InputAction.CallbackContext context)
  {
    if (context.canceled)
    {
      _isDampingGravity = false;
    }
    else if (context.started)
    {
      _isDampingGravity = true;
    }

    if (_timeSinceLastGrounded > 0 && _jumpCooldown == 0)
    {
      _animator.SetBool("isGrounded", false);
      _isDampingGravity = true;
      _timeSinceLastGrounded = 0;
      _jumpCooldown = _jumpDelay + _coyoteTime + 0.01f;
      _jumpQueuedDelay = _jumpDelay;
    }
  }

  public void OnMove(InputAction.CallbackContext context)
  {
    var direction = context.ReadValue<float>();
    if (direction > 0)
    {
      Quaternion localRotation = transform.localRotation;
      localRotation.y = 0;
      transform.localRotation = localRotation;
      this._direction = 1;
    }
    else if (direction < 0)
    {
      Quaternion localRotation = transform.localRotation;
      localRotation.y = 180;
      transform.localRotation = localRotation;
      this._direction = -1;
    }
    else
    {
      this._direction = 0;
    }
  }


  private void Update()
  {
    if (_jumpQueuedDelay > 0)
    {
      _jumpQueuedDelay -= Time.deltaTime;
      if (_jumpQueuedDelay <= 0)
      {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
      }
    }
    else if (_jumpCooldown == 0)
    {
      var bounds = _boxCollider2D.bounds;
      _isGrounded = Physics2D.OverlapArea(new Vector2(bounds.min.x, bounds.min.y), new Vector2(bounds.max.x, bounds.min.y - 0.1f), _groundLayerMask);
      _timeSinceLastGrounded = _isGrounded ? _coyoteTime : Mathf.Max(_timeSinceLastGrounded - Time.deltaTime, 0);
      _animator.SetBool("isGrounded", _isGrounded);
    }
    _animator.SetBool("isMoving", _rigidbody2D.velocity.x != 0);
    _jumpCooldown = Mathf.Max(_jumpCooldown - Time.deltaTime, 0);
  }

  private void FixedUpdate()
  {
    var gravity = _rigidbody2D.velocity.y > 0 ? _gravityUp : _gravityDown;
    var gravityVelocity = Time.fixedDeltaTime * gravity * (_isDampingGravity ? _gravityDamp : 1);
    var velocityX = _direction * _moveSpeed;
    var velocityY = Mathf.Max(_rigidbody2D.velocity.y - gravityVelocity, _maxFallVelocity);
    _rigidbody2D.velocity = new Vector2(velocityX, velocityY);
  }
}
