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
  [SerializeField]
  private float _moveSpeed = 5f;
  [SerializeField]
  private float _jumpForce = 10f;
  [SerializeField]
  private float _coyoteTime = 0.5f;


  private bool _isGrounded = false;
  private float _timeSinceLastGrounded = 0f;
  private float _direction = 0f;


  private void Start()
  {
    _rigidbody2D = GetComponent<Rigidbody2D>();
    _animator = GetComponent<Animator>();

    _animator.SetBool("isGrounded", true);
    _animator.SetBool("isMoving", false);
  }

  public void OnJump()
  {
    if (_timeSinceLastGrounded > 0)
    {
      _timeSinceLastGrounded = 0;
      _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
    }
  }

  public void OnMove(InputValue value)
  {
    var direction = value.Get<float>();
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
  }

  private void FixedUpdate()
  {
    _rigidbody2D.velocity = new Vector2(_direction * _moveSpeed, _rigidbody2D.velocity.y);
  }
}
