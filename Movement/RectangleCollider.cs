using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleCollider : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayer;

    private BoxCollider2D boxCollider2d;
    private float extraWidth = .3f;

    private void Awake()
    {
        boxCollider2d = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        GetComponent<ICollider>().SetBoolIsGrounded(IsGrounded());
        GetComponent<ICollider>().SetBoolOnRightWall(OnRightWall());
        GetComponent<ICollider>().SetBoolOnLeftWall(OnLeftWall());
    }
    private bool OnRightWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center + new Vector3(0, (boxCollider2d.bounds.extents.y) / 4), boxCollider2d.bounds.size, 0f, Vector2.right, extraWidth, platformLayer);
        Color rayColor;
        if (raycastHit.collider != null) rayColor = Color.yellow;
        else rayColor = Color.red;
        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(0, boxCollider2d.bounds.extents.y), Vector2.right * (boxCollider2d.bounds.extents.x + extraWidth), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(0, (boxCollider2d.bounds.extents.y) / 2), Vector2.right * (boxCollider2d.bounds.extents.x + extraWidth), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3((boxCollider2d.bounds.extents.x) + extraWidth, boxCollider2d.bounds.extents.y), Vector2.down * ((3 * boxCollider2d.bounds.extents.y) / 2), rayColor);

        return raycastHit.collider != null;
    }
    private bool OnLeftWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center + new Vector3(0, (boxCollider2d.bounds.extents.y) / 4), boxCollider2d.bounds.size, 0f, Vector2.left, extraWidth, platformLayer);
        Color rayColor;
        if (raycastHit.collider != null) rayColor = Color.cyan;
        else rayColor = Color.red;
        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(0, boxCollider2d.bounds.extents.y), Vector2.left * (boxCollider2d.bounds.extents.x + extraWidth), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(0, (boxCollider2d.bounds.extents.y) / 2), Vector2.left * (boxCollider2d.bounds.extents.x + extraWidth), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3((-boxCollider2d.bounds.extents.x) - extraWidth, boxCollider2d.bounds.extents.y), Vector2.down * ((3 * boxCollider2d.bounds.extents.y) / 2), rayColor);

        return raycastHit.collider != null;
    }
    private bool IsGrounded()
    {
        float extraHeightText = .2f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHeightText, platformLayer);
        Color rayColor;
        if (raycastHit.collider != null) rayColor = Color.green;
        else rayColor = Color.red;

        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, boxCollider2d.bounds.extents.y + extraHeightText), Vector2.right * (boxCollider2d.bounds.extents.x * 2f), rayColor);
        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }
}
