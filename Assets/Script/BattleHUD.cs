using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
public TextMeshProUGUI  nameText;
public TextMeshProUGUI  levelText;
public Slider healthSlider;

public Slider countdownBar;
public float countdownBaseMax = 5f;
public float countdownSpeed = 1f;
public float countdownReset = 0f;

    public void SetHud(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.level;
        healthSlider.maxValue = unit.maxHP;
        healthSlider.value = unit.currentHP;

    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }
}
