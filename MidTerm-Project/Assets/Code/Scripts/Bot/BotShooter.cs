using UnityEngine;

public class BotShooter : MonoBehaviour
{
    [Header("Cấu hình Đạn & Điểm bắn")]
    public GameObject bulletPrefab;       // Kéo Prefab viên đạn vào đây
    public Transform firePointCenter;     // Kéo FirePoint_Center vào đây
    public Transform firePointLeft;       // Kéo FirePoint_Left vào đây
    public Transform firePointRight;      // Kéo FirePoint_Right vào đây

    [Header("Thời gian bắn")]
    public float fireRate = 1.5f;         // Bắn mỗi 1.5 giây
    private float nextFireTime = 0f;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            ShootAll();
            nextFireTime = Time.time + fireRate;
        }
    }

    public void ShootAll()
    {
        if (bulletPrefab == null) return;

        if (firePointCenter != null)
            Instantiate(bulletPrefab, firePointCenter.position, firePointCenter.rotation);

        if (firePointLeft != null)
            Instantiate(bulletPrefab, firePointLeft.position, firePointLeft.rotation);

        if (firePointRight != null)
            Instantiate(bulletPrefab, firePointRight.position, firePointRight.rotation);
    }
}