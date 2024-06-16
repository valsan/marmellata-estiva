using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayCanvas : MonoBehaviour
{
   [field: SerializeField] public TextMeshProUGUI LevelNameText { get; private set; }
   [field: SerializeField] public DebuffPopup DebuffPopup { get; private set; }
   [field: SerializeField] public GameOverScreen GameOverScreen { get; private set; }
}
