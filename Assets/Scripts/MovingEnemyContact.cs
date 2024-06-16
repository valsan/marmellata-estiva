using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyContact : MonoBehaviour
{
  [SerializeField] private FloatValue _charge;
  [SerializeField] private FloatValue _damage;
  [SerializeField] private bool _killEnemyOnTouch = false;

  private void OnTriggerEnter2D(Collider2D other)
  {
    Player player = other.GetComponent<Player>();
    if (player)
    {
      _charge.Value -= _damage.Value;
      player.Damage();
      if (_killEnemyOnTouch) transform.parent.gameObject.SetActive(false);
    }
  }
}
