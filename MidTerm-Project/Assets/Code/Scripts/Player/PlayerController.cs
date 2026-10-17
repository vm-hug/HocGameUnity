using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Tốc độ điều khiển 4 hướng (WASD / Mũi tên)")]
    public float moveSpeed = 7f;

    [Header("Khoảng cách lề 4 cạnh màn hình")]
    public float paddingX = 0.8f; // Lề Trái / Phải
    public float paddingY = 0.6f; // Lề Trên / Dưới

    [Header("Bắn đạn")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.2f;
    private float nextFireTime;

    private Camera mainCam;
    private CameraFollow camFollow;

    void Start()
    {
        mainCam = Camera.main;
        if (mainCam != null)
        {
            camFollow = mainCam.GetComponent<CameraFollow>();
        }
    }

    void Update()
    {
        MoveAndClampPlayer();
        HandleShooting();
    }

    void MoveAndClampPlayer()
    {
        // 1. Lấy vận tốc tiến về phía trước của Camera để tàu luôn đồng hành cùng Camera
        float baseForwardSpeed = (camFollow != null) ? camFollow.forwardSpeed : 4f;
        Vector3 autoForward = new Vector3(baseForwardSpeed * Time.deltaTime, 0f, 0f);

        // 2. Nhận điều khiển 4 hướng từ bàn phím (W, A, S, D hoặc 4 phím Mũi tên)
        float inputX = Input.GetAxisRaw("Horizontal"); // Trái (-1) / Phải (+1)
        float inputY = Input.GetAxisRaw("Vertical");   // Dưới (-1) / Trên (+1)

        Vector3 controlMove = new Vector3(inputX, inputY, 0f).normalized * moveSpeed * Time.deltaTime;

        // Cộng vị trí mới
        Vector3 newPos = transform.position + autoForward + controlMove;

        // 3. Tính toán 4 cạnh hiển thị của Camera và khóa không cho máy bay bay ra ngoài
        if (mainCam != null)
        {
            // Lấy tọa độ góc dưới-trái (0,0) và góc trên-phải (1,1) của khung hình Camera hiện tại
            float camDistance = Mathf.Abs(mainCam.transform.position.z - transform.position.z);
            Vector3 bottomLeft = mainCam.ViewportToWorldPoint(new Vector3(0f, 0f, camDistance));
            Vector3 topRight = mainCam.ViewportToWorldPoint(new Vector3(1f, 1f, camDistance));

            // Giới hạn tọa độ X giữa Cạnh Trái và Cạnh Phải
            newPos.x = Mathf.Clamp(newPos.x, bottomLeft.x + paddingX, topRight.x - paddingX);

            // Giới hạn tọa độ Y giữa Cạnh Dưới và Cạnh Trên
            newPos.y = Mathf.Clamp(newPos.y, bottomLeft.y + paddingY, topRight.y - paddingY);
        }

        transform.position = newPos;
    }

    void HandleShooting()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            if (bulletPrefab != null && firePoint != null)
            {
                Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, -90f));
            }
            nextFireTime = Time.time + fireRate;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Chạm Ngôi sao -> Cộng 5 điểm & Xóa sao
        if (other.CompareTag("Star"))
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(5);
            }
            Destroy(other.gameObject);
        }

        // 2. Chạm Thiên thạch -> Mất 1 trái tim & Nổ thiên thạch
        Asteroid asteroid = other.GetComponentInParent<Asteroid>();
        if (other.CompareTag("Asteroid") || asteroid != null)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoseLife();
            }

            if (asteroid != null)
                Destroy(asteroid.gameObject);
            else
                Destroy(other.gameObject);
        }
    }
}