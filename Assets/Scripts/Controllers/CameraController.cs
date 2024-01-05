using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 targetOffset;
    [SerializeField] private float cameraSpeed;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, cameraSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x + 26f, transform.rotation.y, transform.rotation.z), cameraSpeed);
    }
}