using UnityEngine;

public class CameraController : MonoBehaviour
{ 
    [SerializeField] private Transform target;
    [SerializeField] private Transform firstScene;
    [SerializeField] private Vector3 targetOffset;
    [SerializeField] private float cameraSpeed;
    public bool isFirstScene;

   private void Start()
   {
        isFirstScene = false;        
   }

   private void LateUpdate()
   {     if (isFirstScene)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, cameraSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(transform.rotation.x+26f, transform.rotation.y, transform.rotation.z), cameraSpeed);
        }
         else 
        {
            transform.position = Vector3.Lerp(transform.position, firstScene.position, cameraSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, firstScene.rotation, cameraSpeed);
        }      
   }
}