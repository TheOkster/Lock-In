using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    void Update()
    {
        Vector3 euler = transform.rotation.eulerAngles;
        euler.z = 0; 
        transform.rotation = Quaternion.Euler(euler);
    }
}
