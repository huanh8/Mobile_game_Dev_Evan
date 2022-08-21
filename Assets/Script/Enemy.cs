using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int CurrentHealth { get; private set; }
    private Animator animator;
    [SerializeField] private int maxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log("Enemy take damage");
        animator.SetTrigger("isHurt");
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Enemy die");
        animator.SetBool("isDead", true);
        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
    }
}
