using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Component References")]
    public Rigidbody2D playerRigidbody;
    public Collider2D playerCollider;

    [Header("Movement Settings")]
    public float movementSpeed = 5f;

    private Vector3 movementDirection;

    public void UpdateMovementData(Vector3 newMovementDirection)
    {
        movementDirection = newMovementDirection;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        var myVelocity = movementDirection * movementSpeed * Time.fixedDeltaTime;
        //var cameraBoundsCollider = CameraManager.Instance.Confiner2D.m_BoundingShape2D;

        
        var camera = CameraManager.Instance.followPlayerCamera;
        float cameraHeight = 2f * camera.m_Lens.OrthographicSize;
        float cameraWidth = cameraHeight * camera.m_Lens.Aspect;

        var cameraMinBound = (Vector2)camera.transform.position - new Vector2(cameraWidth, cameraHeight) * 0.5f;
        var cameraMaxBound = (Vector2)camera.transform.position + new Vector2(cameraWidth, cameraHeight) * 0.5f;

        Vector2 buffer = Vector2.one;
        cameraMinBound += new Vector2(0.5f, 0.8f);
        //cameraMaxBound -= new Vector2();


        //Vector3 clampedPosition = transform.position;
        //clampedPosition.x = Mathf.Clamp(clampedPosition.x, cameraMinBound.x + playerCollider.bounds.extents.x, cameraMaxBound.x - playerCollider.bounds.extents.x);
        //clampedPosition.y = Mathf.Clamp(clampedPosition.y, cameraMinBound.y + playerCollider.bounds.extents.y, cameraMaxBound.y - playerCollider.bounds.extents.y);

        var isOnRightSide = transform.position.x + playerCollider.bounds.extents.x > cameraMaxBound.x;
        var isOnLeftSide = transform.position.x + playerCollider.bounds.extents.x < cameraMinBound.x;

        var isOnTopSide = transform.position.y + playerCollider.bounds.extents.y > cameraMaxBound.y;
        var isOnBottomSide = transform.position.y + playerCollider.bounds.extents.y < cameraMinBound.y;
        if (myVelocity.x > 0 && isOnRightSide)
        {
            myVelocity.x = Vector2.zero.x;
        }
        if (myVelocity.x < 0 && isOnLeftSide)
        {
            myVelocity.x = Vector2.zero.x;
        }

        if (myVelocity.y > 0 && isOnTopSide)
        {
            myVelocity.y = Vector2.zero.y;
        }
        if (myVelocity.y < 0 && isOnBottomSide)
        {
            myVelocity.y = Vector2.zero.y;
        }
        //else if (transform.position.y < cameraBounds.minY + (height / 2) && newVelocity.y < 0)
        //{
        //    myVelocity = Vector2.zero;
        //}
        //transform.position = clampedPosition;

        playerRigidbody.velocity = myVelocity;
    }
}
