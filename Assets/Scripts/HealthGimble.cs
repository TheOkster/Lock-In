using Unity.Mathematics;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    void LateUpdate()
    {
        // face camera
        Transform cameraTransform = Camera.main.transform;
        Vector3 directionToCamera = cameraTransform.position - transform.position;
        directionToCamera.y = 0; // keep only horizontal rotation
        if (directionToCamera.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);
            transform.rotation = targetRotation;
        }
    }
}
