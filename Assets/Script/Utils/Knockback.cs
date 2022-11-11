using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float knockbackForce = 5f, delay = 0.2f;
    public UnityEvent OnBegin, OnDone;
    public void PlayKnockback(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        //rb.AddForce(new Vector2(direction.x * knockbackForce, 0), ForceMode2D.Impulse);
        StartCoroutine(ResetKnockback());
    }
    private IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(delay);
        OnDone.Invoke();
    }

}
