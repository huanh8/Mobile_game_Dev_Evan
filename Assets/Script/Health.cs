using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int CurrentHealth { get; private set; }
    public bool BlockingHealth { get; private set; }
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
        if (BlockingHealth) return;
        CurrentHealth -= damage;
        animator.SetTrigger("isHurt");
        if (CurrentHealth <= 0)
            Die();
    }
    void Die()
    {
        animator.SetBool("isDead", true);
        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 3f);
    }
    public void SetBlock(bool isBlocking)
    {
        BlockingHealth = isBlocking;
    }

}
