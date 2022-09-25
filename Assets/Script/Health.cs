using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int CurrentHealth { get; private set; }
    public bool BlockingHealth { get; private set; }
    private Animator animator;
    [SerializeField] private int maxHealth = 100;
    public UnityEvent<GameObject> OnHitEvent, OnDieEvent;
    public bool IsDead { get; private set; }
    [SerializeField] private float destroyDelayTime = 3f;
    // Start is called before the first frame update
    void Awake()
    {
        IsDead = false;
        CurrentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage, GameObject sender)
    {
        if (IsDead) return;
        if (BlockingHealth) return;
        if (sender.layer == gameObject.layer) return; // same layer will not hit

        CurrentHealth -= damage;
        // trigger hit animation or other events
        OnHitEvent.Invoke(sender);
        if (CurrentHealth <= 0) Die(sender);
    }
    void Die(GameObject sender)
    {
        // trigger die animation or other events
        OnDieEvent.Invoke(sender);
        GetComponent<BoxCollider2D>().enabled = false;
        IsDead = true;
        Destroy(gameObject, destroyDelayTime);
    }
    public void SetBlock(bool isBlocking)
    {
        BlockingHealth = isBlocking;
    }

}
