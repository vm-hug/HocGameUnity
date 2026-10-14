using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float offsetX = 4f;

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x + offsetX, transform.position.y, -10f);
        }
    }
}