using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int CurrentHealth { get; private set; }
    private Animator animator;
    [SerializeField] private int maxHealth = 100;

    // Start is called before the first frame update
    void Awake()
    {
        CurrentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log("Take damage");
        animator.SetTrigger("isHurt");
        if (CurrentHealth <= 0)
            Die();
    }
    void Die()
    {
        Debug.Log("Dead");
        animator.SetBool("isDead", true);
        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 3f);
    }
}