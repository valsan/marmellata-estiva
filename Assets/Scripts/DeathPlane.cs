using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPlane : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D other)
  {
    Player player = other.GetComponent<Player>();
    if (player) SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    FallingPlatform fallingPlatform = other.GetComponent<FallingPlatform>();
    if (fallingPlatform) fallingPlatform.Reset();
  }
}
