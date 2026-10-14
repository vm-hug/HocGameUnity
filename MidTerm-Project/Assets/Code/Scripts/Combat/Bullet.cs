using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 14f;
    private Transform cam;

    void Start()
    {
        if (Camera.main != null)
            cam = Camera.main.transform;
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);

        // Hủy đạn ngay khi bay tới mép phải màn hình Camera (tránh bắn trúng thiên thạch chưa xuất hiện)
        if (cam != null && transform.position.x > cam.position.x + 10f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Chạm thiên thạch (cả object cha hoặc object con)
        Asteroid asteroid = collision.GetComponentInParent<Asteroid>();
        if (asteroid != null || collision.CompareTag("Asteroid"))
        {
            if (asteroid != null)
            {
                asteroid.TakeDamage(1);
            }
            Destroy(gameObject); // Xóa viên đạn ngay khi trúng thiên thạch
        }
    }
}