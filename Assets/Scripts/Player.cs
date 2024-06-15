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

  [Header("Customization")]
  [SerializeField]
  private float _moveSpeed = 5f;
  [SerializeField]
  private float _jumpForce = 10f;

  private float _moveDirection = 0f;
  public bool IsGrounded { get; private set; } = false;

  private void Reset()
  {
    _rigidbody2D = GetComponent<Rigidbody2D>();
  }

  public void OnJump()
  {
    if (IsGrounded)
    {
      _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
    }
  }

  public void OnMove(InputValue value)
  {
    var direction = value.Get<float>();
    if (direction > 0)
    {
      _moveDirection = 1;
      Quaternion localRotation = transform.localRotation;
      localRotation.y = 0;
      transform.localRotation = localRotation;
    }
    else if (direction < 0)
    {
      _moveDirection = -1;
      Quaternion localRotation = transform.localRotation;
      localRotation.y = 180;
      transform.localRotation = localRotation;
    }
    else
    {
      _moveDirection = 0;
    }
  }


  private void Update()
  {
    float radius = 0.1f;
    IsGrounded = Physics2D.OverlapCircle(_feet.position, radius);
  }

  private void FixedUpdate()
  {
    _rigidbody2D.velocity = new Vector2(_moveDirection * _moveSpeed, _rigidbody2D.velocity.y);
  }
}
