using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chest : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D boxCollider;
    public UnityEvent OnOpen;
    private Rigidbody2D rb;
    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }


    // collider2D other
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player got chest");
            anim.SetBool("OpenIt", true);
            boxCollider.enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
            other.gameObject.GetComponent<Animator>().SetTrigger("IsWin");
            OnOpen.Invoke();
        }
    }

}
