using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Component References")]
    public Rigidbody2D playerRigidbody;
    public Collider2D playerCollider;

    private float movementSpeed = 5f;

    [SerializeField]
    private PlayerStats stats;

    private Vector3 movementDirection;

    void Start()
    {
        movementSpeed = stats.MovementSpeed;
    }
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
        
        var camera = CameraManager.Instance.followPlayerCamera;
        float cameraHeight = 2f * camera.m_Lens.OrthographicSize;
        float cameraWidth = cameraHeight * camera.m_Lens.Aspect;

        var cameraMinBound = (Vector2)camera.transform.position - new Vector2(cameraWidth, cameraHeight) * 0.5f;
        var cameraMaxBound = (Vector2)camera.transform.position + new Vector2(cameraWidth, cameraHeight) * 0.5f;

        cameraMinBound += new Vector2(0.5f, 0.8f);

        var isOnRightSide = transform.position.x + playerCollider.bounds.extents.x > cameraMaxBound.x;
        var isOnLeftSide = transform.position.x + playerCollider.bounds.extents.x < cameraMinBound.x;
        if (myVelocity.x > 0 && isOnRightSide)
        {
            myVelocity.x = Vector2.zero.x;
        }
        if (myVelocity.x < 0 && isOnLeftSide)
        {
            myVelocity.x = Vector2.zero.x;
        }

        var isOnTopSide = transform.position.y + playerCollider.bounds.extents.y > cameraMaxBound.y;
        var isOnBottomSide = transform.position.y + playerCollider.bounds.extents.y < cameraMinBound.y;
        if (myVelocity.y > 0 && isOnTopSide)
        {
            myVelocity.y = Vector2.zero.y;
        }
        if (myVelocity.y < 0 && isOnBottomSide)
        {
            myVelocity.y = Vector2.zero.y;
        }

        playerRigidbody.velocity = myVelocity;
    }
}
