using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float cameraMoveSpeed = 2f;
    public float cameraMoveThreshold = 0.2f;
    public float cameraFollowDistance = 1f;

    private Vector3 initialOffset;
    private Vector3 targetOffset;

    private void Start()
    {
        initialOffset = transform.position - player.position;
        targetOffset = initialOffset;
    }

    private void LateUpdate()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = transform.position.z - player.position.z;
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > cameraFollowDistance)
        {
            if (Mathf.Abs(targetPosition.x - player.position.x) > cameraMoveThreshold)
            {
                targetOffset.x = targetPosition.x - player.position.x;
            }
            else
            {
                targetOffset.x = initialOffset.x;
            }

            if (Mathf.Abs(targetPosition.y - player.position.y) > cameraMoveThreshold)
            {
                targetOffset.y = targetPosition.y - player.position.y;
            }
            else
            {
                targetOffset.y = initialOffset.y;
            }
        }
        else
        {
            targetOffset = initialOffset;
        }

        Vector3 newPosition = player.position + targetOffset;
        newPosition.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, newPosition, cameraMoveSpeed * Time.deltaTime);

        // Limit camera's vertical position to keep the player in view
        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        float minX = player.position.x - cameraWidth;
        float maxX = player.position.x + cameraWidth;
        float minY = player.position.y - cameraHeight;
        float maxY = player.position.y + cameraHeight;

        float clampedX = Mathf.Clamp(transform.position.x, minX + 5f, maxX - 5f);
        float clampedY = Mathf.Clamp(transform.position.y, minY + 5f, maxY - 5f);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}