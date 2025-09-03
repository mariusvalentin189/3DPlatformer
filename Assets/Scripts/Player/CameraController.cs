using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;  // The player to follow
    [SerializeField] float distance = 5.0f;  // Default distance behind the player
    [SerializeField] float height = 2.0f;  // Default height above the player
    [SerializeField] float rotationSpeed = 5.0f;  // Speed at which the camera rotates
    [SerializeField] float zoomSpeed = 2.0f;  // Speed at which the camera zooms in and out
    [SerializeField] float minHeight = 1.0f;  // Minimum height (camera up/down bound)
    [SerializeField] float maxHeight = 4.0f;  // Maximum height (camera up/down bound)
    [SerializeField] float minZoom = 2.0f;  // Minimum zoom (distance to the player)
    [SerializeField] float maxZoom = 10.0f;  // Maximum zoom (distance to the player)

    [SerializeField] LayerMask collisionLayers;

    float currentAngleX = 0f;  // Vertical angle
    float currentAngleY = 0f;  // Horizontal angle
    void LateUpdate()
    {
        if (player == null || PauseMenu.instance.isPaused)
            return;

        // Handle horizontal rotation
        currentAngleY += Input.GetAxis("Mouse X") * rotationSpeed;

        // Handle vertical rotation
        currentAngleX -= Input.GetAxis("Mouse Y") * rotationSpeed;
        currentAngleX = Mathf.Clamp(currentAngleX, -40f, 85f);  // Clamp the vertical rotation to avoid flipping

        // Handle zoom with mouse scroll
        float zoomInput = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        distance = Mathf.Clamp(distance - zoomInput, minZoom, maxZoom);

        // Convert the camera's direction from spherical to Cartesian (X, Y, Z) space
        float horizontal = Mathf.Cos(currentAngleX * Mathf.Deg2Rad) * distance;
        float vertical = Mathf.Sin(currentAngleX * Mathf.Deg2Rad) * distance;

        // Set the camera position relative to the player with an adjustable height
        Vector3 targetPosition = player.position - new Vector3(horizontal * Mathf.Sin(currentAngleY * Mathf.Deg2Rad),
                                                                 -vertical,
                                                                 horizontal * Mathf.Cos(currentAngleY * Mathf.Deg2Rad));

        // Apply the minHeight/maxHeight clamp on the vertical position
        targetPosition.y = Mathf.Clamp(targetPosition.y, minHeight + player.position.y, maxHeight + player.position.y);

        // Camera collision check
        RaycastHit hit;
        if (Physics.Raycast(player.position, targetPosition - player.position, out hit, distance, collisionLayers))
        {
            targetPosition = hit.point;  // Stop the camera at the point of collision
        }

        transform.position = targetPosition;

        // Look at the player while following
        transform.LookAt(player);
    }

    public void SetSensitivity(float value)
    {
        rotationSpeed = value;
    }
}
