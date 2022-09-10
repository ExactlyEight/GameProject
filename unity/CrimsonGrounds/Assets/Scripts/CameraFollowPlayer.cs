using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [Header("Camera tracking")]
    public bool isFollowing = true;
    public Transform player;
    public Transform cameraTransform;
    public Vector3 offset;
    public float cameraDistance = 10f;
    public float cameraHeight = 5f;
    public float cameraSpeed = 5f;
    public float cameraRotationSpeed = 5f;
    
    [Header("Camera Collision")]
    public bool isColliding = true;
    public float cameraCollisionRadius = 0.2f;
    public LayerMask cameraCollisionLayer;
    public float cameraCollisionOffset = 0.2f;

    
    
    void Update()
    {
        if (isFollowing)
        {
            // Move camera to player position
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, player.position + offset, cameraSpeed * Time.deltaTime);
            // Rotate camera to player rotation
            cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, player.rotation, cameraRotationSpeed * Time.deltaTime);
        }

        if (isColliding)
        {
            // check if camera is colliding with something
            if (Physics.CheckSphere(cameraTransform.position, cameraCollisionRadius, cameraCollisionLayer))
            {
               Debug.Log("Camera is colliding");
            }
        }
    }
}
