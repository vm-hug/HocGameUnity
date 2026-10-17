using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Hệ thống 3 Trái Tim")]
    public GameObject[] hearts; // Kéo 3 object hình trái tim vào đây
    private int currentLives = 3;

    [Header("Giao diện Nút Pause (Đổi 2 Icon)")]
    public Image pauseButtonImage; // Kéo PauseButton vào đây
    public Sprite pauseChaySprite; // Kéo ảnh pause_chay (||) vào đây
    public Sprite pauseDungSprite;

    [Header("Các bảng UI Panel")]
    public GameObject pausePanel;
    public GameObject gameOverPanel; // Bảng hiện Replay / Menu khi hết 3 tim

    private bool isPaused = false;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f; // Đảm bảo game chạy bình thường khi bắt đầu hoặc Replay
    }

    void Start()
    {
        currentLives = hearts.Length;
        UpdateHeartsUI();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);

        // Mặc định khi đang chơi hiển thị icon pause_chay (||)
        UpdatePauseIcon();
    }

    // Hàm gọi khi máy bay đâm trúng thiên thạch
    public void LoseLife()
    {
        if (currentLives <= 0) return;

        currentLives--;
        UpdateHeartsUI();

        // Nếu mất hết cả 3 trái tim -> Hiện trang Replay / Menu
        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null)
            {
                // Ẩn bớt trái tim khi bị mất máu
                hearts[i].SetActive(i < currentLives);
            }
        }
    }

    void GameOver()
    {
        Time.timeScale = 0f; // Dừng màn chơi lại
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    // Gắn vào nút Replay
    public void ReplayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Gắn vào nút Menu
    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("00_MainMenu");
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;

        if (pausePanel != null)
            pausePanel.SetActive(isPaused);

        // Tự động đổi hình nút Pause
        UpdatePauseIcon();
    }

    void UpdatePauseIcon()
    {
        if (pauseButtonImage != null)
        {
            // Nếu đang dừng (isPaused = true) -> Hiện icon Play (pause_dung)
            // Nếu đang chạy (isPaused = false) -> Hiện icon Pause (pause_chay)
            pauseButtonImage.sprite = isPaused ? pauseDungSprite : pauseChaySprite;
        }
    }
}