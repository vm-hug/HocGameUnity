using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Header("Tốc độ cuộn nền")]
    public float scrollSpeed = 0.5f;

    private Material mat;

    void Start()
    {
        // Lấy Material từ Mesh Renderer của Quad
        mat = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        if (mat != null)
        {
            // Cuộn ảnh liên tục theo thời gian thực
            mat.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime, 0f);
        }
    }
}