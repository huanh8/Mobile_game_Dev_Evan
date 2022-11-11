using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask pathLayer;
    [SerializeField] private float extraHeightTest = 0.1f;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    public bool IsGrounded()
    {
        var bounds = boxCollider.bounds;
        var center = boxCollider.bounds.center;
        var extents = boxCollider.bounds.extents;
        RaycastHit2D raycastHit = Physics2D.BoxCast(center, bounds.size, 0f, Vector2.down, extraHeightTest, pathLayer);
        Color raycolor = raycastHit ? Color.red : Color.green;

        //right side of box collider
        Debug.DrawRay(center + new Vector3(extents.x, 0), Vector2.down * (extents.y + extraHeightTest), raycolor);
        //left side of box collider
        Debug.DrawRay(center - new Vector3(extents.x, 0), Vector2.down * (extents.y + extraHeightTest), raycolor);
        //down side of box collider
        Debug.DrawRay(center - new Vector3(extents.x, extents.y + extraHeightTest), Vector2.right * (extents.x + extraHeightTest), raycolor);
        return raycastHit;
    }
}
