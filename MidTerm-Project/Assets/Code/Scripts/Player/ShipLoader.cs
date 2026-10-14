using UnityEngine;

public class ShipLoader : MonoBehaviour
{
    public Sprite[] shipSprites;

    void Start()
    {
        int selectedIndex = PlayerPrefs.GetInt("SelectedShipIndex", 0);
        if (shipSprites != null && selectedIndex < shipSprites.Length)
        {
            GetComponent<SpriteRenderer>().sprite = shipSprites[selectedIndex];
        }
    }
}