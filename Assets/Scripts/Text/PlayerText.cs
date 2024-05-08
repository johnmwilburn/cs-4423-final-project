using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class PlayerText : MonoBehaviour
{
    [SerializeField] private PlayerSO playerSO;
    private TextMeshProUGUI healthText;

    public void Start(){
        healthText = GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {
        healthText.text = String.Format("Health: {0} / {1}\nAmmo: {2} / {3}", playerSO.health, playerSO.maxHealth, playerSO.ammo, playerSO.maxAmmo);
    }
}
