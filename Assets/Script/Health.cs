using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public HealthBar healthBar;
    public int CurrentHealth { get; private set; }
    public bool BlockingHealth { get; private set; }
    [SerializeField] private int maxHealth = 100;
    public UnityEvent<GameObject> OnHitEvent, OnDieEvent, OnBlockEvent; 
    //public UnityEvent OnHitEvent, OnDieEvent;
    public bool IsDead { get; private set; }
    [SerializeField] private float destroyDelayTime = 3f;
    // Start is called before the first frame update
    void Awake()
    {
        IsDead = false;
        CurrentHealth = maxHealth;
        if(healthBar != null)
            healthBar.SetMaxHealth(maxHealth);
    }

    // pass bool isTouching false by default
    public void TakeDamage(int damage, GameObject sender, bool isTouching = false)
    {
        if (IsDead) return;
        if (BlockingHealth){
            OnBlockEvent.Invoke(sender);
            return;
        } 
        if (isTouching) OnBlockEvent.Invoke(sender);;
        // same layer will not hit
        if (sender.layer == gameObject.layer) { 
            Debug.Log("same layer"); 
            return; 
            } 

        CurrentHealth -= damage;
        if (healthBar != null)
            healthBar.SetHealth(CurrentHealth);

        // trigger animation or other events
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
