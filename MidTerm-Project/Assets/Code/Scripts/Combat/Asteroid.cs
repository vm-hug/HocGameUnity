using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int health = 3;
    [Header("Tốc độ trôi (Đặt nhỏ từ 0.5 đến 1.5 để bay chậm)")]
    public float speed = 0.5f; 

    private Transform cam;

    void Start()
    {
        if (Camera.main != null)
            cam = Camera.main.transform;
    }

    void Update()
    {
        // 1. Bay thẳng sang trái theo phương ngang
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);

        // 2. Tự động xóa khi đã trôi qua hẳn khỏi mép trái màn hình Camera (tăng lên 20f để khuất hẳn mới xóa)
        if (cam != null && transform.position.x < cam.position.x - 20f)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int dmg)
    {
        // Bảo vệ: Chỉ nhận sát thương khi thiên thạch đã bay vào trong khung hình Camera
        if (cam != null && transform.position.x > cam.position.x + 11f)
        {
            return; 
        }

        health -= dmg;
        if (health <= 0)
        {
            // Cộng 5 điểm khi bắn nổ thiên thạch bằng 3 viên đạn
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(5);
            }
            Destroy(gameObject);
        }
    }
}