using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _lifetime = 5.0f;

    
    private Vector3 _direction = Vector3.up;
    private float _spawnTime;

    private void Start()
    {
        _spawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time > _spawnTime + _lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.position += _direction * (_speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            // Handle Player
        }
    }
}
