using UnityEngine;

public class ShipLoader : MonoBehaviour
{
    [Header("Danh sách hình ảnh tàu (0: Su-57, 1: F-35)")]
    public Sprite[] shipSprites;

    [Header("Các ngọn lửa đuôi tàu")]
    public GameObject leftFlame;   // Kéo Jet_Flame_Left vào đây
    public GameObject rightFlame;  // Kéo Jet_Flame_Right vào đây
    public GameObject centerFlame; // Kéo Jet_Flame_Center vào đây

    void Start()
    {
        // Đọc chỉ số tàu đã chọn từ Main Menu (mặc định là 0 - Su-57)
        int selectedIndex = PlayerPrefs.GetInt("SelectedShipIndex", 0);

        // 1. Thay đổi hình ảnh thân máy bay
        if (shipSprites != null && selectedIndex < shipSprites.Length)
        {
            GetComponent<SpriteRenderer>().sprite = shipSprites[selectedIndex];
        }

        // 2. Bật/tắt ngọn lửa tương ứng với từng loại tàu
        if (selectedIndex == 0)
        {
            // Tàu 0 (top_xoanen - Su-57): Bật 2 lửa hai bên, tắt lửa giữa
            if (leftFlame != null) leftFlame.SetActive(true);
            if (rightFlame != null) rightFlame.SetActive(true);
            if (centerFlame != null) centerFlame.SetActive(false);
        }
        else if (selectedIndex == 1)
        {
            // Tàu 1 (topf35_xoanen - F-35): Tắt 2 lửa hai bên, chỉ bật 1 lửa giữa
            if (leftFlame != null) leftFlame.SetActive(false);
            if (rightFlame != null) rightFlame.SetActive(false);
            if (centerFlame != null) centerFlame.SetActive(true);
        }
    }
}