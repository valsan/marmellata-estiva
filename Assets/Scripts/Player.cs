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
  private Transform _feet;
  private Animator _animator;

  [Header("Customization")]
  [SerializeField]
  private LayerMask _groundLayerMask;
  public float _moveSpeed = 5f;
  [SerializeField]
  private float _jumpForce = 10f;
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
  private float _direction = 0f;
  private float _jumpCooldown = 0f;
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
      _isDampingGravity = true;
      _timeSinceLastGrounded = 0;
      _jumpCooldown = _coyoteTime + 0.01f;
      _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
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
    float radius = 0.1f;
    _isGrounded = Physics2D.OverlapCircle(_feet.position, radius, _groundLayerMask);
    _timeSinceLastGrounded = _isGrounded ? _coyoteTime : Mathf.Max(_timeSinceLastGrounded - Time.deltaTime, 0);
    _animator.SetBool("isGrounded", _isGrounded);
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
