using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rd;
    void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
    }
    public void MovePlayer(Vector2 movementVector, float movementSpeed)
    {
        movementVector.Normalize();
        transform.Translate(movementVector * Time.deltaTime * movementSpeed);
    }
    public void PlayerJump(float jumpSpeed)
    {
        rd.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }
}
