using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [Header("Các bảng UI Panel")]
    public GameObject mainMenuPanel;
    public GameObject shipSelectPanel;
    public GameObject settingPanel;

    [Header("Chọn Tàu (Ship Selection)")]
    public TextMeshProUGUI shipStatusText; // Hiển thị thông báo tàu đang được chọn

    [Header("Cài đặt (Settings)")]
    public Slider volumeSlider;            // Thanh trượt chỉnh âm lượng
    public TextMeshProUGUI highScoreText;  // Hiển thị kỷ lục hiện tại trong bảng Cài đặt

    void Start()
    {
        // 1. Khi mới vào Menu: Hiện bảng chính, ẩn 2 bảng phụ
        ShowMainMenu();

        // 2. Tải mức âm lượng đã lưu (mặc định là 1 = 100%)
        float savedVolume = PlayerPrefs.GetFloat("GameVolume", 1f);
        AudioListener.volume = savedVolume;
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
        }

        // 3. Hiển thị tàu đang chọn và kỷ lục điểm
        int currentShip = PlayerPrefs.GetInt("SelectedShipIndex", 0);
        UpdateShipStatusText(currentShip);
        UpdateHighScoreDisplay();
    }

    // --- 1. CHỨC NĂNG NÚT PLAY & THOÁT ---
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("01_Gameplay");
    }

    public void QuitGame()
    {
        Debug.Log("Đã bấm thoát Game!");
        Application.Quit();
        
        // Nếu đang chạy thử trong Unity Editor thì tự động dừng chế độ Play
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // --- 2. CHUYỂN ĐỔI QUA LẠI GIỮA CÁC BẢNG PANEL ---
    public void ShowMainMenu()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (shipSelectPanel != null) shipSelectPanel.SetActive(false);
        if (settingPanel != null) settingPanel.SetActive(false);
    }

    public void OpenShipSelect()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (shipSelectPanel != null) shipSelectPanel.SetActive(true);
    }

    public void OpenSettings()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (settingPanel != null) settingPanel.SetActive(true);
        UpdateHighScoreDisplay();
    }

    // --- 3. CHỨC NĂNG CHỌN TÀU (0: Su57, 1: F35) ---
    public void SelectShip(int shipIndex)
    {
        PlayerPrefs.SetInt("SelectedShipIndex", shipIndex);
        PlayerPrefs.Save();
        UpdateShipStatusText(shipIndex);
    }

    void UpdateShipStatusText(int index)
    {
        if (shipStatusText != null)
        {
            string shipName = (index == 0) ? "Tàu Chiến Su-57 (Xanh)" : "Tàu Chiến F-35 (Xám)";
            shipStatusText.text = "Đang chọn: " + shipName;
        }
    }

    // --- 4. CHỨC NĂNG NÂNG CẤP TRONG CÀI ĐẶT ---
    public void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("GameVolume", value);
        PlayerPrefs.Save();
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.Save();
        UpdateHighScoreDisplay();
    }

    void UpdateHighScoreDisplay()
    {
        if (highScoreText != null)
        {
            int best = PlayerPrefs.GetInt("HighScore", 0);
            highScoreText.text = "Kỷ lục hiện tại: " + best;
        }
    }
}