using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Awake()
    {
        instance = this;
    }

    public void AddScore(int amount)
    {
        score += amount;

        // ใส่ if กันเหนียวไว้ เผื่อพี่ยังไม่ได้ลาก UI มาใส่ โค้ดจะได้ไม่พังครับ
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }

        Debug.Log("คะแนนปัจจุบัน: " + score);
    }
}