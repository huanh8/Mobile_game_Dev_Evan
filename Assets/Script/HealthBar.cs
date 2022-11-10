using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Image HeadIcon;
    public Animator animator;
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
        if (animator != null)
            FillAnimation();
    }
    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    public void DisableAnimator()
    {
        animator.enabled = false;
    }
    private void FillAnimation()
    {
        animator.enabled = true;
    }
    public void SetHeadIcon(Sprite sprite)
    {
        HeadIcon.sprite = sprite;
        Debug.Log("set head icon");
        Debug.Log(sprite.name);
    }

}
