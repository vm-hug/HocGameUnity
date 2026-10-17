using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject asteroidPrefab;
    public GameObject[] starPrefabs;

    [Header("Thời gian sinh (Giây)")]
    public float asteroidSpawnRate = 2.5f;
    private float nextAsteroidTime;

    public float starSpawnRate = 2f;
    private float nextStarTime;

    [Header("Khoảng cách lề Trên/Dưới để không tràn khỏi khung hình")]
    public float paddingY = 0.8f;

    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (mainCam == null) return;

        if (Time.time >= nextAsteroidTime)
        {
            SpawnAsteroid();
            nextAsteroidTime = Time.time + asteroidSpawnRate;
        }

        if (Time.time >= nextStarTime)
        {
            SpawnStar();
            nextStarTime = Time.time + starSpawnRate;
        }
    }

    void SpawnAsteroid()
    {
        if (asteroidPrefab == null) return;

        // Tính toán 4 cạnh hiển thị của Camera tại thời điểm hiện tại
        float camDist = Mathf.Abs(mainCam.transform.position.z);
        Vector3 bottomLeft = mainCam.ViewportToWorldPoint(new Vector3(0f, 0f, camDist));
        Vector3 topRight = mainCam.ViewportToWorldPoint(new Vector3(1f, 1f, camDist));

        // Chỉ random độ cao Y trong phạm vi từ Cạnh Dưới (bottomLeft.y) đến Cạnh Trên (topRight.y)
        float randomY = Random.Range(bottomLeft.y + paddingY, topRight.y - paddingY);

        // Sinh ngay sát ngoài cạnh phải màn hình (topRight.x + 1.5f)
        Vector3 spawnPos = new Vector3(topRight.x + 1.5f, randomY, 0f);
        Instantiate(asteroidPrefab, spawnPos, asteroidPrefab.transform.rotation);
    }

    void SpawnStar()
    {
        if (starPrefabs == null || starPrefabs.Length == 0) return;

        // Tính toán 4 cạnh hiển thị của Camera
        float camDist = Mathf.Abs(mainCam.transform.position.z);
        Vector3 bottomLeft = mainCam.ViewportToWorldPoint(new Vector3(0f, 0f, camDist));
        Vector3 topRight = mainCam.ViewportToWorldPoint(new Vector3(1f, 1f, camDist));

        // Random độ cao Y nằm gọn trong khung hình
        float randomY = Random.Range(bottomLeft.y + paddingY, topRight.y - paddingY);
        int randomIndex = Random.Range(0, starPrefabs.Length);

        Vector3 spawnPos = new Vector3(topRight.x + 1.5f, randomY, 0f);
        GameObject star = Instantiate(starPrefabs[randomIndex], spawnPos, Quaternion.identity);

        // Tự xóa ngôi sao sau 8 giây (khi Camera đã bay vượt qua và sao lọt ra ngoài cạnh trái)
        Destroy(star, 8f);
    }
}