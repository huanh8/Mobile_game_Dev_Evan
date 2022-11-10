using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindHealtheBar : MonoBehaviour
{
    Health health;
    public HealthBar healthBar;
    public Image HeadIcon;
    void OnEnable()
    {

        health = GetComponent<Health>();
        healthBar.SetMaxHealth(health.maxHealth);
        if (health != null)
        {
            health.healthBar = healthBar;
        }
        if (HeadIcon != null)
        {
            healthBar.SetHeadIcon(HeadIcon.sprite);
        }
    }

}
