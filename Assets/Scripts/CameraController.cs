using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;  // The player position

    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, target.position.z); 
    }
}
