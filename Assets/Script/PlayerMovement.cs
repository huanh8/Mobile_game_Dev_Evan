using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rd;
    public bool IsCrouching { get; private set; }
    void Awake()
    {
        IsCrouching = false;
        rd = GetComponent<Rigidbody2D>();
    }
    public void MovePlayer(Vector2 movementVector, float movementSpeed)
    {
        movementVector.Normalize();
        if (!IsCrouching)
            rd.velocity = new Vector2(movementVector.x * movementSpeed, rd.velocity.y);
        //transform.Translate(movementVector * Time.deltaTime * movementSpeed);
        else rd.velocity = new Vector2(0, rd.velocity.y);
    }
    public void PlayerJump(float jumpSpeed)
    {
        rd.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }
    public void SetCrouch(bool crouching)
    {
        IsCrouching = crouching;
    }


}
