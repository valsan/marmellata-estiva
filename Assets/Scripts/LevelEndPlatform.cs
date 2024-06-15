using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelEndPlatform : MonoBehaviour
{
    public Action OnLevelEnded;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            OnLevelEnded?.Invoke();
        }
    }
}
