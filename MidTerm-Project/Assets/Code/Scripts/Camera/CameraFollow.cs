using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float forwardSpeed = 4f;

    void LateUpdate()
    {
        if (target == null) return;

        transform.position += new Vector3(forwardSpeed * Time.deltaTime, 0f, 0f);
    }
}