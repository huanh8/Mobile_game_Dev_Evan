using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D headCollider;
    public bool IsDashing { get; private set; }
    [Range(0, 20)][SerializeField] private float movementSpeed = 5f;
    [Range(0, 15)][SerializeField] private float jumpSpeed = 8f;
    [Range(0, 20)][SerializeField] private float dashSpeed = 6f;
    [Range(0, 1)][SerializeField] private float dashingTime = 0.5f;
    float nextDash = 0;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        headCollider = GetComponent<CapsuleCollider2D>();
    }
    public void MovePlayer(Vector2 movementVector)
    {
        if (!IsDashing)
            rb.velocity = new Vector2(movementVector.x * movementSpeed, rb.velocity.y);
    }
    public void PlayerJump()
    {
        if (!IsDashing)
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }

    public void PlayerCanDash(Vector2 movementVector)
    {
        if (Time.time >= nextDash)
        {
            StartCoroutine(PlayerDashing(movementVector));
            nextDash = Time.time + dashingTime;
        }
    }
    public IEnumerator PlayerDashing(Vector2 movementVector)
    {
        IsDashing = true;
        headCollider.enabled = false;
        rb.AddForce(movementVector * dashSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashingTime);
        IsDashing = false;
        headCollider.enabled = true;
    }
}
