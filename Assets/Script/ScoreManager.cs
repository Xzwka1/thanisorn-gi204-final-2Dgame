using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;

    // 🌟 จุดสำคัญที่ 1: เปลี่ยนมาใช้คำว่า static เพื่อให้คะแนนอยู่รอดข้าม Scene
    public static int totalScore = 0;

    [Header("การเปลี่ยนฉาก")]
    public int targetScore = 26;        // ตั้งเป้าหมายรวมทั้งหมด
    public string creditSceneName = "EndCredit";

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // 🌟 ทันทีที่โหลด Scene ใหม่ ให้ดึงคะแนนที่สะสมไว้ออกมาโชว์ที่ UI ทันที
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        totalScore += amount;
        UpdateScoreUI();

        // ตรวจสอบเงื่อนไข: ถ้าคะแนนรวมถึงเป้าหมาย ให้เด้งไปหน้า End Credit
        if (totalScore >= targetScore)
        {
            Invoke("GoToCreditScene", 1f);
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + totalScore.ToString();
        }
    }

    void GoToCreditScene()
    {
        SceneManager.LoadScene(creditSceneName);
    }

    // 🌟 จุดสำคัญที่ 2: ฟังก์ชันสำหรับเคลียร์คะแนนกลับเป็น 0 ตอนเริ่มเกมใหม่
    public void ResetScore()
    {
        totalScore = 0;
    }
}