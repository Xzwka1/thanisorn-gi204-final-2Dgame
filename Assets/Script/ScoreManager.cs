using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // 🚨 สำคัญมาก: ต้องเพิ่มบรรทัดนี้เพื่อใช้คำสั่งเปลี่ยนฉาก

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    private int score = 0;

    [Header("การเปลี่ยนฉาก")]
    public int targetScore = 16;        // ตั้งเป้าหมายไว้ที่ 16 แต้ม
    public string creditSceneName = "EndCredit"; // ชื่อ Scene ที่จะเด้งไป (ต้องตั้งให้ตรงใน Unity)

    void Awake() { instance = this; }

    public void AddScore(int amount)
    {
        score += amount;
        if (scoreText != null) scoreText.text = "Score: " + score.ToString();

        // 🌟 ตรวจสอบเงื่อนไข: ถ้าคะแนนครบตามเป้า ให้เปลี่ยนฉากทันที
        if (score >= targetScore)
        {
            Invoke("GoToCreditScene", 1f); // หน่วงเวลา 1 วินาทีก่อนเปลี่ยนฉาก (เพื่อให้เห็นคะแนนสุดท้าย)
        }
    }

    void GoToCreditScene()
    {
        // คำสั่งโหลดฉากใหม่
        SceneManager.LoadScene(creditSceneName);
    }
}