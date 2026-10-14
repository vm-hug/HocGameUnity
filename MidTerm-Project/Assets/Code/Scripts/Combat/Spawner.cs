using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject asteroidPrefab;
    public GameObject[] starPrefabs;

    [Header("Thời gian sinh")]
    public float asteroidSpawnRate = 2.5f;
    private float nextAsteroidTime;

    public float starSpawnRate = 2f;
    private float nextStarTime;

    [Header("Phạm vi xuất hiện")]
    public float spawnOffsetX = 14f;
    public float minY = -4f;
    public float maxY = 4f;

    private Transform cam;

    void Start()
    {
        if (Camera.main != null)
            cam = Camera.main.transform;
    }

    void Update()
    {
        if (cam == null) return;

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
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(cam.position.x + spawnOffsetX, randomY, 0f);
        
        // Dùng asteroidPrefab.transform.rotation để giữ nguyên hướng nằm ngang đã chỉnh
        Instantiate(asteroidPrefab, spawnPos, asteroidPrefab.transform.rotation);
    }

    void SpawnStar()
    {
        if (starPrefabs == null || starPrefabs.Length == 0) return;
        float randomY = Random.Range(minY, maxY);
        int randomIndex = Random.Range(0, starPrefabs.Length);
        Vector3 spawnPos = new Vector3(cam.position.x + spawnOffsetX, randomY, 0f);

        GameObject star = Instantiate(starPrefabs[randomIndex], spawnPos, Quaternion.identity);
        
        // Tự động xóa ngôi sao sau 7 giây (khi đã trôi qua khỏi mép trái màn hình)
        Destroy(star, 7f);
    }
}