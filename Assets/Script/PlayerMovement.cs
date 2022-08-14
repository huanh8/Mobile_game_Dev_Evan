using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D headCollider;
    private bool IsDashing;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        headCollider = GetComponent<CapsuleCollider2D>();
    }
    public void MovePlayer(Vector2 movementVector, float movementSpeed)
    {
        if (!IsDashing)
            rb.velocity = new Vector2(movementVector.x * movementSpeed, rb.velocity.y);
    }
    public void PlayerJump(float jumpSpeed)
    {
        if (!IsDashing)
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }

    public IEnumerator DashPlayer(Vector2 movementVector, float dashSpeed, float dashTime)
    {
        IsDashing = true;
        headCollider.enabled = false;
        rb.AddForce(movementVector * dashSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashTime);
        IsDashing = false;
        headCollider.enabled = true;
    }
}
