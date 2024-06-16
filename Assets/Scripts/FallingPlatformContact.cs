using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformContact : MonoBehaviour
{
  [SerializeField] FallingPlatform _fallingPlatform;

  private void OnTriggerEnter2D(Collider2D other)
  {
    Player player = other.GetComponent<Player>();
    if (player)
    {
      player.transform.SetParent(transform);
      _fallingPlatform.Shake();
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    Player player = other.GetComponent<Player>();
    if (player) player.transform.parent = null;
  }
}
