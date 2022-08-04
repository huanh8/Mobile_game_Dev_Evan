using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour
{
    private Animator animator;
    private float horizontal;
    private bool isGrounded;
    private Rigidbody2D rd;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask pathLayer;
    [Range(0, 15)][SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float extraHeightTest = 0.1f;
    void Awake()
    {
        animator = GetComponent<Animator>();
        rd = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        Move();
    }
    void FixedUpdate()
    {
        Jump();
    }
    private void Move()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        if (horizontal != 0)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0) * Time.deltaTime * 10);

            if (horizontal > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void Jump()
    {
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            animator.SetBool("IsJumping", true);
            rd.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }

    }
    //check isgrounded
    private bool IsGrounded()
    {
        var bounds = boxCollider.bounds;
        var center = boxCollider.bounds.center;
        var extents = boxCollider.bounds.extents;
        RaycastHit2D raycastHit = Physics2D.BoxCast(center, bounds.size, 0f, Vector2.down, extraHeightTest, pathLayer);
        Color raycolor = raycastHit ? Color.red : Color.green;

        if (raycastHit.collider != null)
        {   
            animator.SetBool("IsJumping", false);
        }

        //right side of box collider
        Debug.DrawRay(center + new Vector3(extents.x, 0), Vector2.down * (extents.y + extraHeightTest), raycolor);
        //left side of box collider
        Debug.DrawRay(center - new Vector3(extents.x, 0), Vector2.down * (extents.y + extraHeightTest), raycolor);
        //down side of box collider
        Debug.DrawRay(center - new Vector3(extents.x, extents.y + extraHeightTest), Vector2.right * (extents.x + extraHeightTest), raycolor);
        return raycastHit.collider != null;
    }
}
