using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] float cameraSpeed;
    [SerializeField] private Vector3 offset;

    private Vector3 targetPosition;

    void Start()
    {
        transform.position = player.position + offset;
    }

    public void Move()
    {
        float moveVertical = Input.GetAxis("Vertical");
        targetPosition = player.position + offset;

        if (moveVertical != 0)
        {
            float moveDirection = moveVertical > 0 ? 1 : -1;
            targetPosition += transform.forward * cameraSpeed * moveDirection * Time.deltaTime;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraSpeed);
    }
}