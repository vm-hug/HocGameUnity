using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Di chuyển")]
    public float forwardSpeed = 5f;
    public float verticalSpeed = 6f;

    [Header("Bắn đạn")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.2f;
    private float nextFireTime;

    void Update()
    {
        // 1. Tự động bay sang phải, điều khiển lên/xuống bằng W/S hoặc phím mũi tên
        float inputY = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(forwardSpeed, inputY * verticalSpeed, 0f);
        transform.position += movement * Time.deltaTime;

        // 2. Bắn đạn khi giữ phím Space
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, -90f));
        }
    }

    // 3. Xử lý va chạm nhặt sao
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Star"))
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(5); // Cộng đúng 5 điểm
            }
            Destroy(other.gameObject); // Xóa ngôi sao ngay khi chạm
        }
    }
}